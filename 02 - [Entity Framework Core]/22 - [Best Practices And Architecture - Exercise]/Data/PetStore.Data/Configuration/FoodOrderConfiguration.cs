using PetStore.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetStore.Data.Configuration
{
    public class FoodOrderConfiguration : IEntityTypeConfiguration<FoodOrder>
    {
        public void Configure(EntityTypeBuilder<FoodOrder> foodOrder)
        {
            foodOrder
                .HasKey(fo => new
                {
                    fo.FoodId,
                    fo.OrderId
                });

            foodOrder
                .HasOne(fo => fo.Order)
                .WithMany(o => o.Foods)
                .HasForeignKey(fo=>fo.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            foodOrder
                .HasOne(fo => fo.Food)
                .WithMany(f => f.Orders)
                .HasForeignKey(fo => fo.FoodId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}