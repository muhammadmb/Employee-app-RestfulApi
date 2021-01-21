using EmployeeApi.Entities;
using System;
using System.Collections.Generic;

namespace EmployeeApi.Models
{
    public class ProjectDto
    {
        public string ProjectName { get; set; }
        public double Budget { get; set; }
        public double Profit { get; set; }

        public List<Guid> Employees { get; set; }
    }
}
