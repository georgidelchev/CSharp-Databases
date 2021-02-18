using System;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using VaporStore.Data;
using System.Globalization;
using System.Xml.Serialization;
using System.Collections.Generic;
using VaporStore.Data.Models.Enums;
using VaporStore.DataProcessor.Dto.Export;

namespace VaporStore.DataProcessor
{
    public static class Serializer
    {
        public static string ExportGamesByGenres(VaporStoreDbContext context, string[] genreNames)
        {
            var genres = context
                .Genres
                .ToList()
                .Where(g => genreNames.Contains(g.Name))
                .Select(g => new
                {
                    Id = g.Id,
                    Genre = g.Name,
                    Games = g.Games
                        .Where(gm => gm.Purchases.Count >= 1)
                        .Select(gm => new
                        {
                            Id = gm.Id,
                            Title = gm.Name,
                            Developer = gm.Developer.Name,
                            Tags = string.Join(", ", gm.GameTags.Select(gt => gt.Tag.Name)),
                            Players = gm.Purchases.Count
                        })
                        .OrderByDescending(gm => gm.Players)
                        .ThenBy(gm => gm.Id)
                        .ToList(),
                    TotalPlayers = g.Games.Sum(gm => gm.Purchases.Count)
                })
                .OrderByDescending(g => g.TotalPlayers)
                .ThenBy(g => g.Id)
                .ToList();

            var json = JsonConvert.SerializeObject(genres, Formatting.Indented);

            return json;
        }

        public static string ExportUserPurchasesByType(VaporStoreDbContext context, string storeType)
        {
            var sb = new StringBuilder();

            var users = context
                .Users
                .ToList()
                .Where(u => u.Cards.Any(c => c.Purchases.Any()))
                .Select(u => new ExportUserPurchaseDto()
                {
                    Username = u.Username,
                    Purchases = context
                        .Purchases
                        .ToList()
                        .Where(p => p.Card.User.Username == u.Username &&
                                    p.Type == Enum.Parse<PurchaseType>(storeType))
                        .Select(p => new ExportPurchaseDto()
                        {
                            Game = new ExportPurchaseGameDto()
                            {
                                Genre = p.Game.Genre.Name,
                                Price = p.Game.Price,
                                Title = p.Game.Name
                            },
                            Card = p.Card.Number,
                            Date = p.Date.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                            Cvc = p.Card.Cvc,
                        })
                        .OrderBy(p => p.Date)
                        .ToList(),
                    TotalSpent = context
                        .Purchases
                        .ToList()
                        .Where(p => p.Card.User.Username == u.Username &&
                                    p.Type == Enum.Parse<PurchaseType>(storeType))
                        .Sum(p => p.Game.Price)
                })
                .ToList()
                .Where(u => u.Purchases.Count >= 1)
                .OrderByDescending(u => u.TotalSpent)
                .ThenBy(u => u.Username)
                .ToList();

            var serializer = new XmlSerializer(typeof(List<ExportUserPurchaseDto>), new XmlRootAttribute("Users"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            var writer = new StringWriter(sb);

            using (writer)
            {
                serializer.Serialize(writer, users, namespaces);
            }

            return sb.ToString().Trim();
        }
    }
}