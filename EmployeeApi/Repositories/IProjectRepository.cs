using EmployeeApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.Services
{
    public interface IProjectRepository
    {
        public Task<IEnumerable<Project>> GetProjects();
        public Task<Project> GetProject(Guid projectId);
        bool ProjectExist(Guid projectId);
    }
}
