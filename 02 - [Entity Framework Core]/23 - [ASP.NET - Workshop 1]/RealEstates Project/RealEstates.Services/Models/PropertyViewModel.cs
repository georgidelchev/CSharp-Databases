namespace RealEstates.Services.Models
{
    public class PropertyViewModel
    {
        public string District { get; set; }

        public int Size { get; set; }

        public string PropertyType { get; set; }

        public string BuildingType { get; set; }

        public int? Year { get; set; }

        public string Floor { get; set; }

        public int Price { get; set; }
    }
}