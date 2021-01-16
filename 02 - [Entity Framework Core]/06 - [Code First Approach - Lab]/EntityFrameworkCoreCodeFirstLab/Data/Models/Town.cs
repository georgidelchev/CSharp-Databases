using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using static EntityFrameworkCoreCodeFirstLab.Data.DataValidations.Town;

namespace EntityFrameworkCoreCodeFirstLab.Data.Models
{
    public class Town
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(TOWN_NAME_MAX_LENGTH)]
        public string Name { get; set; }

        public ICollection<Student> Students { get; set; } = new HashSet<Student>();
    }
}