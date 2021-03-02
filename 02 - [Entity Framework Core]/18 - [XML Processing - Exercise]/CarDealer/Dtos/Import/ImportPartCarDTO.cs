using System.Xml.Serialization;

namespace CarDealer.Dtos.Import
{
    [XmlType("partId")]
    public class ImportPartCarDTO
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
    }
}