using System;
using System.Collections.Generic;

namespace EntityFrameworkPractice.Data
{
    public class EmployeesProject
    {
        public int EmployeeId { get; set; }

        public int ProjectId { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Project Project { get; set; }
    }
}
