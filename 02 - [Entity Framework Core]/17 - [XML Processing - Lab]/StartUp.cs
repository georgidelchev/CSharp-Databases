using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace XMLProcessingLabPractice
{
    public class StartUp
    {
        [SuppressMessage("ReSharper.DPA", "DPA0002: Excessive memory allocations in SOH", MessageId = "type: System.String")]
        public static void Main(string[] args)
        {
            var xml = File.ReadAllText("Planes.xml");

            // First variant
            //var xmlDocument = XDocument.Parse(xml);

            // Second variant
            // var xmlDocument2 = XDocument.Load("Planes.xml");

            // GetEncoding(xmlDocument);

            // GetVersion(xmlDocument);

            // GetElementCount(xmlDocument);

            // GetFirstValueWithCriteria(xmlDocument);

            // GetFirstValueYearWithCriteria(xmlDocument);

            Console.OutputEncoding = Encoding.UTF8;

            var xmlDocument = XDocument.Load("bgwiki_updated.xml");

            // XmlDemo(xmlDocument);

            var xmlSerializer = new XmlSerializer(typeof(Article[]), new XmlRootAttribute("feed"));

            // DeserializeObjectXml(xmlSerializer);

            var docs = new List<Article>()
            {
                new Article
                {
                    Title = "Bulgaria",
                    Abstract = "Country",
                    Url = "wikipedia"
                },
                new Article
                {
                    Title = "SoftUni",
                    Abstract = "School",
                    Url = "wikipedia"
                },
            };

            var xmlSerializer1 = new XmlSerializer(typeof(List<Article>),new XmlRootAttribute("feed"));

            xmlSerializer1.Serialize(File.OpenWrite("myNewDocs.xml"), docs);
        }

        private static void DeserializeObjectXml(XmlSerializer xmlSerializer)
        {
            var docs = (Article[])xmlSerializer.Deserialize(File.OpenRead("bgwiki_updated.xml"));

            var articles = docs.Where(x => x.Title.Contains("Ставри"))
                .OrderBy(x => x.Title);

            foreach (var article in articles)
            {
                Console.WriteLine(article.Title);
            }
        }

        private static void XmlDemo(XDocument xmlDocument)
        {
            var articles = xmlDocument
                            .Root
                            .Elements()
                            .Select(x => new
                            {
                                Title = x.Element("title").Value,
                                Description = x.Element("abstract").Value,
                                Url = x.Element("url").Value
                            })
                            .Where(x => x.Title.Contains("Ставри"))
                            .OrderBy(x => x.Title);

            foreach (var article in articles)
            {
                Console.WriteLine(article.Title);
            }

            xmlDocument.Save("bgwiki_updated.xml");
        }

        private static void GetFirstValueYearWithCriteria(XDocument xmlDocument)
        {
            Console.WriteLine(xmlDocument.Root.Elements().FirstOrDefault(e => e.Element("color").Value == "Blue").Element("year").Value);
        }

        private static void GetFirstValueWithCriteria(XDocument xmlDocument)
        {
            Console.WriteLine(xmlDocument.Root.Elements().FirstOrDefault(e => e.Element("color").Value == "Blue"));
        }

        private static void GetElementCount(XDocument xmlDocument)
        {
            Console.WriteLine(xmlDocument.Root.Elements().Count());
        }

        private static void GetVersion(XDocument xmlDocument)
        {
            Console.WriteLine(xmlDocument.Declaration.Version);
        }

        private static void GetEncoding(XDocument xmlDocument)
        {
            Console.WriteLine(xmlDocument.Declaration.Encoding);
        }
    }
}
