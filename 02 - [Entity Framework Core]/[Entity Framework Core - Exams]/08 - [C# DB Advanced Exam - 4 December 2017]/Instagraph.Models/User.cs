using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Instagraph.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Username { get; set; }

        [Required]
        [MaxLength(20)]
        public string Password { get; set; }

        [Required]
        [ForeignKey(nameof(Picture))]
        public int ProfilePictureId { get; set; }

        [Required]
        public virtual Picture ProfilePicture { get; set; }

        public virtual ICollection<UserFollower> Followers { get; set; }
            = new HashSet<UserFollower>();

        public virtual ICollection<UserFollower> UserFollowing { get; set; }
            = new HashSet<UserFollower>();

        public virtual ICollection<Post> Posts { get; set; }
            = new HashSet<Post>();

        public virtual ICollection<Comment> Comments { get; set; }
            = new HashSet<Comment>();
    }
}