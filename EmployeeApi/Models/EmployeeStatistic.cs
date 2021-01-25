using EmployeeApi.Entities;
using EmployeeApi.Helper;
using System;

namespace EmployeeApi.Models
{
    public class EmployeeStatistic
    {

        public EmployeeStatistic()
        {
            MaxSalary = Int32.MinValue;
            MinSalary = Int32.MaxValue;
        }

        public String DepartmentName { get; set; }

        public int EmployeesNumber { get; set; }

        public double MaxSalary { get; set; }

        public double MinSalary { get; set; }

        public double AgeAverage { get; set; }

        public double SalaryAverage { get; set; }

        public double TotalSalary { get; set; }

        private int Ages;

        public EmployeeStatistic Accumulate(Employee e)
        {
            EmployeesNumber += 1;
            TotalSalary += e.salary;
            MaxSalary = Math.Max(MaxSalary, e.salary);
            MinSalary = Math.Min(MinSalary, e.salary);
            Ages += e.DateOfBirth.GetCurrentAge();
            return this;
        }

        public EmployeeStatistic Compute()
        {
            AgeAverage = Ages / EmployeesNumber;
            SalaryAverage = TotalSalary / EmployeesNumber;

            return this;
        }
    }
}
