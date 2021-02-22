using System.IO;
using System.Text;
using System.Linq;
using Instagraph.Data;
using Newtonsoft.Json;
using Instagraph.Models;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Instagraph.DataProcessor.DtoModels.Import;

namespace Instagraph.DataProcessor
{
    public class Deserializer
    {
        private static string SuccessMessage = "Successfully imported {0}.";

        private static string ErrorMessage = "Error: Invalid data.";

        public static string ImportPictures(InstagraphContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var serializer = JsonConvert.DeserializeObject<List<ImportPictureDto>>(jsonString);

            var picturesToAdd = new List<Picture>();
            var pathsList = new List<string>();

            foreach (var pictureDto in serializer)
            {
                if (!IsValid(pictureDto) ||
                    pathsList.Contains(pictureDto.Path))
                {
                    sb.AppendLine(ErrorMessage);

                    continue;
                }

                var picture = new Picture()
                {
                    Path = pictureDto.Path,
                    Size = pictureDto.Size
                };

                pathsList.Add(pictureDto.Path);

                picturesToAdd.Add(picture);

                sb.AppendLine($"Successfully imported Picture {picture.Path}.");
            }

            context.Pictures.AddRange(picturesToAdd);

            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportUsers(InstagraphContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var serializer = JsonConvert.DeserializeObject<List<ImportUserDto>>(jsonString);

            var usersToAdd = new List<User>();

            foreach (var userDto in serializer)
            {
                if (!IsValid(userDto))
                {
                    sb.AppendLine(ErrorMessage);

                    continue;
                }

                var picture = context.Pictures.FirstOrDefault(p => p.Path == userDto.ProfilePicture);

                if (picture == null)
                {
                    sb.AppendLine(ErrorMessage);

                    continue;
                }

                var user = new User()
                {
                    Password = userDto.Password,
                    ProfilePicture = picture,
                    Username = userDto.Username
                };

                usersToAdd.Add(user);

                sb.AppendLine($"Successfully imported User {user.Username}.");
            }

            context.Users.AddRange(usersToAdd);

            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportFollowers(InstagraphContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var serializer = JsonConvert.DeserializeObject<List<ImportFollowerDto>>(jsonString);

            var followersToAdd = new List<UserFollower>();

            foreach (var followerDto in serializer)
            {
                if (!IsValid(followerDto))
                {
                    sb.AppendLine(ErrorMessage);

                    continue;
                }

                var user = context.Users.FirstOrDefault(u => u.Username == followerDto.User);

                var follower = context.Users.FirstOrDefault(u => u.Username == followerDto.Follower);

                var isFollowing = followersToAdd.Any(u => u.User == user && u.Follower == follower);

                if (user == null ||
                    follower == null ||
                    isFollowing)
                {
                    sb.AppendLine(ErrorMessage);

                    continue;
                }

                var userFollower = new UserFollower()
                {
                    User = user,
                    Follower = follower,
                };

                followersToAdd.Add(userFollower);

                sb.AppendLine($"Successfully imported Follower {follower.Username}to User {user.Username}.");
            }

            context.UsersFollowers.AddRange(followersToAdd);

            context.SaveChanges();

            return sb.ToString().Trim();
        }

        public static string ImportPosts(InstagraphContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(List<ImportPostDto>), new XmlRootAttribute("posts"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            var reader = new StringReader(xmlString);

            var postsToAdd = new List<Post>();

            using (reader)
            {
                var postDtos = (List<ImportPostDto>)serializer.Deserialize(reader);

                foreach (var postDto in postDtos)
                {
                    if (!IsValid(postDto))
                    {
                        sb.AppendLine(ErrorMessage);

                        continue;
                    }

                    var user = context.Users.FirstOrDefault(u => u.Username == postDto.User);

                    var picture = context.Pictures.FirstOrDefault(p => p.Path == postDto.Picture);

                    if (user == null || picture == null)
                    {
                        sb.AppendLine(ErrorMessage);

                        continue;
                    }

                    var post = new Post()
                    {
                        User = user,
                        Caption = postDto.Caption,
                        Picture = picture,
                        UserId = user.Id,
                        PictureId = picture.Id
                    };

                    postsToAdd.Add(post);

                    sb.AppendLine($"Successfully imported Post {post.Caption}.");
                }

                context.Posts.AddRange(postsToAdd);

                context.SaveChanges();

                return sb.ToString().Trim();
            }
        }

        public static string ImportComments(InstagraphContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(List<ImportCommentDto>), new XmlRootAttribute("comments"));

            var namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            var reader = new StringReader(xmlString);

            var commentsToAdd = new List<Comment>();

            using (reader)
            {
                var commentDtos = (List<ImportCommentDto>)serializer.Deserialize(reader);

                foreach (var commentDto in commentDtos)
                {
                    if (!IsValid(commentDto))
                    {
                        sb.AppendLine(ErrorMessage);

                        continue;
                    }

                    var user = context.Users.FirstOrDefault(u => u.Username == commentDto.User);

                    var post = context.Posts.FirstOrDefault(p => p.Id == commentDto.Post.Id);

                    if (user == null || post == null)
                    {
                        sb.AppendLine(ErrorMessage);

                        continue;
                    }

                    var comment = new Comment()
                    {
                        User = user,
                        UserId = user.Id,
                        Post = post,
                        PostId = post.Id,
                        Content = commentDto.Content
                    };

                    commentsToAdd.Add(comment);

                    sb.AppendLine($"Successfully imported Comment {comment.Content}.");
                }

                context.Comments.AddRange(commentsToAdd);

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
