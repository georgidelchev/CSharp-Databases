using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static EntityFrameworkCoreCodeFirstLab.Data.DataValidations.Student;

namespace EntityFrameworkCoreCodeFirstLab.Data.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(STUDENT_NAME_MAX_LENGTH)]
        public string FirstName { get; set; }

        [MaxLength(STUDENT_NAME_MAX_LENGTH)]
        public string MiddleName { get; set; }

        [Required]
        [MaxLength(STUDENT_NAME_MAX_LENGTH)]
        public string LastName { get; set; }

        public int? Age { get; set; }

        public bool HasScholarship { get; set; }

        public DateTime RegistrationDate { get; set; }

        public StudentType Type { get; set; }

        public int TownId { get; set; }

        public Town Town { get; set; }

        public ICollection<StudentInCourse> Courses { get; set; } = new HashSet<StudentInCourse>();

        public ICollection<Homework> Homeworks { get; set; } = new HashSet<Homework>();

        [NotMapped]
        public string FullName
        {
            get
            {
                if (this.MiddleName == null)
                {
                    return $"{this.FirstName} {this.LastName}";
                }

                return $"{this.FirstName} {this.MiddleName} {this.LastName}";
            }
        }
    }
}