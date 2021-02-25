using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace PetClinic.DataProcessor.Dto.Import
{
    [XmlType("Procedure")]
    public class ImportProcedureDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        public string Vet { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Animal { get; set; }

        [Required]
        public string DateTime { get; set; }

        [Required]
        public List<ImportProcedureAnimalAidDto> AnimalAids { get; set; }
    }
}