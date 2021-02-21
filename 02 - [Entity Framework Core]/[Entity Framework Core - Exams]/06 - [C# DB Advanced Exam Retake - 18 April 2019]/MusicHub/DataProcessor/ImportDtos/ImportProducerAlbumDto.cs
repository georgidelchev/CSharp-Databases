using System.ComponentModel.DataAnnotations;

namespace MusicHub.DataProcessor.ImportDtos
{
    public class ImportProducerAlbumDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        public string ReleaseDate { get; set; }
    }
}