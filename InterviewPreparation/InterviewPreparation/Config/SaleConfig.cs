using InterviewPreparation.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewPreparation.Config;

public class SaleConfig : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.HasKey(sale => sale.IdSale);

        builder.HasOne(sale => sale.Client)
            .WithMany(client => client.Sales)
            .HasForeignKey(sale => sale.IdClient);

        builder.HasOne(sale => sale.Subscription)
            .WithMany(subscription => subscription.Sales)
            .HasForeignKey(sale => sale.IdSubscription);
    }
}