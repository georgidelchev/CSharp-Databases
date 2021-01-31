using System.Collections.Generic;

namespace PetStore.Data.Models
{
    public class Toy
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal DistributorPrice { get; set; }

        public decimal Price { get; set; }

        public int BrandId { get; set; }

        public Brand Brand { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public ICollection<ToyOrder> Orders { get; set; }
            = new HashSet<ToyOrder>();
    }
}