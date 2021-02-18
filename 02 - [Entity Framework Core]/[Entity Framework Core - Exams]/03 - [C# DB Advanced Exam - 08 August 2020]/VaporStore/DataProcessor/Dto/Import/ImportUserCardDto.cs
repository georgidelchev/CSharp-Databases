using Newtonsoft.Json;
using VaporStore.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace VaporStore.DataProcessor.Dto.Import
{
    public class ImportUserCardDto
    {
        [Required]
        [RegularExpression(@"^\d{4} \d{4} \d{4} \d{4}$")]
        public string Number { get; set; }

        [Required]
        [JsonProperty("CVC")]
        [RegularExpression(@"^\d{3}$")]
        public string Cvc { get; set; }

        [Required]
        [Range(0, 1)]
        public CardType Type { get; set; }
    }
}