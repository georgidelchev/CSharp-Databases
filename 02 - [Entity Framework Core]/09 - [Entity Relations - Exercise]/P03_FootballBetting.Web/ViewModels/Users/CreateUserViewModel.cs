using System.ComponentModel.DataAnnotations;

namespace P03_FootballBetting.Web.ViewModels.Users
{
    public class CreateUserViewModel
    {
        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(30)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Name { get; set; }

    }
}