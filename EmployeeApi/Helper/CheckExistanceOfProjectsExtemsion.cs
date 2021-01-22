using EmployeeApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.Helper
{
    public static class CheckExistanceOfProjectsExtemsion
    {
        public static bool CheckExistanceOfProjects(this List<Guid> projectsId, IProjectRepository projectRepository)
        {
            foreach (var projectId in projectsId)
            {
                if (!projectRepository.ProjectExist(projectId))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
