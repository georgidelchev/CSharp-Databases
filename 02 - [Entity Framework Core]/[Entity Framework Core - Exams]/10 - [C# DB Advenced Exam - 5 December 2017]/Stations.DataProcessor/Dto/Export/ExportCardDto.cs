using System.Xml.Serialization;
using System.Collections.Generic;

namespace Stations.DataProcessor.Dto.Export
{
    [XmlType("Card")]
    public class ExportCardDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlArray("Tickets")]
        public List<ExportCardTicketDto> Tickets { get; set; }
    }
}