using Application.Helpers;
using Domain.Primitives;
using Domain.Repositories;
using Infrastructure.DatabaseContexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public class UnitOfWork(AppDbContext dbContext, 
                        ITokenResolverService tokenResolverService) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        SetAuditProperties();
        return await dbContext.SaveChangesAsync(cancellationToken);
    }

    private void SetAuditProperties()
    {
        var entries = dbContext.ChangeTracker.Entries().Where(e => e.Entity is Auditable && e.State is EntityState.Added or EntityState.Modified);

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
    }

}
