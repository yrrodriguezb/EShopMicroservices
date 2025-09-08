using MassTransit;
using Microsoft.FeatureManagement;

namespace Ordering.Application.Orders.EventHandlers.Domain;

public class OrderCreatedEventHandler
(
    IPublishEndpoint publishEndpoint,
    FeatureManager featureManager,
    ILogger<OrderCreatedEventHandler> logger
)
 : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event: {DomainEvent}", domainEvent.GetType().Name);

        var orderCreateIntegrationEvent = domainEvent.Order.ToOrderDto();

        if (await featureManager.IsEnabledAsync("OrderFullfilment", cancellationToken))
        {
            await publishEndpoint.Publish(orderCreateIntegrationEvent, cancellationToken);
        }
    }
}