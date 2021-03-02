using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCoolCarSystem.Data.Models;

namespace MyCoolCarSystem.Data.Configurations
{
    public class CarPurchaseConfiguration : IEntityTypeConfiguration<CarPurchase>
    {
        public void Configure(EntityTypeBuilder<CarPurchase> carPurchase)
        {
            carPurchase
                .HasKey(cp => new
                {
                    cp.CustomerId,
                    cp.CarId
                });

            carPurchase
                .HasOne(cp => cp.Customer)
                .WithMany(c => c.Purchases)
                .HasForeignKey(cp => cp.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            carPurchase
                .HasOne(cp => cp.Car)
                .WithMany(c => c.Owners)
                .HasForeignKey(cp => cp.CarId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}