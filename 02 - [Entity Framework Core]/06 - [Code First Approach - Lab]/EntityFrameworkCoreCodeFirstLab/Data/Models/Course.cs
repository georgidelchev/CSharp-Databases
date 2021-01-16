using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static EntityFrameworkCoreCodeFirstLab.Data.DataValidations.Course;

namespace EntityFrameworkCoreCodeFirstLab.Data.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(COURSE_NAME_MAX_LENGTH)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DESCRIPTION_MAX_LENGTH)]
        public string Description { get; set; }

        public ICollection<StudentInCourse> Students { get; set; } = new HashSet<StudentInCourse>();

        public ICollection<Homework> Homeworks { get; set; } = new HashSet<Homework>();
    }
}