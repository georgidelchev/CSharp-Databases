using System.ComponentModel.DataAnnotations;

using static MyCoolCarSystem.Data.DataValidations.Address;

namespace MyCoolCarSystem.Data.Models
{
    public class Address
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(MAX_TEXT_LENGTH)]
        public string Text { get; set; }

        [Required]
        [MaxLength(MAX_TOWN_LENGTH)]
        public string Town { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
    }
}