using System;
using System.Collections.Generic;

#nullable disable

namespace EntityFrameworkPractice.Data
{
    public class Town
    {
        public Town()
        {
            this.Addresses = new HashSet<Address>();
        }

        public int TownId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}
