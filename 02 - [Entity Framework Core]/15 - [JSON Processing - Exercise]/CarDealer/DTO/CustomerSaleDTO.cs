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

        public decimal Discount { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("priceWithDiscount")]
        public decimal PriceWithDiscount { get; set; }
    }
}