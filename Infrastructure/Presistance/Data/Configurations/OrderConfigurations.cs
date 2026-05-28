using DomainLayer.Models.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistance.Data.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(O => O.ShippingAddress, Address => Address.WithOwner());

            builder.Property(O => O.Status)
                .HasConversion(S => S.ToString(), S => (OrderStatus)Enum.Parse(typeof(OrderStatus), S));

            builder.Property(O => O.Subtotal)
                .HasColumnType("decimal(12,2)");

            builder.HasOne(O => O.DeliveryMethod)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(O => O.Items)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
