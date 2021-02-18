using EmployeeApi.Contexts;
using EmployeeApi.Entities;
using EmployeeApi.Helper;
using EmployeeApi.ResourceParameters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.Repositories
{
    public class EmployeeRepository : IEmployeeRepository, IDisposable
    {
        private readonly EmployeeContext _context;

        public EmployeeRepository(EmployeeContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }


        public async Task<PagedList<Employee>> GetEmployees(EmployeeResourceParameter employeeResourceParameter)
        {

            if (employeeResourceParameter == null)
                throw new ArgumentNullException(nameof(employeeResourceParameter));


            var collection = _context.Employees
                    .Include(e => e.department)
                    .Include(e => e.employeeProjects)
                    .ThenInclude(ep => ep.project)
                    as IQueryable<Employee>;

            if (!string.IsNullOrEmpty(employeeResourceParameter.JobTitle))
            {
                employeeResourceParameter.JobTitle = employeeResourceParameter.JobTitle.Trim();

                collection = 
                    collection.Where(e => e.JobTitle == employeeResourceParameter.JobTitle);
            }

            if (!string.IsNullOrEmpty(employeeResourceParameter.department))
            {
                employeeResourceParameter.department = employeeResourceParameter.department.Trim();

                collection = 
                    collection.Where(e => e.department.DepartmentName == employeeResourceParameter.department);
            }

            if (!string.IsNullOrEmpty(employeeResourceParameter.SearchQuery))
            {
                employeeResourceParameter.SearchQuery = employeeResourceParameter.SearchQuery.Trim();

                collection =
                    collection.Where(e => e.FirstName.Contains(employeeResourceParameter.SearchQuery)
                    || e.LastName.Contains(employeeResourceParameter.SearchQuery)
                    || e.JobTitle.Contains(employeeResourceParameter.SearchQuery));
            }

            if (!string.IsNullOrWhiteSpace(employeeResourceParameter.OrderBy))
            {
                employeeResourceParameter.OrderBy = employeeResourceParameter.OrderBy.Trim();

                if(employeeResourceParameter.OrderBy.ToLowerInvariant() == "name")
                {
                    collection
                        = collection.OrderBy(e => e.FirstName).ThenBy(e => e.LastName);
                }

                if (employeeResourceParameter.OrderBy.ToLowerInvariant() == "age")
                {
                    collection
                        = collection.OrderBy(e => e.DateOfBirth);
                }
            }

            return PagedList<Employee>.Create(
                collection,
                employeeResourceParameter.PageNumber,
                employeeResourceParameter.PageSize);

        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await
                _context.Employees
                .Include(e => e.department)
                .Include(e => e.employeeProjects).ThenInclude(ep => ep.project)
                .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployees(IEnumerable<Guid> Ids)
        {
            return await _context.Employees
            .Where(e => Ids.Contains(e.EmployeeId))
            .Include(e => e.department)
            .Include(e => e.employeeProjects).ThenInclude(ep => ep.project)
            .ToListAsync();
        }

        public async Task<Employee> GetEmployee(Guid Id)
        {
            return await
                _context.Employees
                .Where(e => e.EmployeeId == Id)
                .Include(e => e.department)
                .Include(e => e.employeeProjects).ThenInclude(ep => ep.project)
                .FirstOrDefaultAsync();
        }

        public void CreateEmployee(Guid departmentId, Employee createdEmployee)
        {
            createdEmployee.departmentId = departmentId;
            _context.Employees.Add(createdEmployee);
        }
        public void CreateEmployeeToDepartmentWithProject(Guid departmentId, IEnumerable<Guid> projectIds, Employee createdEmployee)
        {
            createdEmployee.departmentId = departmentId;
            foreach (var projectId in projectIds)
            {
                createdEmployee.employeeProjects.Add(new EmployeeProject
                {
                    EmployeeId = createdEmployee.EmployeeId,
                    ProjectId = projectId
                });
            }

            _context.Employees.Add(createdEmployee);
        }

        public void PartialUpdateEmployee(Employee employee, bool editProject)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            if (editProject)
            {
                var list = new List<EmployeeProject>();

                var employeeProjectsInDb = _context.EmployeeProjects.Where(x => x.EmployeeId == employee.EmployeeId).ToList();

                var IdList = employee.ProjectId.ToList();

                foreach (var emp in IdList)
                {

                    var employeeProject =
                        new EmployeeProject
                        {
                            EmployeeId = employee.EmployeeId,
                            ProjectId = emp
                        };
                    if (!list.Any(p => p.ProjectId == emp))
                    {
                        list.Add(employeeProject);
                    }
                }

                _context.EmployeeProjects.RemoveRange(employeeProjectsInDb);
                _context.EmployeeProjects.AddRange(list);

                employee.employeeProjects = list;
                _context.Employees.Update(employee);
            }
            else
            {
                _context.Employees.Update(employee);
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            _context.Employees.Update(employee);
        }

        public void Delete(Guid employeeId)
        {
            Employee deleteEmployee = new Employee
            {
                EmployeeId = employeeId
            };

            _context.Employees.Remove(deleteEmployee);
            _context.SaveChanges();
        }

        public bool EmployeeExist(Guid EmployeeId)
        {
            return _context.Employees.Any(e => e.EmployeeId == EmployeeId);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        public void Dispose()
        {
        }

    }
}