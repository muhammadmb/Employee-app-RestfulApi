using AutoMapper;
using EmployeeApi.Entities;
using EmployeeApi.Filters;
using EmployeeApi.Helper;
using EmployeeApi.Models;
using EmployeeApi.Repositories;
using EmployeeApi.ResourceParameters;
using EmployeeApi.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmployeeApi.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IPropertyCheckerService _propertyCheckerService;
        private readonly IMapper _mapper;

        public ProjectController(IProjectRepository projectRepository,
            IPropertyCheckerService propertyCheckerService,
            IMapper mapper)
        {
            _projectRepository = projectRepository ??
                throw new ArgumentNullException(nameof(projectRepository));
            _propertyCheckerService = propertyCheckerService ??
                throw new ArgumentNullException(nameof(propertyCheckerService));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet(Name = "GetProjects")]
        [HttpHead]
        public async Task<IActionResult> getProjects(
            [FromQuery] ProjectResourcesParameters projectResourcesParameters)
        {

            if (!_propertyCheckerService.TypeHasProperties<ProjectDto>(projectResourcesParameters.Fields) ||
                !_propertyCheckerService.TypeHasProperties<ProjectDto>(projectResourcesParameters.OrderBy))
            {
                return NotFound();
            }

            var projectsFromDb = await _projectRepository.GetProjects(projectResourcesParameters);
            var projects = _mapper.Map<IEnumerable<ProjectDto>>(projectsFromDb).ShapeData(projectResourcesParameters.Fields);

            var PreviousPageLink = projectsFromDb.HasPrevious ?
                CreateEmployeeResourceUri(projectResourcesParameters, ResourceUriType.PreviousPage) : null;

            var NextPageLink = projectsFromDb.HasNext ?
                CreateEmployeeResourceUri(projectResourcesParameters, ResourceUriType.NextPage) : null;

            var PaginationMetadata = new
            {
                projectsFromDb.CurrentPage,
                projectsFromDb.PageSize,
                projectsFromDb.TotalCount,
                projectsFromDb.TotalPages,
                PreviousPageLink,
                NextPageLink
            };

            Response.Headers.Add("Pagination",
                JsonSerializer.Serialize(PaginationMetadata));

            return Ok(projects);
        }

        [HttpGet("{projectId}", Name = "GetProject")]
        [HttpHead("{projectId}")]

        public async Task<IActionResult> getProject(Guid projectId, string fields)
        {
            var projectFromDb = await _projectRepository.GetProject(projectId);

            if (projectFromDb == null)
            {
                return NotFound();
            }

            var project = _mapper.Map<ProjectDto>(projectFromDb).shapeData(fields);

            return Ok(project);
        }

        [HttpGet("{projectId}/employees")]
        [HttpHead("{projectId}/employees")]
        [EmployeesFilter]
        public async Task<IActionResult> GetEmployeesForProject(Guid projectId)
        {
            var projectFromDb = await _projectRepository.GetProject(projectId);

            if (projectFromDb == null)
            {
                return NotFound();
            }

            var employeeProjects = projectFromDb.employeeProjects;

            var Employees = employeeProjects
                .Select(ep => ep.employee);

            return Ok(Employees);
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

            var projectfromRepo = await _projectRepository.GetProject(projectId);

            if (projectfromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(projectCreation, projectfromRepo);

            _projectRepository.Update(projectfromRepo);

            await _projectRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{projectId}")]
        public async Task<IActionResult> PartialUpdate(Guid projectId, JsonPatchDocument<ProjectCreation> patchDocument)
        {

            var projectfromRepo = await _projectRepository.GetProject(projectId);

            if (projectfromRepo == null)
            {
                return NotFound();
            }

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

        [HttpOptions()]
        public IActionResult GetProjectsOptions()
        {
            Response.Headers.Add("Allow", "Get, Options, Post");
            return Ok();
        }

        [HttpOptions("{projectId}")]
        public IActionResult GetProjectOptions()
        {
            Response.Headers.Add("Allow", "Get, Options, Put, Patch, Delete");
            return Ok();
        }

        private string CreateEmployeeResourceUri(
           ProjectResourcesParameters projectResourcesParameters,
           ResourceUriType resourceUriType)
        {
            switch (resourceUriType)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link("GetProjects",
                        new
                        {
                            OrderBy = projectResourcesParameters.OrderBy,
                            Fields = projectResourcesParameters.Fields,
                            PageNumber = projectResourcesParameters.PageNumber - 1,
                            projectResourcesParameters.PageSize,
                            projectResourcesParameters.SearchQuery
                        });
                case ResourceUriType.NextPage:
                    return Url.Link("GetProjects",
                        new
                        {
                            OrderBy = projectResourcesParameters.OrderBy,
                            Fields = projectResourcesParameters.Fields,
                            PageNumber = projectResourcesParameters.PageNumber + 1,
                            projectResourcesParameters.PageSize,
                            projectResourcesParameters.SearchQuery
                        });
                default:
                    return Url.Link("GetProjects",
                        new
                        {
                            OrderBy = projectResourcesParameters.OrderBy,
                            Fields = projectResourcesParameters.Fields,
                            projectResourcesParameters.PageNumber,
                            projectResourcesParameters.PageSize,
                            projectResourcesParameters.SearchQuery
                        });
            }
        }
    }
}