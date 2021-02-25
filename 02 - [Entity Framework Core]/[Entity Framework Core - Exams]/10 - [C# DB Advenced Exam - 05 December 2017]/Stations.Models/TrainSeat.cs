using System.ComponentModel.DataAnnotations;

namespace Stations.Models
{
    public class TrainSeat
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TrainId { get; set; }

        [Required]
        public virtual Train Train { get; set; }

        [Required]
        public int SeatingClassId { get; set; }

        [Required]
        public virtual SeatingClass SeatingClass { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
    }
}