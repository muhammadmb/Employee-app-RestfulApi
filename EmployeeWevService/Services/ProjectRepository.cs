using EmployeeWevService.Contexsts;
using EmployeeWevService.Entities;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeWevService.Services
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly EmployeeContexst _context;

        public ProjectRepository(EmployeeContexst contexst)
        {
            _context = contexst ??
                throw new ArgumentNullException(nameof(contexst));
        }

        public void AddProject(Project project)
        {
            _context.Add(project);
        }

        public void DeleteProject(Guid projectId)
        {
            _context.Remove(_context.projects.FirstOrDefault(p => p.ProjectId == projectId));
        }

        public async Task<Project> GetProject(Guid projectId)
        {
            return await _context.projects.FirstOrDefaultAsync(p => p.ProjectId == projectId);
        }

        public async Task<IEnumerable<Project>> GetProjects()
        {
            return await _context.projects.ToListAsync();
        }

        public bool ProjectExist(Guid projectId)
        {
            return (_context.projects.Any(p => p.ProjectId == projectId));
        }

        public double TotalBudjet()
        {
            double sum = 0.0;
            foreach (Project project in _context.projects.ToList())
            {
                sum += project.Budget;
            }
            return sum;
        }

        public double TotalProfit()
        {
            double sum = 0.0;
            foreach(Project project in _context.projects.ToList())
            {
                sum += project.Profit;
            }
            return sum;
        }

        public void UpdateProject(Project project)
        {
        }

    }
}
