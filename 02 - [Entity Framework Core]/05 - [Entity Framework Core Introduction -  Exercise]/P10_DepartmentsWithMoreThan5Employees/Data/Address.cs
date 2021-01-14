using System;
using System.Collections.Generic;

namespace EntityFrameworkIntroductionExercise.Data
{
    public class Address
    {
        public Address()
        {
            this.Employees = new HashSet<Employees>();
        }

        public int AddressId { get; set; }

        public string AddressText { get; set; }

        public int? TownId { get; set; }

        public virtual Town Town { get; set; }

        public virtual ICollection<Employees> Employees { get; set; }
    }
}
