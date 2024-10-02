using InterviewPreparation.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewPreparation.Config;

public class DiscountConfig : IEntityTypeConfiguration<Discount>
{
    public void Configure(EntityTypeBuilder<Discount> builder)
    {
        builder.HasKey(discount => discount.IdDiscount);
        
        builder.Property(discount => discount.Value)
            .IsRequired();

        builder.Property(discount => discount.DateFrom)
            .IsRequired();
        
        builder.Property(discount => discount.DateTo)
            .IsRequired();

        builder.HasOne(discount => discount.Client)
            .WithMany(client => client.Discounts)
            .HasForeignKey(discount => discount.IdClient)
            .IsRequired();
    }
}