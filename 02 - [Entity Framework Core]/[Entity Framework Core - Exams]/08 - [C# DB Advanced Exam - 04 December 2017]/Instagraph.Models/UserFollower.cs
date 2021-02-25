using System.ComponentModel.DataAnnotations;

namespace Instagraph.Models
{
    public class UserFollower
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public virtual User User { get; set; }

        [Required]
        public int FollowerId { get; set; }

        [Required]
        public virtual User Follower { get; set; }
    }
}