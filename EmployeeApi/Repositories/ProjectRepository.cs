using EmployeeApi.Contexts;
using EmployeeApi.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.Services
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly EmployeeContext _context;

        public ProjectRepository(EmployeeContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Project>> GetProjects()
        {
            return await _context.Projects
                .Include(p => p.employeeProjects)
                .ThenInclude(ep => ep.employee)
                .ToListAsync();
        }

        public async Task<Project> GetProject(Guid projectId)
        {
            return await _context.Projects
                .Where(p => p.ProjectId == projectId)
                .Include(p => p.employeeProjects)
                .ThenInclude(ep => ep.employee)
                .FirstOrDefaultAsync();
        }

    }
}
