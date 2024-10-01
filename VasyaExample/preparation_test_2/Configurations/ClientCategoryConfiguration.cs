using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using preparation_test_2.Models;

namespace preparation_test_2.Configurations;

public class ClientCategoryConfiguration : IEntityTypeConfiguration<ClientCategory>
{
    public void Configure(EntityTypeBuilder<ClientCategory> builder)
    {
        builder.HasKey(s => s.IdClientCategory);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.DiscountPerc)
            .IsRequired();

        builder.HasMany(s => s.Clients)
            .WithOne(sc => sc.ClientCategory)
            .HasForeignKey(sc => sc.IdClientCategory);
    }
}