using System.ComponentModel.DataAnnotations;

namespace Cinema.DataProcessor.ImportDto
{
    public class ImportHallDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Name { get; set; }

        public bool Is4Dx { get; set; }

        public bool Is3D { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Seats { get; set; }
    }
}