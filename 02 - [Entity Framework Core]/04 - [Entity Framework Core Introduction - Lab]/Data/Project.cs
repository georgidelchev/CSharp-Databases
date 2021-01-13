using System;
using System.Collections.Generic;

namespace EntityFrameworkPractice.Data
{
    public class Project
    {
        public Project()
        {
            this.EmployeesProjects = new HashSet<EmployeesProject>();
        }

        public int ProjectId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public virtual ICollection<EmployeesProject> EmployeesProjects { get; set; }
    }
}
