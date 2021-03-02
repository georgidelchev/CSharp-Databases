using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstates.Models
{
    public class Tag
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<RealEstatePropertyTag> Tags { get; set; }
            = new HashSet<RealEstatePropertyTag>();
    }
}