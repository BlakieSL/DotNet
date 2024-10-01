using Microsoft.EntityFrameworkCore;
using TestPrep2.Models;

namespace TestPrep2.Context;

public class LocalDbContext : DbContext
{
    public LocalDbContext()
    {
        
    }

    public LocalDbContext(DbContextOptions<LocalDbContext> options) : base(options)
    {
    }
    public DbSet<ClientCategory> ClientCategories { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Sailboat_Reservation> SailboatReservations { get; set; }
    public DbSet<Sailboat> Sailboats { get; set; }
    public DbSet<BoatStandart> BoatStandarts { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Sailboat_Reservation>(entity =>
        {
            entity.HasKey(sr => new { sr.IdReservation, sr.IdSailboat });
            entity.HasOne(sr => sr.Reservation)
                .WithMany(r => r.SailboatReservations)
                .HasForeignKey(sr => sr.IdReservation)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(sr => sr.Sailboat)
                .WithMany(s => s.SailboatReservations)
                .HasForeignKey(sr => sr.IdSailboat)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}