using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.DatabaseContexts.ContextExtensions;
internal static class SoftDeleteExtension
{
    public static void AddSoftDeleteExtension(this ModelBuilder modelBuilder, string property, object value)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
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