using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.Models
{
    public class EmployeeDto
    {
        public string Name { get; set; }
        public string JobTitle { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string picUrl { get; set; }
        public double salary { get; set; }
        public string department { get; set; }
        public int age { get; set; }
        public List<string> Projects { get; set; }
    }
}
