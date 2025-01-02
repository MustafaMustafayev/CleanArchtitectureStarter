using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;
internal sealed class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Name).IsRequired().HasMaxLength(30);
        builder.Property(m => m.Surname).IsRequired().HasMaxLength(40);

        builder.Property(m => m.Email).IsRequired();
        builder.HasIndex(m => m.Email).IsUnique();

        builder.Property(m => m.PasswordSalt).IsRequired();
        builder.Property(m => m.PasswordHash).IsRequired();
    }
}