using System;
using System.IO;
using System.Linq;
using System.Text;
using Stations.Data;
using Stations.Models;
using Newtonsoft.Json;
using System.Globalization;
using Stations.Models.Enums;
using System.Xml.Serialization;
using System.Collections.Generic;
using Stations.DataProcessor.Dto.Import;
using System.ComponentModel.DataAnnotations;

namespace Stations.DataProcessor
{
    public static class Deserializer
    {
        private const string FailureMessage = "Invalid data format.";

        private const string SuccessMessage = "Record {0} successfully imported.";

        public static string ImportStations(StationsDbContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var serializer = JsonConvert.DeserializeObject<List<ImportStationDto>>(jsonString);

            var stationsToAdd = new List<Station>();

            foreach (var stationDto in serializer)
            {
                if (!IsValid(stationDto))
                {
                    sb.AppendLine(FailureMessage);

                    continue;
                }

                if (stationsToAdd.Any(s => s.Name == stationDto.Name))
                {
                    sb.AppendLine(FailureMessage);

                    continue;
                }

                var station = new Station()
                {
                    Name = stationDto.Name,
                    Town = stationDto.Town ?? stationDto.Name
                };

                stationsToAdd.Add(station);

                sb.AppendLine(string.Format(SuccessMessage, station.Name));
            }

            context.Stations.AddRange(stationsToAdd);

            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportClasses(StationsDbContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var serializer = JsonConvert.DeserializeObject<List<ImportSeatingClassDto>>(jsonString);

            var seatingClassesToAdd = new List<SeatingClass>();

            foreach (var seatingClassDto in serializer)
            {
                if (!IsValid(seatingClassDto))
                {
                    sb.AppendLine(FailureMessage);

                    continue;
                }

                if (seatingClassesToAdd.Any(sc => sc.Name == seatingClassDto.Name) ||
                    seatingClassesToAdd.Any(sc => sc.Abbreviation == seatingClassDto.Abbreviation))
                {
                    sb.AppendLine(FailureMessage);

                    continue;
                }

                var seatingClass = new SeatingClass()
                {
                    Abbreviation = seatingClassDto.Abbreviation,
                    Name = seatingClassDto.Name
                };

                seatingClassesToAdd.Add(seatingClass);

                sb.AppendLine(string.Format(SuccessMessage, seatingClass.Name));
            }

            context.SeatingClasses.AddRange(seatingClassesToAdd);

            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportTrains(StationsDbContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var serializer = JsonConvert.DeserializeObject<List<ImportTrainDto>>(jsonString);

            var trainsToAdd = new List<Train>();

            foreach (var trainDto in serializer)
            {
                if (!IsValid(trainDto) ||
                    !trainDto.Seats.All(IsValid))
                {
                    sb.AppendLine(FailureMessage);

                    continue;
                }

                if (trainsToAdd.Any(t => t.TrainNumber == trainDto.TrainNumber))
                {
                    sb.AppendLine(FailureMessage);

                    continue;
                }

                if (trainDto.Type == null)
                {
                    trainDto.Type = TrainType.HighSpeed;
                }

                if (!trainDto.Seats
                       .All(s => context.SeatingClasses
                                      .Any(sc => sc.Name == s.Name &&
                                                 sc.Abbreviation == s.Abbreviation)))
                {
                    sb.AppendLine(FailureMessage);

                    continue;
                }

                var trainSeats = trainDto.Seats
                    .Select(s => new TrainSeat()
                    {
                        SeatingClass = context.SeatingClasses.FirstOrDefault(sc =>
                            sc.Name == s.Name && sc.Abbreviation == s.Abbreviation),
                        Quantity = s.Quantity.Value
                    })
                    .ToList();

                var train = new Train()
                {
                    TrainNumber = trainDto.TrainNumber,
                    Type = trainDto.Type,
                    TrainSeats = trainSeats
                };

                trainsToAdd.Add(train);

                sb.AppendLine(string.Format(SuccessMessage, trainDto.TrainNumber));
            }

            context.Trains.AddRange(trainsToAdd);

            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportTrips(StationsDbContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var serializer = JsonConvert.DeserializeObject<List<ImportTripDto>>(jsonString);

            var tripsToAdd = new List<Trip>();

            foreach (var tripDto in serializer)
            {
                if (!IsValid(tripDto))
                {
                    sb.AppendLine(FailureMessage);

                    continue;
                }

                if (tripDto.Status == null)
                {
                    tripDto.Status = TripStatus.OnTime;
                }

                var train = context.Trains.FirstOrDefault(t => t.TrainNumber == tripDto.Train);

                var originStation = context.Stations.FirstOrDefault(s => s.Name == tripDto.OriginStation);

                var destinationStation = context.Stations.FirstOrDefault(s => s.Name == tripDto.DestinationStation);

                if (train == null ||
                    originStation == null ||
                    destinationStation == null)
                {
                    sb.AppendLine(FailureMessage);

                    continue;
                }

                DateTime departureDate;

                var isDepartureDateValid = DateTime.TryParseExact(tripDto.DepartureTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out departureDate);

                DateTime arrivalDate;

                var isArrivalDateValid = DateTime.TryParseExact(tripDto.ArrivalTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out arrivalDate);

                TimeSpan timeDifference;

                if (tripDto.TimeDifference != null)
                {
                    timeDifference = TimeSpan.ParseExact(tripDto.TimeDifference, @"hh\:mm", CultureInfo.InvariantCulture, TimeSpanStyles.None);
                }

                if (!isDepartureDateValid ||
                    !isArrivalDateValid ||
                    (arrivalDate <= departureDate))
                {
                    sb.AppendLine(FailureMessage);

                    continue;
                }

                var trip = new Trip()
                {
                    ArrivalTime = arrivalDate,
                    DepartureTime = departureDate,
                    DestinationStation = destinationStation,
                    OriginStation = originStation,
                    Status = tripDto.Status.Value,
                    TimeDifference = timeDifference,
                    Train = train
                };

                tripsToAdd.Add(trip);

                sb.AppendLine($"Trip from {originStation.Name} to {destinationStation.Name} imported.");
            }

            context.Trips.AddRange(tripsToAdd);

            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportCards(StationsDbContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(List<ImportPersonCardDto>), new XmlRootAttribute("Cards"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            var reader = new StringReader(xmlString);

            var personCardsToAdd = new List<CustomerCard>();

            using (reader)
            {
                var customerCardDtos = (List<ImportPersonCardDto>)serializer.Deserialize(reader);

                foreach (var customerCardDto in customerCardDtos)
                {
                    if (!IsValid(customerCardDto))
                    {
                        sb.AppendLine(FailureMessage);

                        continue;
                    }

                    var cardType = Enum.TryParse<CardType>(customerCardDto.CardType, out var card) ? card : CardType.Normal;

                    var customerCard = new CustomerCard()
                    {
                        Name = customerCardDto.Name,
                        Type = cardType,
                        Age = customerCardDto.Age
                    };

                    personCardsToAdd.Add(customerCard);

                    sb.AppendLine(string.Format(SuccessMessage, customerCard.Name));
                }

                context.CustomerCards.AddRange(personCardsToAdd);

                context.SaveChanges();

                return sb.ToString().Trim();
            }
        }

        public static string ImportTickets(StationsDbContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(List<ImportTicketDto>), new XmlRootAttribute("Tickets"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            var reader = new StringReader(xmlString);

            var ticketsToAdd = new List<Ticket>();

            using (reader)
            {
                var ticketDtos = (List<ImportTicketDto>)serializer.Deserialize(reader);

                foreach (var ticketDto in ticketDtos)
                {
                    if (!IsValid(ticketDto))
                    {
                        sb.AppendLine(FailureMessage);

                        continue;
                    }

                    var departureDate = DateTime.ParseExact(ticketDto.Trip.DepartureTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None);

                    var trip = context.Trips.FirstOrDefault(t => t.OriginStation.Name == ticketDto.Trip.OriginStation && t.DepartureTime == departureDate && t.DestinationStation.Name == ticketDto.Trip.DestinationStation);

                    if (trip == null)
                    {
                        sb.AppendLine(FailureMessage);

                        continue;
                    }

                    CustomerCard card = null;

                    if (ticketDto.Card != null)
                    {
                        card = context.CustomerCards
                            .FirstOrDefault(c => c.Name == ticketDto.Card.Name);

                        if (card == null)
                        {
                            sb.AppendLine(FailureMessage);

                            continue;
                        }
                    }

                    var seatinClassAbbreviation = ticketDto.Seat
                        .Substring(0, 2);

                    var quantity = int.Parse(ticketDto.Seat.Substring(2));

                    var seat = trip.Train
                        .TrainSeats
                        .FirstOrDefault(ts => ts.SeatingClass.Abbreviation == seatinClassAbbreviation && 
                                              quantity <= ts.Quantity);

                    if (seat == null)
                    {
                        sb.AppendLine(FailureMessage);

                        continue;
                    }

                    var ticket = new Ticket()
                    {
                        Trip = trip,
                        CustomerCard = card,
                        Price = ticketDto.Price,
                        SeatingPlace = ticketDto.Seat
                    };

                    ticketsToAdd.Add(ticket);

                    sb.AppendLine($"Ticket from {trip.OriginStation.Name} to {trip.DestinationStation.Name} departing at {departureDate.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)} imported.");
                }

                context.Tickets.AddRange(ticketsToAdd);

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