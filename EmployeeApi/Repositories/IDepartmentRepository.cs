using EmployeeApi.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeApi.Services
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> getDepartments();
        Task<Department> getDepartment(Guid departmentId);

    }
}
