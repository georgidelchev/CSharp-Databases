using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;

namespace MusicHub.DataProcessor.ImportDtos
{
    [XmlType("Song")]
    public class ImportPerformerSongDto
    {
        [XmlAttribute("id")]
        [Required]
        public int Id { get; set; }
    }
}