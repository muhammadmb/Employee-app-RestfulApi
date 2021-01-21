using EmployeeApi.Contexts;
using EmployeeApi.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.Services
{
    public class EmployeeRepository : IEmployeeRepository, IDisposable
    {
        private readonly EmployeeContext _context;

        public EmployeeRepository( EmployeeContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
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

        public void CreateEmployee(Employee createdEmployee)
        {
            if(createdEmployee == null)
            {
                throw new ArgumentNullException(nameof(createdEmployee));
            }

            foreach (var emp in createdEmployee.ProjectId)
            {
                createdEmployee.employeeProjects.Add(new EmployeeProject
                {
                    EmployeeId = createdEmployee.EmployeeId,
                    ProjectId = emp
                }
                );
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

                    if (employeeProjectsInDb.Any(ep => ep.ProjectId == emp))
                    {
                        continue;
                    }
                    else
                    {
                        _context.Add(employeeProject);
                    }

                    list.Add(employeeProject);
                }

                foreach (var ep in employeeProjectsInDb)
                {
                    if (list.Any(i => i.ProjectId == ep.ProjectId))
                    {
                        continue;
                    }
                    else
                    {
                        _context.Remove(ep);
                    }
                }

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
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            PartialUpdateEmployee(employee, true);
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
