using Microsoft.EntityFrameworkCore;
using TestApbd.Models;

namespace TestApbd.Context;

public class LocalDbContext : DbContext
{
    public LocalDbContext()
    {
        
    }

    public LocalDbContext(DbContextOptions<LocalDbContext> options) : base(options)
    {
    }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Sale> Sales { get; set; }
    
}