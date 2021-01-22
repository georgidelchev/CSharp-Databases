using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BookShop.Models
{
    public class Author
    {
        public int AuthorId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [JsonIgnore]
        public ICollection<Book> Books { get; set; }
            = new HashSet<Book>();
    }
}