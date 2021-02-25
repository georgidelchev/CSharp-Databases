using System.Xml.Serialization;

namespace Stations.DataProcessor.Dto.Export
{
    [XmlType("Ticket")]
    public class ExportCardTicketDto
    {
        [XmlElement("OriginStation")]
        public string OriginStation { get; set; }

        [XmlElement("DestinationStation")]
        public string DestinationStation { get; set; }

        [XmlElement("DepartureTime")]
        public string DepartureTime { get; set; }
    }
}