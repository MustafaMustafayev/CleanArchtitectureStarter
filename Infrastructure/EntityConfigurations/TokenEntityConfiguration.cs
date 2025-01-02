using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;
internal class TokenEntityConfiguration : IEntityTypeConfiguration<Token>
{
    public void Configure(EntityTypeBuilder<Token> builder)
    {
        builder.ToTable("Tokens");

        builder.HasKey(x => x.Id);

        builder.HasOne(m => m.User)
               .WithMany()
               .HasForeignKey(m => m.UserId);
    }
}