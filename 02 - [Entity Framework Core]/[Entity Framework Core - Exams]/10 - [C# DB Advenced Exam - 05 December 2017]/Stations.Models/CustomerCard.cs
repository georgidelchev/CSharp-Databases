using Stations.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Stations.Models
{
    public class CustomerCard
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public CardType Type { get; set; }

        public virtual ICollection<Ticket> BoughtTickets { get; set; }
            = new HashSet<Ticket>();
    }
}