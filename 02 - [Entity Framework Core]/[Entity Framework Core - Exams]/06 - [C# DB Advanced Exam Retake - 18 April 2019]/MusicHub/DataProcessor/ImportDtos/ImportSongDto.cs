using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;

namespace MusicHub.DataProcessor.ImportDtos
{
    [XmlType("Song")]
    public class ImportSongDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        public string Duration { get; set; }

        [Required]
        public string CreatedOn { get; set; }

        [Required]
        public string Genre { get; set; }

        public int? AlbumId { get; set; }

        [Required]
        public int WriterId { get; set; }

        [Required]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Price { get; set; }
    }
}