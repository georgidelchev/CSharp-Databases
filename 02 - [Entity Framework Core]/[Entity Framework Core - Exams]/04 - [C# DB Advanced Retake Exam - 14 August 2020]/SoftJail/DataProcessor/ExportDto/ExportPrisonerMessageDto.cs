using System.Xml.Serialization;

namespace SoftJail.DataProcessor.ExportDto
{
    [XmlType("Message")]
    public class ExportPrisonerMessageDto
    {
        [XmlElement("Description")]
        public string Description { get; set; }
    }
}