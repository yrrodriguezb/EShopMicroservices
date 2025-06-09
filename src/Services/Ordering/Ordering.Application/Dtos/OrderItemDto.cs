namespace Ordering.Application.Dtos;

public record OrderItemDto
(
    Guid Id,
    Guid ProductId,
    int Quantity,
    decimal Price
);