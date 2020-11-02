using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeWevService.Entities
{
    public class Project
    {
        [Key]
        public Guid ProjectId { get; set; }

        [Required]
        public string ProjectName { get; set; }

        [Required]
        public double Budget { get; set; }

        [Required]
        public double Profit { get; set; }

        public Employee employee { get; set; }
    }
}
