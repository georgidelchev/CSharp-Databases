﻿using System;
using System.Collections.Generic;

namespace P01_StudentSystem.Data.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime RegisteredOn { get; set; }

        public DateTime? Birthday { get; set; }

        public ICollection<StudentCourse> CourseEnrollments { get; set; }
            = new HashSet<StudentCourse>();

        public ICollection<Homework> HomeworkSubmissions { get; set; }
            = new HashSet<Homework>();
    }
}