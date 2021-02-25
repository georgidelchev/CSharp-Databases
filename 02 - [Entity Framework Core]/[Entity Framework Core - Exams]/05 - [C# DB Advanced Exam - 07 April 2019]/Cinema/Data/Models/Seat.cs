using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Data.Models
{
    public class Seat
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Hall))]
        public int HallId { get; set; }

        [Required]
        public virtual Hall Hall { get; set; }
    }
}