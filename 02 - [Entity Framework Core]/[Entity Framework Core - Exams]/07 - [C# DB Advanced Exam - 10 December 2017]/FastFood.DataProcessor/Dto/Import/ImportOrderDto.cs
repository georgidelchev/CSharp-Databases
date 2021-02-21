using System.Xml.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FastFood.DataProcessor.Dto.Import
{
    [XmlType("Order")]
    public class ImportOrderDto
    {
        [Required]
        public string Customer { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Employee { get; set; }

        [Required]
        public string DateTime { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public List<ImportOrderItemDto> Items { get; set; }
    }
}