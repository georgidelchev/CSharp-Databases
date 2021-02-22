using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Instagraph.Models
{
    public class Picture
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Path { get; set; }

        [Required]
        public decimal Size { get; set; }

        public virtual ICollection<User> Users { get; set; }
            = new HashSet<User>();

        public virtual ICollection<Post> Posts { get; set; }
            = new HashSet<Post>();
    }
}