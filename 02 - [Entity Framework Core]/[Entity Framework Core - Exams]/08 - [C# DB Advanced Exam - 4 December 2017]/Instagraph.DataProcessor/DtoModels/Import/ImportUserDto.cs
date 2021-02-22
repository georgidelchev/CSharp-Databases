using System.ComponentModel.DataAnnotations;

namespace Instagraph.DataProcessor.DtoModels.Import
{
    public class ImportUserDto
    {
        [Required]
        [MaxLength(30)]
        public string Username { get; set; }

        [Required]
        [MaxLength(20)]
        public string Password { get; set; }

        [Required]
        public string ProfilePicture { get; set; }
    }
}