using System;
using System.IO;
using System.Linq;
using System.Text;
using FastFood.Data;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.Collections.Generic;
using FastFood.DataProcessor.Dto.Export;

namespace FastFood.DataProcessor
{
    public class Serializer
    {
        public static string ExportOrdersByEmployee(FastFoodDbContext context, string employeeName, string orderType)
        {
            var employees = context
                .Employees
                .Where(e => e.Name == employeeName)
                .Select(e => new
                {
                    Name = e.Name,
                    Orders = e.Orders
                        .Where(o => o.Type.ToString() == orderType)
                        .Select(o => new
                        {
                            Customer = o.Customer,
                            Items = o.OrderItems
                                .Select(oi => new
                                {
                                    Name = oi.Item.Name,
                                    Price = decimal.Parse($"{oi.Item.Price:f2}"),
                                    Quantity = oi.Quantity
                                })
                                .ToList(),
                            TotalPrice = decimal.Parse($"{o.TotalPrice:f2}")
                        })
                        .OrderByDescending(o => o.TotalPrice)
                        .ThenByDescending(o => o.Items.Count)
                        .ToList(),
                    TotalMade = e.Orders
                        .Sum(o => decimal.Parse($"{o.TotalPrice:f2}"))
                })
                .ToList();

            var json = JsonConvert.SerializeObject(employees, Formatting.Indented);

            return json;
        }

        public static string ExportCategoryStatistics(FastFoodDbContext context, string categoriesString)
        {
            var sb = new StringBuilder();

            var categoriesList = categoriesString
                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            var categories = context
                .Categories
                .ToList()
                .Where(c => categoriesList.Contains(c.Name))
                .Select(c => new ExportCategoryDto()
                {
                    Name = c.Name,
                    MostPopularItem = c.Items.Select(i => new ExportMostPopularCategoryItemDto()
                    {
                        Name = i.Name,
                        TotalMade = i.OrderItems.Sum(oi => oi.Quantity) * decimal.Parse($"{i.Price:f2}"),
                        TimesSold = i.OrderItems.Sum(oi => oi.Quantity)
                    })
                        .OrderByDescending(i => i.TotalMade)
                        .FirstOrDefault()
                })
                .OrderByDescending(i => i.MostPopularItem.TotalMade)
                .ThenByDescending(i => i.MostPopularItem.TimesSold)
                .ToList();

            var serializer = new XmlSerializer(typeof(List<ExportCategoryDto>), new XmlRootAttribute("Categories"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            var writer = new StringWriter(sb);

            using (writer)
            {
                serializer.Serialize(writer, categories, namespaces);
            }

            return sb.ToString().Trim();
        }
    }
}