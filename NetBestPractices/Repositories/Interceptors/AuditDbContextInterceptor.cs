using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Repositories.Entities.Common;

namespace Repositories.Interceptors
{
    public class AuditDbContextInterceptor:SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var selectEntity = eventData.Context!.ChangeTracker.Entries<BaseEntity>();

            foreach (var entityEntry in selectEntity)
            {
                switch (entityEntry.State)
                {
                    case EntityState.Added:
                        entityEntry.Entity.CreateDate = DateTime.UtcNow;
                        entityEntry.Entity.UpdateDate = null;
                        break;
                    case EntityState.Modified:
                        entityEntry.Entity.UpdateDate = DateTime.UtcNow;
                        break;
                    
                    default:
                        _ = DateTime.UtcNow;
                        break;
                }
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
