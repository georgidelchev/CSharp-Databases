using System;
using System.Linq;
using EntityFrameworkCoreCodeFirstLab.Data;
using EntityFrameworkCoreCodeFirstLab.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreCodeFirstLab
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var db = new StudentsDbContext();

            using (db)
            {
                db.Database.Migrate();

                //db.StudentsInCourses.Add(new StudentInCourse
                //{
                //    Student = new Student
                //    {
                //        FirstName = "Dimitar",
                //        LastName = "Ivanov",
                //        Age = 26,
                //        Type = StudentType.Graduated,
                //        RegistrationDate = DateTime.UtcNow,
                //        Town = new Town
                //        {
                //            Name = "Sofia"
                //        }
                //    },
                //    Course = new Course
                //    {
                //        Name = "CSharp Fundamentals",
                //        Description = "CSharp Path Course"
                //    }
                //});

                //db.SaveChanges();

                //db.Homeworks.Add(new Homework
                //{
                //    Content = "Math Homework",
                //    Score = 3.45,
                //    StudentId = 1,
                //    CourseId = 3
                //});

                //db.SaveChanges();

                var courses = db
                    .Courses
                    .Select(c => new
                    {
                        Name = c.Name,
                        TotalStudents = c.Students
                            .Where(s => s.Course
                                .Homeworks
                                .Average(h => h.Score) > 2)
                            .Count(),
                        Students = c
                            .Students
                            .Select(s => new
                            {
                                FullName = s.Student.FirstName
                                           + " " +
                                           s.Student.LastName,
                                Score = s.Student
                                    .Homeworks
                                    .Average(h => h.Score)
                            })
                    })
                    .ToList();
            }
        }
    }
}
