using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ordering.Infrastructure.Data.Interceptors;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public void UpdateEntities(DbContext? context)
    {
        if (context == null) return;

        var entries = context.ChangeTracker.Entries<IEntity>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreateBy = "yrrodriguezb";
                entry.Entity.CreateAt = DateTime.UtcNow;
            }

            if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
            {
                entry.Entity.LastModifiedBy = "yrrodriguezb";
                entry.Entity.LastModified = DateTime.UtcNow;
            }
        }
    }
}

public static class Extension
{ 
    public static bool HasChangedOwnedEntities(this EntityEntry entry)
    {
        return entry.References.Any
        (
            r => r.TargetEntry != null && 
                r.TargetEntry.Metadata.IsOwned() &&
                (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified)
        );
    }
}