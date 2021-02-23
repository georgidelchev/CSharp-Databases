using System.ComponentModel.DataAnnotations;

namespace PetClinic.DataProcessor.Dto.Import
{
    public class ImportAnimalPassportDto
    {
        [Required]
        [RegularExpression(@"^(\w{7}\d{3})$")]
        public string SerialNumber { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string OwnerName { get; set; }

        [Required]
        [RegularExpression(@"^(\+359|0)\d{9}")]
        public string OwnerPhoneNumber { get; set; }

        [Required]
        public string RegistrationDate { get; set; }
    }
}