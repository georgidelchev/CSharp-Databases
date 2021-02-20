using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Data.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [ForeignKey(nameof(Customer))]
        public int CustomerId { get; set; }

        [Required]
        public virtual Customer Customer { get; set; }

        [Required]
        [ForeignKey(nameof(Projection))]
        public int ProjectionId { get; set; }

        [Required]
        public virtual Projection Projection { get; set; }
    }
}