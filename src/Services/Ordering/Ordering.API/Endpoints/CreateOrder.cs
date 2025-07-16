using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.API.Endpoints;

// Accepts a CreateOrderRequest object
// Maps the request to a CreateOrderCommand
// Uses MediatR to send the command to the appropriate handler
// Returns a response indicating the result of the operation 

public record CreateOrderRequest(OrderDto Order);

public record CreateOrdeResponse(Guid Id);

public class CreateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/orders", async (CreateOrderRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateOrderCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateOrdeResponse>();

            return Results.Created($"/orders/{response.Id}", response);
        })
        .WithName("CreateOrder")
        .Produces<CreateOrdeResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Creates a new order")
        .WithDescription("Creates a new order based on the provided order details.");
    }
}