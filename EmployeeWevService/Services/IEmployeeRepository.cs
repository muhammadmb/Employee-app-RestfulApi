using EmployeeWevService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeWevService.Services
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployees();

        Task<Employee> GetEmployee(Guid EmployeeId);

        IEnumerable<Employee> GetEmployeeBySalaryMoreThan (Double salary);

        Double GetTotalSalary();

        Double GetSalaryAverage();

        double GetAgeAverage();

        int GetEmployeesNumber();

        void CreateEmployee(Employee employee);

        void UpdateEmployee(Employee employee);

        void DeleteEmployee(Guid EmployeeId);

        Task<bool> SaveChangesAsync();

        bool EmployeeExist(Guid EmployeeId);
    }
}
