using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeApi.Entities
{
    public class Department
    {
        [Key]
        public Guid DepartmentId { get; set; }

        [Required]
        public string DepartmentName { get; set; }

        [Required]
        public string Headquarter { get; set; }

        public List<Employee> Employees { get; set; }

        [Required]
        public Guid ManagerId { get; set; }

    }
}
