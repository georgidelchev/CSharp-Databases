using Artillery.Data.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Artillery.Data.Models
{
    public class Gun
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int ManufacturerId { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        [Required]
        public int GunWeight { get; set; }

        [Required]
        public double BarrelLength { get; set; }

        public int? NumberBuild { get; set; }

        [Required]
        public int Range { get; set; }

        [Required]
        public GunType GunType { get; set; }

        [Required]
        public int ShellId { get; set; }

        public virtual Shell Shell { get; set; }

        public ICollection<CountryGun> CountriesGuns { get; set; }
            = new HashSet<CountryGun>();
    }
}
