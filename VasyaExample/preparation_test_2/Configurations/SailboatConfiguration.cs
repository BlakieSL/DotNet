using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using preparation_test_2.Models;

namespace preparation_test_2.Configurations;

public class SailboatConfiguration : IEntityTypeConfiguration<Sailboat>
{
    public void Configure(EntityTypeBuilder<Sailboat> builder)
    {
        builder.HasKey(s => s.IdSailboat);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.Capacity)
            .IsRequired();
        
        builder.Property(s => s.Description)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(s => s.Price)
            .IsRequired();

        builder.HasMany(s => s.SailboatReservations)
            .WithOne(sc => sc.Sailboat)
            .HasForeignKey(sc => sc.IdSailboat);

        builder.HasOne(s => s.BoatStandard)
            .WithMany(sc => sc.Sailboats)
            .HasForeignKey(sc => sc.IdBoatStandard);
    }
}