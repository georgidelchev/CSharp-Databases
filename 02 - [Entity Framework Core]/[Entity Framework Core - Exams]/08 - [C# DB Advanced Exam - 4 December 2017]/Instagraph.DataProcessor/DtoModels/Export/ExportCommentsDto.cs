using System.Xml.Serialization;

namespace Instagraph.DataProcessor.DtoModels.Export
{
    [XmlType("user")]
    public class ExportCommentsDto
    {
        public string Username { get; set; }

        public int MostComments { get; set; }
    }
}