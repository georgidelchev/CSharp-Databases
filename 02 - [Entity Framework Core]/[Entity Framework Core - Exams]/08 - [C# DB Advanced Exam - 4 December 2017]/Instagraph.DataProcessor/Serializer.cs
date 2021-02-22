using System.IO;
using System.Linq;
using System.Text;
using Instagraph.Data;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.Collections.Generic;
using Instagraph.DataProcessor.DtoModels.Export;

namespace Instagraph.DataProcessor
{
    public class Serializer
    {
        public static string ExportUncommentedPosts(InstagraphContext context)
        {
            var uncommentedPosts = context
                .Posts
                .Where(p => p.Comments.Count == 0)
                .Select(p => new
                {
                    Id = p.Id,
                    Picture = p.Picture.Path,
                    User = p.User.Username
                })
                .OrderBy(p => p.Id)
                .ToList();

            var json = JsonConvert.SerializeObject(uncommentedPosts, Formatting.Indented);

            return json;
        }

        public static string ExportPopularUsers(InstagraphContext context)
        {
            var popularUsers = context
                .Users
                .Where(u => u.Posts
                    .Any(p => p.Comments
                        .Any(c => u.Followers
                            .Any(f => f.FollowerId == c.UserId))))
                .OrderBy(u => u.Id)
                .Select(u => new
                {
                    Username = u.Username,
                    Followers = u.Followers.Count
                })
                .ToList();

            var json = JsonConvert.SerializeObject(popularUsers, Formatting.Indented);

            return json;
        }

        public static string ExportCommentsOnPosts(InstagraphContext context)
        {
            var sb = new StringBuilder();

            var users = context
                .Users
                .Select(u => new
                {
                    Username = u.Username,
                    Comments = u.Posts
                        .Select(p => p.Comments.Count)
                        .ToList()
                });

            var dtos = new List<ExportCommentsDto>();

            foreach (var dto in users)
            {
                var mostComments = 0;

                if (dto.Comments.Any())
                {
                    mostComments = dto.Comments
                        .OrderByDescending(c => c)
                        .First();
                }

                var user = new ExportCommentsDto()
                {
                    Username = dto.Username,
                    MostComments = mostComments
                };

                dtos.Add(user);
            }

            var serializer = new XmlSerializer(typeof(List<ExportCommentsDto>), new XmlRootAttribute("users"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            var writer = new StringWriter(sb);

            dtos = dtos.OrderByDescending(d => d.MostComments).ToList();

            using (writer)
            {
                serializer.Serialize(writer, dtos, namespaces);
            }

            return sb.ToString().Trim();
        }
    }
}
