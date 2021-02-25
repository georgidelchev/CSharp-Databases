using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Stations.DataProcessor.Dto.Import
{
    [XmlType("Trip")]
    public class ImportTicketTripDto
    {
        [Required]
        [XmlElement("OriginStation")]
        public string OriginStation { get; set; }

        [Required]
        [XmlElement("DestinationStation")]
        public string DestinationStation { get; set; }

        [Required]
        [XmlElement("DepartureTime")]
        public string DepartureTime { get; set; }
    }
}