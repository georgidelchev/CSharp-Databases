using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace BookShop.DataProcessor.ImportDto
{
    public class ImportAuthorDto
    {
        [JsonProperty("FirstName")]
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [JsonProperty("LastName")]
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string LastName { get; set; }

        [JsonProperty("Phone")]
        [Required]
        [RegularExpression(@"^[0-9]{3}-[0-9]{3}-[0-9]{4}$")]
        public string Phone { get; set; }

        [JsonProperty("Email")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public List<ImportAuthorBookDto> Books { get; set; }
    }
}