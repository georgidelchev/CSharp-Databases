using Artillery.Data.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Artillery.DataProcessor.ImportDto
{
    public class ImportGunsDto
    {
        [Required]
        public int ManufacturerId { get; set; }

        [Required]
        [Range(100, 1_350_000)]
        public int GunWeight { get; set; }

        [Required]
        [Range(2.00, 35.00)]
        public double BarrelLength { get; set; }

        public int? NumberBuild { get; set; }

        [Required]
        [Range(1, 100_000)]
        public int Range { get; set; }

        [Required]
        public string GunType { get; set; }

        [Required]
        public int ShellId { get; set; }

        public List<ImportGunCountriesDto> Countries { get; set; }
            = new List<ImportGunCountriesDto>();
    }
}
