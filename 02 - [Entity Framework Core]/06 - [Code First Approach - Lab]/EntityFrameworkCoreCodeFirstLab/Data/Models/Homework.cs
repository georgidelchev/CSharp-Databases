using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkCoreCodeFirstLab.Data.Models
{
    public class Homework
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public double Score { get; set; }

        public int StudentId { get; set; }

        public Student Student { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }
    }
}