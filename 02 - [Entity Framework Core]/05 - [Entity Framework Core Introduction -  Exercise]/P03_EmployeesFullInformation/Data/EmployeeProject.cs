using System;
using System.Collections.Generic;

namespace EntityFrameworkIntroductionExercise.Data
{
    public class EmployeeProject
    {
        public int EmployeeId { get; set; }

        public int ProjectId { get; set; }

        public virtual Employees Employee { get; set; }

        public virtual Project Project { get; set; }
    }
}
