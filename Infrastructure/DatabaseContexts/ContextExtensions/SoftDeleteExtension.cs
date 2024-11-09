using Domain.Entities;
using Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.DatabaseContexts.ContextExtensions;
internal static class SoftDeleteExtension
{
    public static void AddSoftDeleteExtension(this ModelBuilder modelBuilder, string property, object value)
    {
        Type[] softDeleteTypes = { typeof(Auditable), typeof(Token), typeof(ErrorLog) };

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (softDeleteTypes.Contains(entityType.ClrType))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "p");
                var deletedCheck = Expression.Lambda(
                    Expression.Equal(
                        Expression.Property(parameter, property),
                        Expression.Constant(value)
                    ), parameter);
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(deletedCheck);
            }
        }
    }
}