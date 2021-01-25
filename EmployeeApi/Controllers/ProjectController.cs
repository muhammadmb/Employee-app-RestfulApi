using AutoMapper;
using EmployeeApi.Entities;
using EmployeeApi.Models;
using EmployeeApi.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeApi.Controllers
{
    [ApiController]
    [Route("api/projects")]
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
            var projectsFromDb = await _projectRepository.GetProjects();
            var projects = _mapper.Map<IEnumerable<ProjectDto>>(projectsFromDb);

            return Ok(projects);
        }

        [HttpGet("{projectId}", Name = "GetProject")]
        public async Task<IActionResult> getProject(Guid projectId)
        {
            var projectFromDb = await _projectRepository.GetProject(projectId);

            var project = _mapper.Map<ProjectDto>(projectFromDb);

            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(ProjectCreation projectCreation)
        {
            if (projectCreation == null)
            {
                throw new ArgumentNullException(nameof(projectCreation));
            }

            var project = _mapper.Map<Project>(projectCreation);

            _projectRepository.createProject(project);

            await _projectRepository.SaveChangesAsync();

            return CreatedAtRoute(
                "GetProject",
                new { projectId = project.ProjectId },
                projectCreation
                );
        }

        [HttpPut("{projectId}")]
        public async Task<IActionResult> updateProject(Guid projectId, ProjectCreation projectCreation)
        {
            if (projectCreation == null)
            {
                throw new ArgumentNullException(nameof(projectCreation));
            }

            if (projectId == null)
            {
                throw new ArgumentNullException(nameof(projectCreation));
            }

            if (!_projectRepository.ProjectExist(projectId))
            {
                return NotFound("this Id is not right");
            }

            var projectfromRepo = await _projectRepository.GetProject(projectId);

            _mapper.Map(projectCreation, projectfromRepo);

            _projectRepository.Update(projectfromRepo);

            await _projectRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{projectId}")]
        public async Task<IActionResult> PartialUpdate(Guid projectId, JsonPatchDocument<ProjectCreation> patchDocument)
        {
            if (!_projectRepository.ProjectExist(projectId))
            {
                return NotFound("this Id is not right");
            }

            var projectfromRepo = await _projectRepository.GetProject(projectId);

            var project = _mapper.Map<ProjectCreation>(projectfromRepo);

            patchDocument.ApplyTo(project, ModelState);

            if (!TryValidateModel(project))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(project, projectfromRepo);

            _projectRepository.Update(projectfromRepo);

            await _projectRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{projectId}")]
        public IActionResult Delete(Guid projectId)
        {
            if (!_projectRepository.ProjectExist(projectId))
            {
                return NotFound("this Id is not right");
            }

            _projectRepository.Delete(projectId);

            return NoContent();
        }
    }
}
