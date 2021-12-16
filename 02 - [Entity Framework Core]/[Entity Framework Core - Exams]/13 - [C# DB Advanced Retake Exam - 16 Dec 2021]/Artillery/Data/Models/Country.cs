using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Artillery.Data.Models
{
    public class Country
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(60)]
        public string CountryName { get; set; }

        [Required]
        public int ArmySize { get; set; }

        public virtual ICollection<CountryGun> CountriesGuns { get; set; }
            = new HashSet<CountryGun>();
    }
}
