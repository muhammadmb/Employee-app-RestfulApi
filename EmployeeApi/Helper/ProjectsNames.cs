using EmployeeApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.Helper
{
    public static class ProjectsNames
    {
        public static IEnumerable<string> GetProjectsNames(this List<EmployeeProject> employeeProject)
        {
            foreach (var ep in employeeProject)
            {
                yield return ep.project.ProjectName;
            }
        }
        public static IEnumerable<Guid> GetProjectsId(this List<EmployeeProject> employeeProject)
        {
            foreach (var ep in employeeProject)
            {
                yield return ep.project.ProjectId;
            }
        }
    }
}
