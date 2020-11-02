using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace EmployeeWevService.Entities
{
    public class Employee
    {
        [Key]
        public Guid EmployeeId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public string JobTitle { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(15)]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [Url]
        public string picUrl { get; set; }

        [Required]
        public double salary { get; set; }

        public DateTimeOffset DateOfBirth { get; set; }

        public Guid ProId { get; set; }

        public ICollection<Project> projects { get; set; }
            = new List<Project>();
    }
}
