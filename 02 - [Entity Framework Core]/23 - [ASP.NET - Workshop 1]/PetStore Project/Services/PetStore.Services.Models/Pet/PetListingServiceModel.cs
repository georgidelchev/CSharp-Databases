namespace PetStore.Services.Models.Pet
{
    public class PetListingServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Breed { get; set; }

        public string Category { get; set; }

        public decimal Price { get; set; }
    }
}