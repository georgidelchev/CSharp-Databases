using System;
using FastFood.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using FastFood.DataProcessor.Dto.Import;
using FastFood.Models;
using FastFood.Models.Enums;
using Newtonsoft.Json;

namespace FastFood.DataProcessor
{
    public static class Deserializer
    {
        private const string FailureMessage = "Invalid data format.";
        private const string SuccessMessage = "Record {0} successfully imported.";

        public static string ImportEmployees(FastFoodDbContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var serializer = JsonConvert.DeserializeObject<List<ImportEmployeeDto>>(jsonString);

            var employeesToAdd = new List<Employee>();
            var positions = new List<Position>();

            foreach (var employeeDto in serializer)
            {
                if (!IsValid(employeeDto))
                {
                    sb.AppendLine(FailureMessage);

                    continue;
                }

                var position = positions.FirstOrDefault(p => p.Name == employeeDto.Position);

                if (position == null)
                {
                    position = new Position()
                    {
                        Name = employeeDto.Position
                    };

                    positions.Add(position);
                }

                var employee = new Employee()
                {
                    Name = employeeDto.Name,
                    Position = position,
                    Age = employeeDto.Age
                };

                employeesToAdd.Add(employee);

                sb.AppendLine(string.Format(SuccessMessage, employee.Name));
            }

            context.Employees.AddRange(employeesToAdd);

            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportItems(FastFoodDbContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var serializer = JsonConvert.DeserializeObject<List<ImportItemDto>>(jsonString);

            var itemsToAdd = new List<Item>();
            var categories = new List<Category>();

            foreach (var itemDto in serializer)
            {
                if (!IsValid(itemDto))
                {
                    sb.AppendLine(FailureMessage);

                    continue;
                }

                var category = categories.FirstOrDefault(c => c.Name == itemDto.Category);

                if (category == null)
                {
                    category = new Category()
                    {
                        Name = itemDto.Category
                    };

                    categories.Add(category);
                }

                if (itemsToAdd.Any(i => i.Name == itemDto.Name))
                {
                    sb.AppendLine(FailureMessage);

                    continue;
                }

                var item = new Item()
                {
                    Name = itemDto.Name,
                    Price = itemDto.Price,
                    Category = category,
                    CategoryId = category.Id
                };

                itemsToAdd.Add(item);

                sb.AppendLine(string.Format(SuccessMessage, item.Name));
            }

            context.Items.AddRange(itemsToAdd);

            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportOrders(FastFoodDbContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(List<ImportOrderDto>), new XmlRootAttribute("Orders"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            var ordersToAdd = new List<Order>();

            var reader = new StringReader(xmlString);

            using (reader)
            {
                var orderDtos = (List<ImportOrderDto>)serializer.Deserialize(reader);

                foreach (var orderDto in orderDtos)
                {
                    if (!IsValid(orderDto))
                    {
                        sb.AppendLine(FailureMessage);

                        continue;
                    }

                    var isTypeValid = Enum.IsDefined(typeof(OrderType), orderDto.Type);

                    var employee = context.Employees.FirstOrDefault(e => e.Name == orderDto.Employee);


                    if (employee == null || isTypeValid == false)
                    {
                        sb.AppendLine(FailureMessage);

                        continue;
                    }

                    var order = new Order()
                    {
                        Customer = orderDto.Customer,
                        Employee = employee,
                        Type = Enum.Parse<OrderType>(orderDto.Type),
                        DateTime = DateTime.ParseExact(orderDto.DateTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                        OrderItems = orderDto.Items.Select(i => new OrderItem()
                        {
                            Item = context.Items.FirstOrDefault(it => it.Name == i.Name),
                            Quantity = i.Quantity
                        })
                            .ToList()
                    };

                    ordersToAdd.Add(order);

                    sb.AppendLine($"Order for {order.Customer} on {order.DateTime:dd/MM/yyyy HH:mm} added");
                }

                context.Orders.AddRange(ordersToAdd);

                context.SaveChanges();

                return sb.ToString().Trim();
            }
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResult = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResult, true);
            return isValid;
        }
    }
}