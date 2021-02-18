using AutoMapper;
using EmployeeApi.Entities;
using EmployeeApi.Helper;
using EmployeeApi.ModelBinders;
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
    [Route("api/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IPropertyCheckerService _propertyCheckerService;

        public EmployeeController(
            IEmployeeRepository employeeRepsitory,
            IProjectRepository projectRepository,
            IDepartmentRepository departmentRepository,
            IPropertyCheckerService propertyCheckerService,
            IMapper mapper)
        {
            _employeeRepository = employeeRepsitory ??
                throw new ArgumentNullException(nameof(employeeRepsitory));
            _projectRepository = projectRepository ??
                throw new ArgumentNullException(nameof(projectRepository));
            _departmentRepository = departmentRepository ??
                throw new ArgumentNullException(nameof(departmentRepository));
            _propertyCheckerService = propertyCheckerService ??
                throw new ArgumentNullException(nameof(propertyCheckerService));
            _mapper = mapper ??
                    throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet(Name = "GetEmployees")]
        [HttpHead]

        public async Task<IActionResult> GetEmployees(
            [FromQuery] EmployeeResourceParameter employeeResourceParameter)
        {
            if (!_propertyCheckerService.TypeHasProperties<EmployeeDto>(employeeResourceParameter.Fields) ||
                !_propertyCheckerService.TypeHasProperties<EmployeeDto>(employeeResourceParameter.OrderBy))
            {
                return NotFound();
            }

            var employees = await _employeeRepository.GetEmployees(employeeResourceParameter);
            var shapedEmployeesData = _mapper.Map<IEnumerable<EmployeeDto>>(employees).ShapeData(employeeResourceParameter.Fields);

            var PreviousPageLink = employees.HasPrevious ?
                CreateEmployeeResourceUri(employeeResourceParameter, ResourceUriType.PreviousPage) : null;

            var NextPageLink = employees.HasNext ?
                CreateEmployeeResourceUri(employeeResourceParameter, ResourceUriType.NextPage) : null;

            var PaginationMetadata = new
            {
                employees.CurrentPage,
                employees.PageSize,
                employees.TotalCount,
                employees.TotalPages,
                PreviousPageLink,
                NextPageLink
            };

            Response.Headers.Add("Pagination",
                JsonSerializer.Serialize(PaginationMetadata));

            return Ok(shapedEmployeesData);
        }

        [HttpGet("{employeeId}", Name = "GetEmployee")]
        [HttpHead("{employeeId}")]

        public async Task<IActionResult> GetEmployee(Guid employeeId, string fields)
        {
            if (!_propertyCheckerService.TypeHasProperties<EmployeeDto>(fields))
            {
                return NotFound();
            }

            if (employeeId == null)
            {
                throw new ArgumentNullException(nameof(employeeId));
            }

            var Employee = await _employeeRepository.GetEmployee(employeeId);

            if (Employee == null)
            {
                return NotFound();
            }

            var shapedEmployeeData = _mapper.Map<EmployeeDto>(Employee).shapeData(fields);

            return Ok(shapedEmployeeData);
        }

        [HttpPost("Department/{departmentId}/project/{projectIds}")]
        public async Task<IActionResult> CreateEmployeeToDepartment(
            Guid departmentId,
            [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> projectIds,
            EmployeeCreation employeeCreation)

        {
            if (employeeCreation == null)
            {
                throw new ArgumentNullException(nameof(employeeCreation));
            }

            if (!_departmentRepository.DepartmentExist(departmentId))
            {
                return NotFound("this deprtment Id Is not exist");
            }

            if (!projectIds.ToList().CheckExistanceOfProjects(_projectRepository))
            {
                return NotFound("this Project Id Is not exist");
            }

            var CreatedEmployee = _mapper.Map<Employee>(employeeCreation);

            _employeeRepository.CreateEmployeeToDepartmentWithProject(departmentId, projectIds, CreatedEmployee);

            await _employeeRepository.SaveChangesAsync();

            return CreatedAtRoute(
                "GetEmployee",
                new { employeeId = CreatedEmployee.EmployeeId },
                employeeCreation
                );
        }


        [HttpPut("{employeeId}")]
        public async Task<IActionResult> UpdateEmployee(
            Guid employeeId,
            EmployeeCreation employeeCreation)
        {
            if (employeeId == null)
            {
                throw new ArgumentNullException(nameof(employeeId));
            }

            if (employeeCreation == null)
            {
                throw new ArgumentNullException(nameof(employeeCreation));
            }

            var employeeFromRepo = await _employeeRepository.GetEmployee(employeeId);

            if (employeeFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(employeeCreation, employeeFromRepo);

            _employeeRepository.UpdateEmployee(employeeFromRepo);

            await _employeeRepository.SaveChangesAsync();

            return NoContent();
        }


        [HttpPatch("{employeeId}")]
        public async Task<IActionResult> PartiallyUpdate(Guid employeeId, JsonPatchDocument<EmployeeCreation> patchDocument)
        {
            if (!_employeeRepository.EmployeeExist(employeeId))
            {
                return NotFound();
            }

            var employeeFromRepo = await _employeeRepository.GetEmployee(employeeId);

            if (employeeFromRepo == null)
            {
                return NotFound();
            }

            var employee = _mapper.Map<EmployeeCreation>(employeeFromRepo);

            patchDocument.ApplyTo(employee, ModelState);

            if (!TryValidateModel(employee))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(employee, employeeFromRepo);

            _employeeRepository.UpdateEmployee(employeeFromRepo);

            await _employeeRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{employeeId}")]
        public IActionResult DeleteEmployee(Guid employeeId)
        {
            if (!_employeeRepository.EmployeeExist(employeeId))
            {
                return NotFound("This employee is not Exist");
            }

            _employeeRepository.Delete(employeeId);

            return NoContent();
        }

        [HttpOptions()]
        public IActionResult GetEmployeesOptions()
        {
            Response.Headers.Add("Allow", "Get, Options");
            return Ok();
        }

        [HttpOptions("{employeeId}")]
        public IActionResult GetEmployeeOptions()
        {
            Response.Headers.Add("Allow", "Get, Options, Put, Patch, Delete");
            return Ok();
        }


        private string CreateEmployeeResourceUri(
            EmployeeResourceParameter employeeResourceParameter,
            ResourceUriType resourceUriType)
        {
            switch (resourceUriType)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link("GetEmployees",
                        new
                        {
                            Fields = employeeResourceParameter.Fields,
                            OrderBy = employeeResourceParameter.OrderBy,
                            PageNumber = employeeResourceParameter.PageNumber - 1,
                            employeeResourceParameter.PageSize,
                            employeeResourceParameter.SearchQuery,
                            employeeResourceParameter.JobTitle,
                            employeeResourceParameter.department
                        });
                case ResourceUriType.NextPage:
                    return Url.Link("GetEmployees",
                        new
                        {
                            Fields = employeeResourceParameter.Fields,
                            OrderBy = employeeResourceParameter.OrderBy,
                            PageNumber = employeeResourceParameter.PageNumber + 1,
                            employeeResourceParameter.PageSize,
                            employeeResourceParameter.SearchQuery,
                            employeeResourceParameter.JobTitle,
                            employeeResourceParameter.department
                        });
                default:
                    return Url.Link("GetEmployees",
                        new
                        {
                            Fields = employeeResourceParameter.Fields,
                            OrderBy = employeeResourceParameter.OrderBy,
                            employeeResourceParameter.PageNumber,
                            employeeResourceParameter.PageSize,
                            employeeResourceParameter.SearchQuery,
                            employeeResourceParameter.JobTitle,
                            employeeResourceParameter.department
                        });
            }

        }
    }
}
