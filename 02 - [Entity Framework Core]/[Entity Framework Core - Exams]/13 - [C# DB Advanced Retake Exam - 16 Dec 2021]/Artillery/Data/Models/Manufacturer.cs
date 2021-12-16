using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Artillery.Data.Models
{
    public class Manufacturer
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string ManufacturerName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Founded { get; set; }

        public virtual ICollection<Gun> Guns { get; set; }
            = new HashSet<Gun>();
    }
}
