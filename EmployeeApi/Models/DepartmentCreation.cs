using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeApi.Models
{
    public class DepartmentCreation
    {
        [Required]
        public string DepartmentName { get; set; }

        [Required]
        public string Headquarter { get; set; }

    }
}
