using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Cinema.Data;
using Cinema.Data.Models;
using Cinema.DataProcessor.ImportDto;
using Newtonsoft.Json;

namespace Cinema.DataProcessor
{
    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfulImportMovie
            = "Successfully imported {0} with genre {1} and rating {2}!";

        private const string SuccessfulImportHallSeat
            = "Successfully imported {0}({1}) with {2} seats!";

        private const string SuccessfulImportProjection
            = "Successfully imported projection {0} on {1}!";

        private const string SuccessfulImportCustomerTicket
            = "Successfully imported customer {0} {1} with bought tickets: {2}!";

        public static string ImportMovies(CinemaContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var serializer = JsonConvert.DeserializeObject<List<ImportMovieDto>>(jsonString);

            var moviesToAdd = new List<Movie>();

            foreach (var movieDto in serializer)
            {
                if (!IsValid(movieDto))
                {
                    sb.AppendLine(ErrorMessage);

                    continue;
                }

                var movie = new Movie()
                {
                    Director = movieDto.Director,
                    Duration = TimeSpan.ParseExact(movieDto.Duration, @"hh\:mm\:ss", CultureInfo.InvariantCulture),
                    Genre = movieDto.Genre,
                    Rating = movieDto.Rating,
                    Title = movieDto.Title,
                };

                moviesToAdd.Add(movie);

                sb.AppendLine(string.Format(SuccessfulImportMovie, movie.Title, movie.Genre,
                    movie.Rating.ToString("f2")));
            }

            context.Movies.AddRange(moviesToAdd);

            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportHallSeats(CinemaContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var serializer = JsonConvert.DeserializeObject<List<ImportHallDto>>(jsonString);

            var hallsToAdd = new List<Hall>();

            foreach (var hallDto in serializer)
            {
                if (!IsValid(hallDto))
                {
                    sb.AppendLine(ErrorMessage);

                    continue;
                }

                var hall = new Hall()
                {
                    Is3D = hallDto.Is3D,
                    Is4Dx = hallDto.Is4Dx,
                    Name = hallDto.Name,
                };

                for (var i = 0; i < hallDto.Seats; i++)
                {
                    hall.Seats.Add(new Seat());
                }

                var projType = "";

                if (hall.Is3D && hall.Is4Dx)
                {
                    projType = "4Dx/3D";
                }
                else if (hall.Is3D)
                {
                    projType = "3D";

                }
                else if (hall.Is4Dx)
                {
                    projType = "4Dx";
                }
                else
                {
                    projType = "Normal";
                }

                hallsToAdd.Add(hall);

                sb.AppendLine(string.Format(SuccessfulImportHallSeat, hall.Name, projType, hall.Seats.Count));
            }

            context.Halls.AddRange(hallsToAdd);

            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportProjections(CinemaContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(List<ImportProjectionDto>), new XmlRootAttribute("Projections"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            var reader = new StringReader(xmlString);

            using (reader)
            {
                var projectionDtos = (List<ImportProjectionDto>)serializer.Deserialize(reader);

                var projectionsToAdd = new List<Projection>();

                foreach (var projectionDto in projectionDtos)
                {
                    if (!IsValid(projectionDto))
                    {
                        sb.AppendLine(ErrorMessage);

                        continue;
                    }

                    var hall = context.Halls.FirstOrDefault(h => h.Id == projectionDto.HallId);

                    if (hall == null)
                    {
                        sb.AppendLine(ErrorMessage);

                        continue;
                    }

                    var movie = context.Movies.FirstOrDefault(m => m.Id == projectionDto.MovieId);

                    if (movie == null)
                    {
                        sb.AppendLine(ErrorMessage);

                        continue;
                    }

                    DateTime dt;

                    var isDateTimeValid = DateTime.TryParseExact(projectionDto.DateTime,
                        "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);

                    if (!isDateTimeValid)
                    {
                        sb.AppendLine(ErrorMessage);

                        continue;
                    }

                    var projection = new Projection()
                    {
                        Hall = hall,
                        Movie = movie,
                        DateTime = dt,
                        HallId = projectionDto.HallId,
                        MovieId = projectionDto.MovieId
                    };

                    projectionsToAdd.Add(projection);

                    sb.AppendLine(
                        string.Format(SuccessfulImportProjection, projection.Movie.Title, projection.DateTime.ToString("MM/dd/yyyy")));
                }

                context.Projections.AddRange(projectionsToAdd);

                context.SaveChanges();

                return sb.ToString().Trim();
            }
        }

        public static string ImportCustomerTickets(CinemaContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(List<ImportCustomerDto>), new XmlRootAttribute("Customers"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            var reader = new StringReader(xmlString);

            using (reader)
            {
                var customerDtos = (List<ImportCustomerDto>)serializer.Deserialize(reader);

                var customersToAdd = new List<Customer>();

                foreach (var customerDto in customerDtos)
                {
                    if (!IsValid(customerDto))
                    {
                        sb.AppendLine(ErrorMessage);

                        continue;
                    }

                    var customer = new Customer()
                    {
                        Age = customerDto.Age,
                        Balance = customerDto.Balance,
                        FirstName = customerDto.FirstName,
                        LastName = customerDto.LastName
                    };

                    foreach (var ticketDto in customerDto.Tickets)
                    {
                        if (!IsValid(ticketDto))
                        {
                            sb.AppendLine();

                            break;
                        }

                        var projection = context.Projections.FirstOrDefault(p => p.Id == ticketDto.ProjectionId);

                        if (projection == null)
                        {
                            sb.AppendLine(ErrorMessage);

                            continue;
                        }

                        var ticket = new Ticket()
                        {
                            Customer = customer,
                            Price = ticketDto.Price,
                            ProjectionId = ticketDto.ProjectionId,
                            CustomerId = customer.Id,
                            Projection = projection,
                        };

                        customer.Tickets.Add(ticket);
                    }

                    customersToAdd.Add(customer);

                    sb.AppendLine(string.Format(SuccessfulImportCustomerTicket, customer.FirstName,
                        customer.LastName, customer.Tickets.Count));
                }

                context.Customers.AddRange(customersToAdd);

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
