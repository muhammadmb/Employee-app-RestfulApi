using AutoMapper;
using EmployeeApi.Entities;
using EmployeeApi.Models;
using EmployeeApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeApi.Controllers
{
    [ApiController]
    [Route("Api/projects")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public ProjectController(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository ??
                throw new ArgumentNullException(nameof(projectRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> getProjects()
        {
            var projectsFromDb =await _projectRepository.GetProjects();
            var projects = _mapper.Map<IEnumerable<ProjectDto>>(projectsFromDb);

            return Ok(projects);
        }

        [HttpGet("{projectId}")]
        public async Task<IActionResult> getProject(Guid projectId)
        {
            var projectFromDb = await _projectRepository.GetProject(projectId);

            var project = _mapper.Map<ProjectDto>(projectFromDb);

            return Ok(project);
        }

    }
}
