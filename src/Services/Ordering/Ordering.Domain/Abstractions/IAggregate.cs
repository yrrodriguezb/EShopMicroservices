namespace Ordering.Domain.Abstractions;

public interface IAggreate<T> : IAggreate, IEntity<T>
{ 
    
}

public interface IAggreate : IEntity
{
    IReadOnlyList<IDomainEvent> DomainEvents { get; }
    IDomainEvent[] ClearDomainEvents();
}