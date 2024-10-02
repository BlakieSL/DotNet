using InterviewPreparation.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewPreparation.Config;

public class ClientConfig : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(client => client.IdClient);

        builder.Property(client => client.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(client => client.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(client => client.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(client => client.Phone)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(client => client.Discounts)
            .WithOne(discount => discount.Client)
            .HasForeignKey(discount => discount.IdClient);

        builder.HasMany(client => client.Payments)
            .WithOne(payment => payment.Client)
            .HasForeignKey(payment => payment.IdClient);
    }
}