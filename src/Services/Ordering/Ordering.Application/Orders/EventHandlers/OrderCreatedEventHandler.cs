namespace Ordering.Application.Orders.EventHandlers;

public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger)
 : INotificationHandler<OrderCreatedEvent>
{
    public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);
        
        // Here you can add additional logic to handle the order creation event,
        // such as sending notifications, updating other systems, etc.

        return Task.CompletedTask;
    }
}