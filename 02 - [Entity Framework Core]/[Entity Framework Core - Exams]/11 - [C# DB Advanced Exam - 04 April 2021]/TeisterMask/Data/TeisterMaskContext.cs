using TeisterMask.Data.Models;

namespace TeisterMask.Data
{
    using Microsoft.EntityFrameworkCore;

    public class TeisterMaskContext : DbContext
    {
        public TeisterMaskContext() { }

        public TeisterMaskContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<EmployeeTask> EmployeesTasks { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Task> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeTask>(employeeTask =>
            {
                employeeTask
                    .HasKey(et => new
                    {
                        et.EmployeeId,
                        et.TaskId
                    });
            });
        }
    }
}