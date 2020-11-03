using AutoMapper;
using EmployeeWevService.Entities;
using EmployeeWevService.Filters;
using EmployeeWevService.Models;
using EmployeeWevService.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeWevService.Controllers
{
    [ApiController]
    [Route("api/Employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepo;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepo = employeeRepository ??
                throw new ArgumentNullException(nameof(employeeRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [EmployeesResultFilter]
        public async Task<IActionResult> GetEmployees()
        {
            return Ok(await _employeeRepo.GetEmployees());
        }

        [HttpGet("{employeeId}", Name = "GetEmployee")]
        public async Task<IActionResult> GetEmployee(Guid employeeId)
        {
            if(employeeId == null)
            {
                throw new ArgumentNullException(nameof(employeeId));
            }

            var BookEntity = await _employeeRepo.GetEmployee(employeeId);

            var BookReturn = _mapper.Map<EmployeeDTO>(BookEntity);

            return Ok(BookReturn);
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeCreation employeeCreation )
        {
            var EmployeeEntity = _mapper.Map<Employee>(employeeCreation);

            _employeeRepo.CreateEmployee(EmployeeEntity);

            await _employeeRepo.SaveChangesAsync();

            var EmployeeToReturn = _mapper.Map<EmployeeDTO> (EmployeeEntity); 

            return CreatedAtRoute(
                "GetEmployee",
                new {employeeId = EmployeeEntity.EmployeeId},
                EmployeeToReturn
                );

        }
    }
}
