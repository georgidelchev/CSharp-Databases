using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static MyCoolCarSystem.Data.DataValidations.Customer;

namespace MyCoolCarSystem.Data.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(MAX_FIRST_NAME_LENGTH)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(MAX_LAST_NAME_LENGTH)]
        public string LastName { get; set; }

        [Required]
        public int Age { get; set; }

        public Address Address { get; set; }

        public ICollection<CarPurchase> Purchases { get; set; }
            = new HashSet<CarPurchase>();
    }
}