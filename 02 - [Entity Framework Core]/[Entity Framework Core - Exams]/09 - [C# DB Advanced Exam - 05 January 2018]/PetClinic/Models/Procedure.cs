using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace PetClinic.Models
{
    public class Procedure
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Animal))]
        public int AnimalId { get; set; }

        [Required]
        public virtual Animal Animal { get; set; }

        [Required]
        [ForeignKey(nameof(Vet))]
        public int VetId { get; set; }

        [Required]
        public virtual Vet Vet { get; set; }

        public virtual ICollection<ProcedureAnimalAid> ProcedureAnimalAids { get; set; }
            = new HashSet<ProcedureAnimalAid>();

        [NotMapped]
        public decimal Cost { get; set; }

        [Required]
        public DateTime DateTime { get; set; }
    }
}