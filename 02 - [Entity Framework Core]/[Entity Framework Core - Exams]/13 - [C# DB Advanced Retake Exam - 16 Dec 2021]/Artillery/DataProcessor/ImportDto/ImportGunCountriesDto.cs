using System.ComponentModel.DataAnnotations;

namespace Artillery.DataProcessor.ImportDto
{
    public class ImportGunCountriesDto
    {
        [Required]
        public int Id { get; set; }
    }
}
