using EmployeeApi.Entities;
using EmployeeApi.Helper;
using EmployeeApi.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeApi.Repositories
{
    public interface IDepartmentRepository
    {
        Task<PagedList<Department>> getDepartments(DepartmentResourceParameter departmentResourceParameter);
        Task<Department> getDepartment(Guid departmentId);
        void CreateDepartment(Department department, Guid managerId);
        void Update(Department departmentFromRepo);
        bool DepartmentExist(Guid departmentId);
        public Task<bool> SaveChangesAsync();
        void Delete(Guid departmentId);
    }
}
