using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using preparation_test_2.Models;

namespace preparation_test_2.Configurations;

public class SailboatReservationConfiguration : IEntityTypeConfiguration<Sailboat_Reservation>
{
    public void Configure(EntityTypeBuilder<Sailboat_Reservation> builder)
    {
        builder.HasKey(sc => new { sc.IdReservation, sc.IdSailboat });

        builder.HasOne(s => s.Reservation)
            .WithMany(sc => sc.SailboatReservations)
            .HasForeignKey(sc => sc.IdReservation)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(s => s.Sailboat)
            .WithMany(sc => sc.SailboatReservations)
            .HasForeignKey(sc => sc.IdSailboat)
            .OnDelete(DeleteBehavior.NoAction);
    }
}