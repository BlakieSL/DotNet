using Microsoft.EntityFrameworkCore;
using Project3.Models;

namespace Project3.Context;

public class LocalDbContext : DbContext
{
    public LocalDbContext()
    {
    }

    public LocalDbContext(DbContextOptions<LocalDbContext> options) : base(options)
    {
    }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Individual> Individuals { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<SoftwareSystem_Discount> SoftwareSystemDiscounts { get; set; }
    public DbSet<SoftwareSystem> SoftwareSystems { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User_Role> UserRoles { get; set; }
}