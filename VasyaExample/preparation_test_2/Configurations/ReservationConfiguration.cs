using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using preparation_test_2.Models;

namespace preparation_test_2.Configurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasKey(s => s.IdReservation);

        builder.Property(s => s.DateFrom)
            .IsRequired();
        builder.Property(s => s.DateTo)
            .IsRequired();
        builder.Property(s => s.Capacity)
            .IsRequired();
        builder.Property(s => s.NumOfBoats)
            .IsRequired();
        builder.Property(s => s.Fulfilled)
            .IsRequired();
        builder.Property(s => s.Price)
            .IsRequired(false);
        builder.Property(s => s.CancelReason)
            .IsRequired(false)
            .HasMaxLength(100);

        builder.HasMany(s => s.SailboatReservations)
            .WithOne(sc => sc.Reservation)
            .HasForeignKey(sc => sc.IdReservation);

        builder.HasOne(s => s.BoatStandard)
            .WithMany(sc => sc.Reservations)
            .HasForeignKey(sc => sc.IdBoatStandard);

        builder.HasOne(s => s.Client)
            .WithMany(sc => sc.Reservations)
            .HasForeignKey(sc => sc.IdClient);


    }
}