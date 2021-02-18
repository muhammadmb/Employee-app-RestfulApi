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
    public class ProjectRepository : IProjectRepository, IDisposable
    {
        private readonly EmployeeContext _context;

        public ProjectRepository(EmployeeContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }

        public async Task<PagedList<Project>> GetProjects(ProjectResourcesParameters Parameters)
        {
            var Collection = _context.Projects
                .Include(p => p.employeeProjects)
                .ThenInclude(ep => ep.employee)
                as IQueryable<Project>;

            if (!string.IsNullOrWhiteSpace(Parameters.SearchQuery))
            {
                Parameters.SearchQuery = Parameters.SearchQuery.Trim();

                Collection =
                    Collection.Where(p => p.ProjectName.Contains(Parameters.SearchQuery));
            }

            if (!string.IsNullOrEmpty(Parameters.OrderBy))
            {
                Parameters.OrderBy = Parameters.OrderBy.Trim();

                if (Parameters.OrderBy.ToLowerInvariant() == "projectName")
                {
                    Collection =
                        Collection.OrderBy(p => p.ProjectName);
                }
                if (Parameters.OrderBy.ToLowerInvariant() == "employees")
                {
                    Collection =
                        Collection.OrderBy(p => p.employeeProjects.Count());
                }
                if (Parameters.OrderBy.ToLowerInvariant() == "budget")
                {
                    Collection =
                        Collection.OrderBy(p => p.Budget);
                }
                if (Parameters.OrderBy.ToLowerInvariant() == "profit")
                {
                    Collection =
                        Collection.OrderBy(p => p.Profit);
                }
            }

            return PagedList<Project>.Create(
                Collection,
                Parameters.PageNumber,
                Parameters.PageSize);
        }

        public async Task<Project> GetProject(Guid projectId)
        {
            return await _context.Projects
                .Where(p => p.ProjectId == projectId)
                .Include(p => p.employeeProjects)
                .ThenInclude(ep => ep.employee)
                .ThenInclude(e => e.department)
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
