using System.Collections.Generic;

namespace BookShop.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public ICollection<BookCategory> CategoryBooks { get; set; }
            = new HashSet<BookCategory>();
    }
}