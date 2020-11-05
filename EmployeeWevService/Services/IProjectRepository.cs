using EmployeeWevService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeWevService.Services
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetProjects();

        Task<Project> GetProject(Guid projectId);

        Double TotalBudjet();

        Double TotalProfit();

        void AddProject(Project project);

        void UpdateProject(Project project);

        void DeleteProject(Guid projectId);

        bool ProjectExist(Guid projectId);
    }
}
