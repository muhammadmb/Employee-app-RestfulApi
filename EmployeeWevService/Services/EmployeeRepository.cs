using EmployeeWevService.Contexsts;
using EmployeeWevService.Entities;
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
            throw new NotImplementedException();
        }

        public void DeleteEmployee(Guid EmployeeId)
        {
            throw new NotImplementedException();
        }

        public bool EmployeeExist()
        {
            throw new NotImplementedException();
        }

        public int GetAgeAverage()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public double GetTotalSalary()
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void UpdateEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
