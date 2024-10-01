using Microsoft.EntityFrameworkCore;
using preparation_test_2.Models;

namespace preparation_test_2.Context;

public partial class BoatReservationServiceDbContext : DbContext
{
    protected BoatReservationServiceDbContext()
    {
    }

    public BoatReservationServiceDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<ClientCategory> ClientCategories { get; set; }
    public DbSet<BoatStandard> BoatStandards { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Sailboat> Sailboats { get; set; }
    public DbSet<Sailboat_Reservation> SailboatReservations { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BoatReservationServiceDbContext).Assembly);
        // base.OnModelCreating(modelBuilder);
    }
}