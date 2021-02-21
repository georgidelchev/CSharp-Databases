using System.Xml.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicHub.DataProcessor.ImportDtos
{
    [XmlType("Performer")]
    public class ImportPerformerDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string LastName { get; set; }

        [Required]
        [Range(18, 70)]
        public int Age { get; set; }

        [Required]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal NetWorth { get; set; }

        [Required]
        [XmlArray("PerformersSongs")]
        public List<ImportPerformerSongDto> PerformerSongs { get; set; }
    }
}