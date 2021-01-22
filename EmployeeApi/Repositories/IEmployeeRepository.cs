using EmployeeApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.Services
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployees();

        Task<IEnumerable<Employee>> GetEmployees(IEnumerable<Guid> Ids);
        
        Task<Employee> GetEmployee(Guid Id);
        
        void CreateEmployee(Employee createdEmployee);
        public void PartialUpdateEmployee(Employee employee, bool editProject);
        void UpdateEmployee(Employee employee);
        
        public bool EmployeeExist(Guid EmployeeId);
        
        public Task<bool> SaveChangesAsync();
        void Delete(Guid employeeId);
    }
}
