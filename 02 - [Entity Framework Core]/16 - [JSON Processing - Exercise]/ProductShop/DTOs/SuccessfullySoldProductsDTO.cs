using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using ProductShop.Models;

namespace ProductShop.DTOs
{
    public class SuccessfullySoldProductsDTO
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("soldProducts")]
        public List<SuccessfullySoldProductsBuyerDTO> SoldProducts { get; set; }
            = new List<SuccessfullySoldProductsBuyerDTO>();
    }
}