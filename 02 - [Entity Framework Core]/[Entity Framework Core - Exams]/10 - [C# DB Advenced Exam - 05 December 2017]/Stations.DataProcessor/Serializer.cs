using System;
using System.IO;
using System.Text;
using System.Linq;
using Stations.Data;
using Newtonsoft.Json;
using System.Globalization;
using Stations.Models.Enums;
using System.Xml.Serialization;
using System.Collections.Generic;
using Stations.DataProcessor.Dto.Export;

namespace Stations.DataProcessor
{
    public class Serializer
    {
        public static string ExportDelayedTrains(StationsDbContext context, string dateAsString)
        {
            var trains = context
                .Trains
                .Where(t => t.Trips.Any(tr =>
                    tr.Status == TripStatus.Delayed && tr.DepartureTime <=
                    DateTime.ParseExact(dateAsString, "dd/MM/yyyy", CultureInfo.InvariantCulture)))
                .Select(t => new
                {
                    TrainNumber = t.TrainNumber,
                    DelayedTimes = t.Trips.Count(tr =>
                        tr.Status == TripStatus.Delayed && tr.DepartureTime <=
                        DateTime.ParseExact(dateAsString, "dd/MM/yyyy", CultureInfo.InvariantCulture)),
                    MaxDelayedTime = t.Trips.Max(tr => tr.TimeDifference)
                })
                .OrderByDescending(t => t.DelayedTimes)
                .ThenByDescending(t => t.MaxDelayedTime)
                .ThenBy(t => t.TrainNumber)
                .ToList();

            var json = JsonConvert.SerializeObject(trains, Formatting.Indented);

            return json;
        }

        public static string ExportCardsTicket(StationsDbContext context, string cardType)
        {
            var sb = new StringBuilder();

            var cards = context
                .CustomerCards
                .Where(cc => cc.Type.ToString() == cardType&&
                             cc.BoughtTickets.Count>=1)
                .Select(cc => new ExportCardDto()
                {
                    Name = cc.Name,
                    Type = cc.Type.ToString(),
                    Tickets = cc.BoughtTickets
                        .Select(bt => new ExportCardTicketDto()
                    {
                        DepartureTime = bt.Trip
                            .DepartureTime
                            .ToString("dd/MM/yyyy HH:mm",CultureInfo.InvariantCulture),
                        DestinationStation = bt.Trip.DestinationStation.Name,
                        OriginStation = bt.Trip.OriginStation.Name
                    })
                        .ToList()
                })
                .OrderBy(cc => cc.Name)
                .ToList();

            var serializer = new XmlSerializer(typeof(List<ExportCardDto>), new XmlRootAttribute("Cards"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            var writer = new StringWriter(sb);

            using (writer)
            {
                serializer.Serialize(writer, cards, namespaces);
            }

            return sb.ToString().Trim();
        }
    }
}