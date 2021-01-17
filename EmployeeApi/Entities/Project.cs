using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.Entities
{
    public class Project
    {
        public Project()
        {
            employeeProjects = new List<EmployeeProject>();
        }

        [Key]
        public Guid ProjectId { get; set; }

        [Required]
        public string ProjectName { get; set; }

        [Required]
        public double Budget { get; set; }

        [Required]
        public double Profit { get; set; }

        public List<EmployeeProject> employeeProjects { get; set; }
    }
}
