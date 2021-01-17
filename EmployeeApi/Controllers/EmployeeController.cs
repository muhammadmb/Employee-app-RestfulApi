using AutoMapper;
using EmployeeApi.Models;
using EmployeeApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> getEmployee(Guid employeeId)
        {
            var Employee = await _employeeRepository.GetEmployee(employeeId);

            return Ok(Employee);
        }
    }
}
