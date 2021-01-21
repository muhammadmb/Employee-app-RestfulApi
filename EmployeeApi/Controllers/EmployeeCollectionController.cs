using AutoMapper;
using EmployeeApi.Entities;
using EmployeeApi.ModelBinders;
using EmployeeApi.Models;
using EmployeeApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.Controllers
{
    [ApiController]
    [Route("api/EmployeesCollection")]
    public class EmployeeCollectionController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeCollectionController(IEmployeeRepository employeeRepsitory, IMapper mapper)
        {
            _employeeRepository = employeeRepsitory ??
                throw new ArgumentNullException(nameof(employeeRepsitory));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("{EmployeesIds}", Name = "GetCollectionOfEmployees")]
        public async Task<IActionResult> getSpecificEmployees(
            [ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> EmployeesIds)
        {
            var EmployeesFromDb = await _employeeRepository.GetEmployees(EmployeesIds);
            var employees = _mapper.Map<IEnumerable<EmployeeDto>>(EmployeesFromDb);

            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployees(IEnumerable<EmployeeCreation> employeeCreations)
        {
            var employees = _mapper.Map<IEnumerable<Employee>>(employeeCreations);

            foreach (var employee in employees)
            {
                _employeeRepository.CreateEmployee(employee);
            }

            await _employeeRepository.SaveChangesAsync();

            var getEmployees = await _employeeRepository.GetEmployees(
                    employees.Select(e => e.EmployeeId).ToList()
                );

            var returnEmployees = _mapper.Map<IEnumerable<ReturnEmployee>>(getEmployees);

            var EmployeesIds = string.Join(",", employees.Select(e => e.EmployeeId));

            return CreatedAtRoute(
                "GetCollectionOfEmployees",
                new { EmployeesIds },
                returnEmployees
                );
        }
    }
}
