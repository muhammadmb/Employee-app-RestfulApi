using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeApi.Models
{
    public class EmployeeCreation
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

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

        [Required]
        public DateTimeOffset DateOfBirth { get; set; }

        public List<Guid> ProjectId { get; set; }

        public Guid departmentId { get; set; }
    }
}
