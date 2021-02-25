using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Stations.DataProcessor.Dto.Import
{
    [XmlType("Card")]
    public class ImportTicketCardDto
    {
        [Required]
        [XmlAttribute("Name")]
        public string Name { get; set; }
    }
}