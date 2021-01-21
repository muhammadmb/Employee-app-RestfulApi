using AutoMapper;
using EmployeeApi.Entities;
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

        public EmployeeController(IEmployeeRepository employeeRepsitory, IMapper mapper)
        {
            _employeeRepository = employeeRepsitory ??
                throw new ArgumentNullException(nameof(employeeRepsitory));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
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

            var EmployeeFromDb = await _employeeRepository.GetEmployee(employeeId);

            var Employee = _mapper.Map<EmployeeDto>(EmployeeFromDb);

            return Ok(Employee);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(EmployeeCreation employeeCreation)
        {
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

            var employeeFromRepo = await _employeeRepository.GetEmployee(employeeId);

            // if the employee is not exist, we add new employee

            if (employeeFromRepo == null)
            {
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

            _employeeRepository.PartialUpdateEmployee(employeeFromRepo, hasProject);

            await _employeeRepository.SaveChangesAsync();

            return NoContent();
        }

    }
}
