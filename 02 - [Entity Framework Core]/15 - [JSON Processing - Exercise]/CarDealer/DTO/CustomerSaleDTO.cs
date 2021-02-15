using CarDealer.Models;
using Newtonsoft.Json;

namespace CarDealer.DTO
{
    public class CustomerSaleDTO
    {
        [JsonProperty("car")]
        public SaleDTO Car { get; set; }

        [JsonProperty("customerName")]
        public string CustomerName { get; set; }

        public string Discount { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("priceWithDiscount")]
        public string PriceWithDiscount { get; set; }
    }
}