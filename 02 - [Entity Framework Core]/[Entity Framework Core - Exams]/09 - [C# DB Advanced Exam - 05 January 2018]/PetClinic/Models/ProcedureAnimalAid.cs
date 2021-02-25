using System.ComponentModel.DataAnnotations;

namespace PetClinic.Models
{
    public class ProcedureAnimalAid
    {
        [Required]
        public int ProcedureId { get; set; }

        [Required]
        public virtual Procedure Procedure { get; set; }

        [Required]
        public int AnimalAidId { get; set; }

        [Required]
        public virtual AnimalAid AnimalAid { get; set; }
    }
}