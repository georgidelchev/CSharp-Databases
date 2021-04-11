using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Data.Models
{
    public class Projection
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Movie))]
        public int MovieId { get; set; }

        [Required]
        public virtual Movie Movie { get; set; }

        [Required]
        [ForeignKey(nameof(Hall))]
        public int HallId { get; set; }

        [Required]
        public virtual Hall Hall { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
            = new HashSet<Ticket>();
    }
}