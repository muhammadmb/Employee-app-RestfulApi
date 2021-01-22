using AutoMapper;
using EmployeeApi.Entities;
using EmployeeApi.Helper;
using EmployeeApi.Models;
using EmployeeApi.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.Controllers
{

    [ApiController]
    [Route("api/Employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public EmployeeController(
            IEmployeeRepository employeeRepsitory,
            IProjectRepository projectRepository,
            IDepartmentRepository departmentRepository,
            IMapper mapper)
        {
            _employeeRepository = employeeRepsitory ??
                throw new ArgumentNullException(nameof(employeeRepsitory));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _projectRepository = projectRepository ??
                throw new ArgumentNullException(nameof(projectRepository));
            _departmentRepository = departmentRepository ??
                throw new ArgumentNullException(nameof(departmentRepository));
        }

        [HttpGet]
        public async Task<IActionResult> getEmployees()
        {
            var EmployeesFromDb = await _employeeRepository.GetEmployees();
            var employees = _mapper.Map<IEnumerable<EmployeeDto>>(EmployeesFromDb);

            return Ok(employees);
        }

        [HttpGet("{employeeId}", Name = "GetEmployee")]
        public async Task<IActionResult> getEmployee(Guid employeeId)
        {
            if (employeeId == null)
            {
                throw new ArgumentNullException(nameof(employeeId));
            }

            if (!_employeeRepository.EmployeeExist(employeeId))
            {
                return NotFound("This employee is not Exist");
            }

            var EmployeeFromDb = await _employeeRepository.GetEmployee(employeeId);

            var Employee = _mapper.Map<EmployeeDto>(EmployeeFromDb);

            return Ok(Employee);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(EmployeeCreation employeeCreation)
        {
            if (employeeCreation == null)
            {
                throw new ArgumentNullException(nameof(employeeCreation));
            }

            if (!employeeCreation.ProjectId.CheckExistanceOfProjects(_projectRepository))
            {
                return BadRequest("This project is not Exist");
            }

            if (!_departmentRepository.DepartmentExist(employeeCreation.departmentId))
            {
                return BadRequest("this deprtment Id Is not exist");
            }

            var CreatedEmployee = _mapper.Map<Employee>(employeeCreation);

            _employeeRepository.CreateEmployee(CreatedEmployee);

            await _employeeRepository.SaveChangesAsync();

            var returnEmployee = _mapper.Map<ReturnEmployee>(CreatedEmployee);

            return CreatedAtRoute(
                "GetEmployee",
                new { employeeId = CreatedEmployee.EmployeeId },
                returnEmployee
                );
        }

        [HttpPut("{employeeId}")]
        public async Task<IActionResult> UpdateEmployee(Guid employeeId, EmployeeCreation employeeCreation)
        {
            if (employeeId == null)
            {
                throw new ArgumentNullException(nameof(employeeId));
            }

            if (employeeCreation == null)
            {
                throw new ArgumentNullException(nameof(employeeCreation));
            }

            if (!_employeeRepository.EmployeeExist(employeeId))
            {
                return NotFound("This employee is not Exist");
            }

            if (!employeeCreation.ProjectId.CheckExistanceOfProjects(_projectRepository))
            {
                return BadRequest("This project is not Exist");
            }
            
            if (!_departmentRepository.DepartmentExist(employeeCreation.departmentId))
            {
                return BadRequest("this deprtment Id Is not exist");
            }
            
            var employeeFromRepo = await _employeeRepository.GetEmployee(employeeId);

            // if the employee is not exist, we add new employee

            if (employeeFromRepo == null)
            {

                if (!employeeCreation.ProjectId.CheckExistanceOfProjects(_projectRepository))
                {
                    return BadRequest("This project is not Exist");
                }

                var CreatedEmployee = _mapper.Map<Employee>(employeeCreation);
                CreatedEmployee.EmployeeId = employeeId;

                _employeeRepository.CreateEmployee(CreatedEmployee);

                await _employeeRepository.SaveChangesAsync();

                var returnEmployee = _mapper.Map<ReturnEmployee>(CreatedEmployee);

                return CreatedAtRoute(
                    "GetEmployee",
                    new { employeeId = CreatedEmployee.EmployeeId },
                    returnEmployee
                    );
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

            if(employeeFromRepo == null)
            {
                return NotFound();
            }

            var employee = _mapper.Map<EmployeeCreation>(employeeFromRepo);

            patchDocument.ApplyTo(employee, ModelState);

            if (!TryValidateModel(employee))
            {
                return ValidationProblem(ModelState);
            }

            var hasProject = patchDocument.Operations.Any(o => o.path == "/projectId");

            _mapper.Map(employee, employeeFromRepo);

            if (!employee.ProjectId.CheckExistanceOfProjects(_projectRepository))
            {
                return BadRequest("This project is not Exist");
            }

            if (!_departmentRepository.DepartmentExist(employee.departmentId))
            {
                return BadRequest("this deprtment Id Is not exist");
            }

            _employeeRepository.PartialUpdateEmployee(employeeFromRepo, hasProject);

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
    }
}
