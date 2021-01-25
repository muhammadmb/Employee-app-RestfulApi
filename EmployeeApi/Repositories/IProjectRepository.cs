using EmployeeApi.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeApi.Repositories
{
    public interface IProjectRepository
    {
        public Task<IEnumerable<Project>> GetProjects();
        public Task<Project> GetProject(Guid projectId);
        void createProject(Project project);
        void Update(Project projectfromRepo);
        void Delete(Guid projectId);
        bool ProjectExist(Guid projectId);
        Task<bool> SaveChangesAsync();
    }
}
