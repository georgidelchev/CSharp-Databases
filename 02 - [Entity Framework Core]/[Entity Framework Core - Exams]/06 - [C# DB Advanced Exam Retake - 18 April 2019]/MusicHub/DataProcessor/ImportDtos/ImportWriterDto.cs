using System.ComponentModel.DataAnnotations;

namespace MusicHub.DataProcessor.ImportDtos
{
    public class ImportWriterDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Name { get; set; }

        [RegularExpression(@"^[A-Z][a-z]+ [A-Z][a-z]+$")]
        public string Pseudonym { get; set; }
    }
}