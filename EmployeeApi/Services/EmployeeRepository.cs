using EmployeeApi.Contexts;
using EmployeeApi.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.Services
{
    public class EmployeeRepository : IEmployeeRepository
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
        
        public async Task<Employee> GetEmployee(Guid Id)
        {
            return await
                _context.Employees
                .Where(e => e.EmployeeId == Id)
                .Include(e => e.department)
                .Include(e => e.employeeProjects).ThenInclude(ep => ep.project)
                .FirstOrDefaultAsync();
        }

        
    }
}
