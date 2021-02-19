using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;

namespace SoftJail.DataProcessor.ImportDto
{
    [XmlType("Prisoner")]
    public class ImportOfficerPrisonerDto
    {
        [Required]
        [XmlAttribute("id")]
        public int Id { get; set; }
    }
}