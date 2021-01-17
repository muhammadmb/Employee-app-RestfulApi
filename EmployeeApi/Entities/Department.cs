using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeApi.Entities
{
    public class Department
    {
        [Key]
        public Guid DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public string Headquarter { get; set; }

        public List<Employee> Employees { get; set; }
        
        public Guid ManagerId { get; set; }
        
        public Employee Manager { get; set; }

    }
}
