using System.Xml.Serialization;

namespace FastFood.DataProcessor.Dto.Export
{
    [XmlType("Category")]
    public class ExportCategoryDto
    {
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "MostPopularItem")]
        public ExportMostPopularCategoryItemDto MostPopularItem { get; set; }
    }
}