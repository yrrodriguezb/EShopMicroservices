using FluentValidation;
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand(OrderDto Order) : BuildingBlocks.CQRS.ICommand<UpdateOrderResult>;

public record UpdateOrderResult(bool IsSuccess);

public class UpdateOrderCommandValidator : FluentValidation.AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.Order)
            .NotNull()
            .WithMessage("Order cannot be null.");

        RuleFor(x => x.Order.Id)
            .NotEmpty()
            .WithMessage("Order Id is required.");

        RuleFor(x => x.Order.OrderName)
            .NotEmpty()
            .WithMessage("Name is required.");

        RuleFor(x => x.Order.CustomerId)
            .NotNull()
            .WithMessage("CustomerId is required.");
    }
}