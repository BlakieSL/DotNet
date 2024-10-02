using InterviewPreparation.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewPreparation.Config;

public class SubscriptionConfig : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.HasKey(subscription => subscription.IdSubscription);

        builder.HasMany(subscription => subscription.Sales)
            .WithOne(sale => sale.Subscription)
            .HasForeignKey(sale => sale.IdSubscription);

        builder.HasMany(subscription => subscription.Payments)
            .WithOne(payment => payment.Subscription)
            .HasForeignKey(payment => payment.IdSubscription);
    }
}