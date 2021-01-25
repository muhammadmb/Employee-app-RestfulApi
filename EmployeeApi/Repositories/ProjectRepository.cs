using EmployeeApi.Contexts;
using EmployeeApi.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.Repositories
{
    public class ProjectRepository : IProjectRepository, IDisposable
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

        public void createProject(Project project)
        {
            _context.Projects.Add(project);
        }
        public void Update(Project project)
        {
            _context.Projects.Update(project);
        }

        public bool ProjectExist(Guid projectId)
        {
            return _context.Projects.Any(p => p.ProjectId == projectId);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        public void Delete(Guid projectId)
        {
            var project = new Project
            {
                ProjectId = projectId
            };

            _context.Projects.Remove(project);

            _context.SaveChanges();
        }

        public void Dispose()
        {
        }
    }
}
