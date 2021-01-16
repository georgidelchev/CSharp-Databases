using EntityFrameworkCoreCodeFirstLab.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreCodeFirstLab.Data
{
    public class StudentsDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Town> Towns { get; set; }

        public DbSet<StudentInCourse> StudentsInCourses { get; set; }

        public DbSet<Homework> Homeworks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DataSettings.CONNECTION_STRING);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Student>()
                .HasOne(st => st.Town)
                .WithMany(t => t.Students)
                .HasForeignKey(st => st.TownId);

            modelBuilder
                .Entity<Student>()
                .HasMany(s => s.Homeworks)
                .WithOne(h => h.Student)
                .HasForeignKey(h => h.StudentId);

            modelBuilder
                .Entity<Course>()
                .HasMany(c => c.Homeworks)
                .WithOne(h => h.Course)
                .HasForeignKey(h => h.CourseId);

            modelBuilder
                .Entity<StudentInCourse>()
                .HasKey(sc => new
                {
                    sc.StudentId,
                    sc.CourseId
                });

            modelBuilder
                .Entity<StudentInCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(st => st.Courses)
                .HasForeignKey(sc => sc.StudentId);

            modelBuilder
                .Entity<StudentInCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.Students)
                .HasForeignKey(sc => sc.CourseId);
        }
    }
}