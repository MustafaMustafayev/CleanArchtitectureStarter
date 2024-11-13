using Application.Helpers;
using Domain.Entities;
using Infrastructure.DatabaseContexts.ContextExtensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DatabaseContexts;
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Token> Tokens { get; set; }
    public DbSet<ErrorLog> ErrorLogs { get; set; }

    /* migration commands
          dotnet ef --startup-project ../WebApi migrations add remove_refresh_token --context AppDbContext
          dotnet ef --startup-project ../WebApi database update --context AppDbContext
    */

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        const string softDeleteProperty = "IsDeleted";
        modelBuilder.AddSoftDeleteExtension(softDeleteProperty, false);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReference).Assembly);
    }
}