using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstates.Models
{
    public class RealEstatePropertyTag
    {
        public int PropertyId { get; set; }

        public virtual RealEstateProperty Property { get; set; }
        
        public int TagId { get; set; }

        public virtual Tag Tag { get; set; }
    }
}