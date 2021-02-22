using System.ComponentModel.DataAnnotations;

namespace Instagraph.DataProcessor.DtoModels.Import
{
    public class ImportPictureDto
    {
        [Required]
        public string Path { get; set; }

        [Required]
        [Range(typeof(decimal), "1", "79228162514264337593543950335")]
        public decimal Size { get; set; }
    }
}