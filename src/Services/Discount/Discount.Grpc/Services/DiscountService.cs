using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Discount.Grpc.Protos;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;
namespace Discount.Grpc.Services;

public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
{
    //private readonly IDiscountRepository _repository;
    private readonly ILogger<DiscountService> _logger;
    private readonly DiscountContext _dbContext;

    public DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger)
    {
        //_repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await _dbContext.Coupons
            .FirstOrDefaultAsync(x => x.ProductName == request.ProductName, context.CancellationToken);

        if (coupon is null)
            coupon = new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };

        _logger.LogInformation("Discount retrieved for the product: {ProductName}, Amount: {Amount}", coupon.ProductName, coupon.Amount);

        return coupon.Adapt<CouponModel>();
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();

        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid coupon data"));

        _dbContext.Coupons.Add(coupon);
        await _dbContext.SaveChangesAsync(context.CancellationToken);

        _logger.LogInformation("Discount created for the product: {ProductName}, Amount: {Amount}", coupon.ProductName, coupon.Amount);
    
        return coupon.Adapt<CouponModel>();
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();

        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid coupon data"));

        _dbContext.Coupons.Update(coupon);
        await _dbContext.SaveChangesAsync(context.CancellationToken);

        _logger.LogInformation("Discount updated for the product: {ProductName}, Amount: {Amount}", coupon.ProductName, coupon.Amount);
    
        return coupon.Adapt<CouponModel>();
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await _dbContext.Coupons
            .FirstOrDefaultAsync(x => x.ProductName == request.ProductName, context.CancellationToken);

        if (coupon is null)
            throw new RpcException(new Status(StatusCode.NotFound, "Coupon not found"));

        _dbContext.Coupons.Remove(coupon);
        await _dbContext.SaveChangesAsync(context.CancellationToken);

        _logger.LogInformation("Discount deleted for the product: {ProductName}, Amount: {Amount}", coupon.ProductName, coupon.Amount);

        return new DeleteDiscountResponse
        {
            Success = true
        };
    }
}