namespace Ordering.Domain.Abstractions;

public abstract class Entity<T> : IEntity
{
    public T Id { get; set; } = default!;
    public DateTime? CreateAt { get; set; }
    public string? CreateBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
}