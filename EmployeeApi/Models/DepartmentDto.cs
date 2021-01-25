using EmployeeApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.Models
{
    public class DepartmentDto
    {
        public string DepartmentName { get; set; }

        public string Headquarter { get; set; }

        public List<string> Employees { get; set; }

    }
}
