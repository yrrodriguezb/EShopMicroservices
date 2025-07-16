
using Ordering.Application.Orders.Commands.DeleteOrder;

namespace Ordering.API.Endpoints;

// Represents a request to delete an order
// Constructs a DeleteOrderCommand
// Sends the command using MediatR
// Returns a success or not found response

public record DeleteOrderRequest(Guid Id);

public record DeleteOrderResponse(bool IsSuccess);

public class DeleteOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/orders/{id}", async (Guid Id, ISender sender) =>
        {
            var command = new DeleteOrderCommand(Id);

            var result = await sender.Send(command);

            var response = result.Adapt<DeleteOrderResponse>();

            return Results.Ok(response);
        })
        .WithName("DeleteOrder")
        .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Deletes an existing order")
        .WithDescription("Deletes an existing order based on the provided order ID.");
    }
}