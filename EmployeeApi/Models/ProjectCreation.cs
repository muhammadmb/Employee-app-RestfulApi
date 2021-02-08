using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.Models
{
    public class ProjectCreation
    {
        [Required]
        public string ProjectName { get; set; }
        
        [Required]
        public double Budget { get; set; }

        [Required]
        public double Profit { get; set; }
    }
}
