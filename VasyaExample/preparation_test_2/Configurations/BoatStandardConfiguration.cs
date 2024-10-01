using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using preparation_test_2.Models;

namespace preparation_test_2.Configurations;

public class BoatStandardConfiguration : IEntityTypeConfiguration<BoatStandard>
{
    public void Configure(EntityTypeBuilder<BoatStandard> builder)
    {
        builder.HasKey(s => s.IdBoatStandard);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.Level)
            .IsRequired();

        builder.HasMany(s => s.Reservations)
            .WithOne(sc => sc.BoatStandard)
            .HasForeignKey(sc => sc.IdBoatStandard);
        
        builder.HasMany(s => s.Sailboats)
            .WithOne(sc => sc.BoatStandard)
            .HasForeignKey(sc => sc.IdBoatStandard);
    }
}