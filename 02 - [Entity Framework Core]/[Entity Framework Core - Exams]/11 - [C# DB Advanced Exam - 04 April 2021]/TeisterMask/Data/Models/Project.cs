using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TeisterMask.Data.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        public DateTime OpenDate { get; set; }
        
        public DateTime? DueDate { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
            = new HashSet<Task>();
    }
}