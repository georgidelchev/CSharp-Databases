using System.Collections.Generic;

namespace PetStore.Data.Models
{
    public class Breed
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Pet> Pets { get; set; }
            = new HashSet<Pet>();
    }
}