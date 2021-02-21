using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FastFood.Models
{
    public class OrderItem
    {
        [Required]
        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }

        [Required]
        public virtual Order Order { get; set; }

        [Required]
        [ForeignKey(nameof(Item))]
        public int ItemId { get; set; }

        [Required]
        public virtual Item Item { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}