using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Instagraph.DataProcessor.DtoModels.Import
{
    [XmlType("comment")]
    public class ImportCommentDto
    {
        [Required]
        [MaxLength(250)]
        [XmlElement("content")]
        public string Content { get; set; }

        [Required]
        [XmlElement("user")]
        public string User { get; set; }

        [Required]
        [XmlElement("post")]
        public ImportCommentPostDto Post { get; set; }
    }
}