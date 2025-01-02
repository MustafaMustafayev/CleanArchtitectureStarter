using Application.Interfaces;
using Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Interceptors;
public sealed class AuditingSaveChangesInterceptor(ITokenResolverService tokenResolverService) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        var dbContext = eventData.Context;

        var entries = dbContext!.ChangeTracker.Entries().Where(e => e.Entity is Auditable && e.State is EntityState.Added or EntityState.Modified);

        Guid? userId = tokenResolverService.GetUserIdFromToken();
        DateTime operationDate = DateTime.Now;

        foreach (var entityEntry in entries)
        {
            Auditable auditableEntry = (Auditable)entityEntry.Entity;

            switch (entityEntry.State)
            {
                case EntityState.Added:
                    auditableEntry.CreatedAt = operationDate;
                    auditableEntry.CreatedById = userId;
                    break;
                case EntityState.Modified:
                    {
                        dbContext.Entry(auditableEntry).Property(p => p.CreatedAt).IsModified = false;
                        dbContext.Entry(auditableEntry).Property(p => p.CreatedById).IsModified = false;

                        if (((Auditable)entityEntry.Entity).IsDeleted)
                        {
                            dbContext.Entry(auditableEntry).Property(p => p.ModifiedById).IsModified = false;
                            dbContext.Entry(auditableEntry).Property(p => p.ModifiedAt).IsModified = false;

                            auditableEntry.DeletedAt = operationDate;
                            auditableEntry.DeletedById = userId;
                        }
                        else
                        {
                            auditableEntry.ModifiedAt = operationDate;
                            auditableEntry.ModifiedById = userId;
                        }

                        break;
                    }
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                case EntityState.Deleted:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return base.SavingChanges(eventData, result);
    }
}