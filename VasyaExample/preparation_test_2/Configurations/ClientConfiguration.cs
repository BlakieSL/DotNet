using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using preparation_test_2.Models;

namespace preparation_test_2.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(s => s.IdClient);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(s => s.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.Birthday)
            .IsRequired();
        
        builder.Property(s => s.Pesel)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(s => s.Reservations)
            .WithOne(sc => sc.Client)
            .HasForeignKey(sc => sc.IdClient);

        builder.HasOne(s => s.ClientCategory)
            .WithMany(sc => sc.Clients)
            .HasForeignKey(sc => sc.IdClientCategory);


    }
}