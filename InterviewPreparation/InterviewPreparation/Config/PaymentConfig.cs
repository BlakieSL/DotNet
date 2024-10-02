using InterviewPreparation.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewPreparation.Config;

public class PaymentConfig : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(payment => payment.IdPayment);

        builder.HasOne(payment => payment.Client)
            .WithMany(client => client.Payments)
            .HasForeignKey(payment => payment.IdClient);
        
        builder.HasOne(payment => payment.Subscription)
            .WithMany(subscription => subscription.Payments)
            .HasForeignKey(payment => payment.IdSubscription);
    }
}