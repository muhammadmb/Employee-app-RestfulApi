using EmployeeWevService.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeWevService.Contexsts
{
    public class EmployeeContexst : DbContext
    {
        public EmployeeContexst(DbContextOptions<EmployeeContexst> options):base(options) 
        {
        }

        public DbSet<Project> projects { get; set; }

        public DbSet<Employee> employees { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(

                new Employee
                {
                    EmployeeId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                    Name = "hana",
                    JobTitle = "accountant",
                    Email = "h@gmail.com",
                    PhoneNumber = "01200012010",
                    picUrl = "http://nnn.com",
                    salary = 5000,
                    DateOfBirth = new DateTime(1998, 5, 5),
                    ProId = Guid.Parse("a28888e9-2ba9-473a-a40f-e38cb54f9b38")
                },
                new Employee
                {
                    EmployeeId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b36"),
                    Name = "efsa",
                    JobTitle = "afasfsd",
                    Email = "s@gmail.com",
                    PhoneNumber = "444444",
                    picUrl = "http://nnn.com",
                    salary = 5000,
                    DateOfBirth = new DateTime(1998, 5, 5),
                    ProId = Guid.Parse("a28888e9-2ba9-473a-a40f-e38cb54f9b38")
                },
                new Employee
                {
                    EmployeeId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b37"),
                    Name = "l",
                    JobTitle = "a",
                    Email = "s@gmail.com",
                    PhoneNumber = "5555",
                    picUrl = "http://nnn.com",
                    salary = 5000,
                    DateOfBirth = new DateTime(1998, 5, 5),
                    ProId = Guid.Parse("a28888e9-2ba9-473a-a40f-e38cb54f9b38")
                },
                new Employee
                {
                    EmployeeId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b38"),
                    Name = "d",
                    JobTitle = "f",
                    Email = "s@gmail.com",
                    PhoneNumber = "44444",
                    picUrl = "http://nnn.com",
                    salary = 5000,
                    DateOfBirth = new DateTime(1998, 5, 5),
                    ProId = Guid.Parse("a28888e9-2ba9-473a-a40f-e38cb54f9b38")
                },
                new Employee
                {
                    EmployeeId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b39"),
                    Name = "q",
                    JobTitle = "e",
                    Email = "s@gmail.com",
                    PhoneNumber = "01001001",
                    picUrl = "http://nnn.com",
                    salary = 5000,
                    DateOfBirth = new DateTime(1998, 5, 5),
                    ProId = Guid.Parse("a28888e9-2ba9-473a-a40f-e38cb54f9b38")
                }
                );
        }

    }
}
