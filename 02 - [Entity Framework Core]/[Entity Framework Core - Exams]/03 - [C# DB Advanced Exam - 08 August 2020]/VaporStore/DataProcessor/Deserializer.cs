using System;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using VaporStore.Data;
using System.Globalization;
using VaporStore.Data.Models;
using System.Xml.Serialization;
using System.Collections.Generic;
using VaporStore.DataProcessor.Dto.Import;
using System.ComponentModel.DataAnnotations;

namespace VaporStore.DataProcessor
{
    public static class Deserializer
    {
        private const string ERROR_MESSAGE = "Invalid Data";

        private const string SUCCESSFULLY_ADDED_GAME = "Added {0} ({1}) with {2} tags";

        private const string SUCCESSFULLY_ADDED_USER = "Imported {0} with {1} cards";

        private const string SUCCESSFULLY_ADDED_PURCHASE = "Imported {0} for {1}";

        public static string ImportGames(VaporStoreDbContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var gameDtos = JsonConvert.DeserializeObject<List<ImportGameDto>>(jsonString);

            var games = new List<Game>();

            var developers = new List<Developer>();
            var genres = new List<Genre>();
            var tags = new List<Tag>();

            foreach (var game in gameDtos)
            {
                if (!IsValid(game))
                {
                    sb.AppendLine(ERROR_MESSAGE);

                    continue;
                }

                DateTime releaseDate;

                var isReleaseDateValid = DateTime.TryParseExact(game.ReleaseDate, "yyyy-MM-dd",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out releaseDate);

                if (!isReleaseDateValid)
                {
                    sb.AppendLine(ERROR_MESSAGE);

                    continue;
                }

                var gameToAdd = new Game()
                {
                    Name = game.Name,
                    ReleaseDate = releaseDate,
                    Price = game.Price
                };

                var developer = developers.FirstOrDefault(d => d.Name == game.Developer) ??
                new Developer()
                {
                    Name = game.Developer
                };

                developers.Add(developer);

                gameToAdd.Developer = developer;

                var genre = genres.FirstOrDefault(g => g.Name == game.Genre) ??
                new Genre()
                {
                    Name = game.Genre
                };

                genres.Add(genre);

                gameToAdd.Genre = genre;

                foreach (var tag in game.Tags)
                {
                    var tagToAdd = tags.FirstOrDefault(t => t.Name == tag) ??
                    new Tag()
                    {
                        Name = tag
                    };

                    tags.Add(tagToAdd);

                    gameToAdd.GameTags.Add(new GameTag()
                    {
                        Game = gameToAdd,
                        Tag = tagToAdd
                    });
                }

                if (gameToAdd.GameTags.Count == 0)
                {
                    sb.AppendLine(ERROR_MESSAGE);

                    continue;
                }

                games.Add(gameToAdd);

                sb.AppendLine(string.Format(SUCCESSFULLY_ADDED_GAME, gameToAdd.Name, gameToAdd.Genre.Name,
                    gameToAdd.GameTags.Count));
            }

            context.Games.AddRange(games);

            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportUsers(VaporStoreDbContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var userDtos = JsonConvert.DeserializeObject<List<ImportUserDto>>(jsonString);

            var usersToAdd = new List<User>();

            foreach (var user in userDtos)
            {
                if (!IsValid(user))
                {
                    sb.AppendLine(ERROR_MESSAGE);

                    continue;
                }

                var userToAdd = new User()
                {
                    FullName = user.FullName,
                    Username = user.UserName,
                    Email = user.Email,
                    Age = user.Age
                };

                foreach (var card in user.Cards)
                {
                    if (!IsValid(card))
                    {
                        sb.AppendLine(ERROR_MESSAGE);

                        continue;
                    }

                    var cardToAdd = new Card()
                    {
                        Number = card.Number,
                        Cvc = card.Cvc,
                        Type = card.Type,
                        User = userToAdd
                    };

                    userToAdd.Cards.Add(cardToAdd);
                }

                usersToAdd.Add(userToAdd);

                sb.AppendLine(string.Format(SUCCESSFULLY_ADDED_USER, userToAdd.Username, userToAdd.Cards.Count));
            }

            context.Users.AddRange(usersToAdd);

            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(List<ImportPurchaseDto>), new XmlRootAttribute("Purchases"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            var reader = new StringReader(xmlString);

            using (reader)
            {
                var purchaseDtos = (List<ImportPurchaseDto>)serializer.Deserialize(reader);

                var purchasesToAdd = new List<Purchase>();

                foreach (var purchase in purchaseDtos)
                {
                    if (!IsValid(purchase))
                    {
                        sb.AppendLine(ERROR_MESSAGE);

                        continue;
                    }

                    DateTime purchaseDate;

                    var isPurchaseDateValid = DateTime.TryParseExact(purchase.Date, "dd/MM/yyyy HH:mm",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out purchaseDate);

                    if (!isPurchaseDateValid)
                    {
                        sb.AppendLine(ERROR_MESSAGE);

                        continue;
                    }

                    var game = context.Games.FirstOrDefault(g => g.Name == purchase.GameTitle);

                    if (game == null)
                    {
                        sb.AppendLine(ERROR_MESSAGE);
                    }

                    var card = context.Cards.FirstOrDefault(c => c.Number == purchase.CardNumber);

                    if (card == null)
                    {
                        sb.AppendLine(ERROR_MESSAGE);
                    }

                    var purchaseToAdd = new Purchase()
                    {
                        Game = game,
                        Type = purchase.PurchaseType,
                        Date = purchaseDate,
                        ProductKey = purchase.ProductKey,
                        Card = card
                    };

                    purchasesToAdd.Add(purchaseToAdd);

                    sb.AppendLine(string.Format(SUCCESSFULLY_ADDED_PURCHASE, game.Name, purchaseToAdd.Card.User.Username));
                }

                context.Purchases.AddRange(purchasesToAdd);

                context.SaveChanges();

                return sb.ToString();
            }
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}