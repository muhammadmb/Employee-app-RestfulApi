using EmployeeApi.Contexts;
using EmployeeApi.Entities;
using EmployeeApi.Helper;
using EmployeeApi.ResourceParameters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.Repositories
{
    public class DepartmentRepository : IDepartmentRepository, IDisposable
    {
        private EmployeeContext _context;

        public DepartmentRepository(EmployeeContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }
        public async Task<PagedList<Department>> getDepartments(DepartmentResourceParameter Parameter)
        {
            var collection =
                _context.Departments
                .Include(d => d.Employees)
                as IQueryable<Department>;

            if (!string.IsNullOrEmpty(Parameter.SearchQuery))
            {
                Parameter.SearchQuery = Parameter.SearchQuery.Trim();

                collection =
                    collection.Where(d => d.DepartmentName.Contains(Parameter.SearchQuery)
                    || d.Headquarter.Contains(Parameter.SearchQuery));
            }

            if (!string.IsNullOrEmpty(Parameter.Headquarter))
            {
                Parameter.Headquarter = Parameter.Headquarter.Trim();

                collection =
                    collection.Where(d => d.Headquarter == Parameter.Headquarter);
            }

            if (!string.IsNullOrEmpty(Parameter.OrderBy))
            {
                Parameter.OrderBy = Parameter.OrderBy.Trim();

                if (Parameter.OrderBy.ToLowerInvariant() == "departmentName")
                {
                    collection =
                        collection.OrderBy(d => d.DepartmentName);
                }
                if (Parameter.OrderBy.ToLowerInvariant() == "employees")
                {
                    collection =
                        collection.OrderBy(d => d.Employees.Count());
                }
            }

            return PagedList<Department>.Create(
                collection,
                Parameter.PageNumber,
                Parameter.PageSize);
        }

        public async Task<Department> getDepartment(Guid departmentId)
        {
            return await _context.Departments
                .Where(d => d.DepartmentId == departmentId)
                .Include(d => d.Employees)
                .ThenInclude(e => e.employeeProjects)
                .ThenInclude(ep => ep.project)
                .FirstOrDefaultAsync();
        }

        public void CreateDepartment(Department department, Guid managerId)
        {
            department.ManagerId = managerId;
            _context.Departments.Add(department);
        }

        public void Update(Department department)
        {
            _context.Departments.Update(department);
        }
        public void Delete(Guid departmentId)
        {
            var department = new Department
            {
                DepartmentId = departmentId
            };
            _context.Departments.Remove(department);
            _context.SaveChanges();
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
