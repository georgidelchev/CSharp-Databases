using System;
using System.Linq;
using P03_FootballBetting.Data;

namespace P03_FootballBetting
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new FootballBettingContext();

            
                var users = context
                    .Users
                    .Select(u => new
                    {
                        Username = u.Username,
                        Email = u.Email,
                        Name = u.Name ?? "(No Name)"
                    })
                    .ToList();

                foreach (var user in users)
                {
                    Console.WriteLine($"{user.Username} -> " +
                                      $"{user.Name} -> " +
                                      $"{user.Email}");
                }
        }
    }
}
