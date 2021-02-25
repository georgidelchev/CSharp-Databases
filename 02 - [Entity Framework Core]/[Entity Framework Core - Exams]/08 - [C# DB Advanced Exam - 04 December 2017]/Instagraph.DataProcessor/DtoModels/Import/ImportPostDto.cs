using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Instagraph.DataProcessor.DtoModels.Import
{
    [XmlType("post")]
    public class ImportPostDto
    {
        [Required]
        [XmlElement("caption")]
        public string Caption { get; set; }

        [Required]
        [XmlElement("user")]
        public string User { get; set; }

        [Required]
        [XmlElement("picture")]
        public string Picture { get; set; }
    }
}