using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Stations.DataProcessor.Dto.Import
{
    [XmlType("Card")]
    public class ImportPersonCardDto
    {
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        [Required]
        [Range(0, 120)]
        public int Age { get; set; }

        public string CardType { get; set; }
    }
}