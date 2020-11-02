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

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepo = employeeRepository ??
                throw new ArgumentNullException(nameof(employeeRepository));
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            return Ok(await _employeeRepo.GetEmployees());
        }
    }
}
