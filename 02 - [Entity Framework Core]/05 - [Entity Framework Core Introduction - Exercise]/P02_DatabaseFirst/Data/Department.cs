using System;
using System.Collections.Generic;

namespace EntityFrameworkIntroductionExercise.Data
{
    public class Department
    {
        public Department()
        {
            this.Employees = new HashSet<Employees>();
        }

        public int DepartmentId { get; set; }

        public string Name { get; set; }

        public int ManagerId { get; set; }

        public virtual Employees Manager { get; set; }

        public virtual ICollection<Employees> Employees { get; set; }
    }
}
