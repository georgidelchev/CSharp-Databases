using System.Xml.Serialization;

namespace Instagraph.DataProcessor.DtoModels.Import
{
    public class ImportCommentPostDto
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
    }
}