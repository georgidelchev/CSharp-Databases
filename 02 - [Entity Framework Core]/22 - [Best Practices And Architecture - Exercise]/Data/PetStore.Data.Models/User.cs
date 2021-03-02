using System.Collections.Generic;

namespace PetStore.Data.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public ICollection<Order> Orders { get; set; }
            = new HashSet<Order>();
    }
}