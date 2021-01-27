using System.Xml.Serialization;

namespace XMLProcessingLabPractice
{
    [XmlType("doc")]
    public class Article
    {
        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("abstract")]
        public string Abstract { get; set; }

        [XmlIgnore]
        public string Url { get; set; }
    }
}