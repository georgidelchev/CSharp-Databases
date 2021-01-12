using System;
using System.Linq;
using MiniORM.App.Data;
using MiniORM.App.Data.Entities;

namespace MiniORM.App
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var connectionString = @"Server=DESKTOP-10E0DVG\SQLEXPRESS;Database=MiniORM;Integrated Security=true;";

            var context = new SoftUniDbContext(connectionString);

            context.Employees.Add(new Employees
            {
                FirstName = "Gosho",
                LastName = "Inserted",
                DepartmentId = context.Departments.First().Id,
                IsEmployed = true
            });

            var employee = context.Employees.Last();
            employee.FirstName = "Modified";

            context.SaveChanges(); ;
        }
    }
}
