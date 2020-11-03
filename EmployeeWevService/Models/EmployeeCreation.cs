using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeWevService.Models
{
    public class EmployeeCreation
    {
       public string Name { get; set; }

        public string JobTitle { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string picUrl { get; set; }

       public double salary { get; set; }

        public DateTimeOffset DateOfBirth { get; set; }

        public Guid ProId { get; set; }
    }
}
