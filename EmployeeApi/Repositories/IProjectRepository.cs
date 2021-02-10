using EmployeeApi.Entities;
using EmployeeApi.Helper;
using EmployeeApi.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeApi.Repositories
{
    public interface IProjectRepository
    {
        public Task<PagedList<Project>> GetProjects(ProjectResourcesParameters projectResourcesParameters);
        public Task<Project> GetProject(Guid projectId);
        void createProject(Project project);
        void Update(Project projectfromRepo);
        void Delete(Guid projectId);
        bool ProjectExist(Guid projectId);
        Task<bool> SaveChangesAsync();
    }
}
