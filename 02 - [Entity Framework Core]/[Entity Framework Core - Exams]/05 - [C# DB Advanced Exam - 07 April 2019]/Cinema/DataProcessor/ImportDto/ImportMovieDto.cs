using Cinema.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Cinema.DataProcessor.ImportDto
{
    public class ImportMovieDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Title { get; set; }

        [Required]
        [Range(0, 9)]
        public Genre Genre { get; set; }

        [Required]
        public string Duration { get; set; }

        [Required]
        [Range(1.00, 10.00)]
        public double Rating { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Director { get; set; }
    }
}