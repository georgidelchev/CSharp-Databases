using System;
using System.Collections.Generic;

namespace EntityFrameworkPractice.Data
{
    public class Department
    {
        public Department()
        {
            this.Employees = new HashSet<Employee>();
        }

        public int DepartmentId { get; set; }

        public string Name { get; set; }

        public int ManagerId { get; set; }

        public virtual Employee Manager { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
