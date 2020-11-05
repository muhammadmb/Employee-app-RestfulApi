using EmployeeWevService.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeWevService.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepo;

        public ProjectController(IProjectRepository projectRepository) 
        {
            _projectRepo = projectRepository ??
                throw new ArgumentNullException(nameof(projectRepository));
        }

        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            return Ok(await _projectRepo.GetProjects());
        }
        [HttpGet("{projectId}")]
        public async Task<IActionResult> GetAProject(Guid projectId)
        {
            if(projectId == null)
            {
                throw new ArgumentNullException(nameof(projectId));
            }

            if (!_projectRepo.ProjectExist(projectId))
            {
                throw new ArgumentNullException(nameof(projectId));
            }

            var project = await _projectRepo.GetProject(projectId);

            return Ok(project);
        } 
    }
}
