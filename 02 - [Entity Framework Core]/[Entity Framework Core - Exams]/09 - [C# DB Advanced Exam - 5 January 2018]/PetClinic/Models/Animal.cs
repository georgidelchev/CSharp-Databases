using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetClinic.Models
{
    public class Animal
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        public string Type { get; set; }

        [Required]
        public int Age { get; set; }

        public string PassportSerialNumber { get; set; }

        [Required]
        public virtual Passport Passport { get; set; }

        public virtual ICollection<Procedure> Procedures { get; set; }
            = new HashSet<Procedure>();
    }
}