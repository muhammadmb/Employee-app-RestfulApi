using EmployeeApi.Contexts;
using EmployeeApi.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.Services
{
    public class DepartmentRepository : IDepartmentRepository, IDisposable
    {
        private EmployeeContext _context;

        public DepartmentRepository(EmployeeContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<Department>> getDepartments()
        {
            return await _context.Departments
                .Include(d => d.Employees)
                .ToListAsync();
        }

        public async Task<Department> getDepartment(Guid departmentId)
        {
            return await _context.Departments
                .Where(d => d.DepartmentId == departmentId)
                .Include(d => d.Employees)
                .FirstOrDefaultAsync();
        }

        public void CreateDepartment(Department department, Guid managerId)
        {
            department.ManagerId = managerId;
            _context.Departments.Add(department);
        }

        public bool DepartmentExist(Guid departmentId)
        {
            return _context.Departments.Any(d => d.DepartmentId == departmentId);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
        }

    }
}
