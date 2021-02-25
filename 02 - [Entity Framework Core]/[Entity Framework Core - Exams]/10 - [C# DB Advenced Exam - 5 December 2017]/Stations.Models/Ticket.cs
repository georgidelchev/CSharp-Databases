using System.ComponentModel.DataAnnotations;

namespace Stations.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string SeatingPlace { get; set; }

        [Required]
        public int TripId { get; set; }

        [Required]
        public virtual Trip Trip { get; set; }

        public int? CustomerCardId { get; set; }

        public virtual CustomerCard CustomerCard { get; set; }
    }
}