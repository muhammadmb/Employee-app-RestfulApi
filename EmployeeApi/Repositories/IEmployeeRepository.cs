using EmployeeApi.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeApi.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployees();

        Task<IEnumerable<Employee>> GetEmployees(IEnumerable<Guid> Ids);

        Task<Employee> GetEmployee(Guid Id);
        void CreateEmployeeToDepartmentWithProject(Guid departmentId, IEnumerable<Guid> projectIds, Employee createdEmployee);

        void CreateEmployee(Guid departmentId, Employee createdEmployee);

        public void PartialUpdateEmployee(Employee employee, bool editProject);

        void UpdateEmployee(Employee employee);

        public bool EmployeeExist(Guid EmployeeId);

        public Task<bool> SaveChangesAsync();
        void Delete(Guid employeeId);
    }
}
