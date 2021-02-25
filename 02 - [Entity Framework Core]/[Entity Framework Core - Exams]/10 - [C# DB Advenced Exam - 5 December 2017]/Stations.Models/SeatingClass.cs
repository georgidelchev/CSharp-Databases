using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Stations.Models
{
    public class SeatingClass
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        public string Abbreviation { get; set; }

        public virtual ICollection<TrainSeat> TrainSeats { get; set; }
            = new HashSet<TrainSeat>();
    }
}