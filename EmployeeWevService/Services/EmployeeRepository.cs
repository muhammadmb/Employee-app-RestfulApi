using EmployeeWevService.Contexsts;
using EmployeeWevService.Entities;
using EmployeeWevService.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeWevService.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContexst _contexst;

        public EmployeeRepository (EmployeeContexst contexst)
        {
            _contexst = contexst ??
                throw new ArgumentNullException(nameof(contexst));
        }

        public void CreateEmployee(Employee employee)
        {
            if(employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }
            _contexst.Add(employee);
        }

        public void DeleteEmployee(Guid EmployeeId)
        {
            _contexst.Remove(EmployeeId);
        }

        public bool EmployeeExist(Guid EmployeeId)
        {
            if (EmployeeId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(EmployeeId));
            }

            return (_contexst.employees.Any(e => e.EmployeeId == EmployeeId));
        }

        public double GetAgeAverage()
        {
            double SumAges = 0;

            foreach (Employee employee in _contexst.employees.ToList())
            {
                SumAges += employee.DateOfBirth.GetCurrentAge();
            }
            return (SumAges / GetEmployeesNumber());
        }

        public async Task<Employee> GetEmployee(Guid EmployeeId)
        {
            if(EmployeeId == null)
            {
                throw new ArgumentNullException(nameof(EmployeeId));
            }
            return await _contexst.employees.Include(e => e.projects).FirstOrDefaultAsync(e => e.EmployeeId == EmployeeId);
        }

        public IEnumerable<Employee> GetEmployeeBySalaryMoreThan(double salary)
        {
            List<Employee> Employees = new List<Employee>();

            foreach (Employee employee in _contexst.employees.ToList())
            {
                if (employee.salary >= salary)
                {
                    Employees.Add(employee);
                }
            }
            return Employees;
        }

        // Get All Employees async
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _contexst.employees.Include(e => e.projects).ToListAsync();
        }

        public int GetEmployeesNumber()
        {
            return _contexst.employees.ToList().Count();
        }

        public double GetSalaryAverage()
        {
            double SumSalary = 0;

            foreach (Employee employee in _contexst.employees.ToList())
            {
                SumSalary += employee.salary;
            }
            return (SumSalary);
        }

        public double GetTotalSalary()
        {
            double SumSalary = 0;
            foreach (Employee employee in _contexst.employees.ToList())
            {
                SumSalary += employee.salary;
            }
            return (SumSalary / GetEmployeesNumber());
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _contexst.SaveChangesAsync() > 0);
        }

        public void UpdateEmployee(Employee employee)
        {
        }
    }
}
