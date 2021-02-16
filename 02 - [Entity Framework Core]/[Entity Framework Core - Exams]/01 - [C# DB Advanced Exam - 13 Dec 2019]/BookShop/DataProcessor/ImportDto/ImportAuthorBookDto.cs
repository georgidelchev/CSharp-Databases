using Newtonsoft.Json;

namespace BookShop.DataProcessor.ImportDto
{
    public class ImportAuthorBookDto
    {
        [JsonProperty("Id")]
        public int? BookId { get; set; }
    }
}