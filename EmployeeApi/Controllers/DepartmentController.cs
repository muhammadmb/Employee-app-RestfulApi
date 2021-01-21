using AutoMapper;
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
    [Route("api/departments")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository ??
                throw new ArgumentNullException(nameof(departmentRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            var departmentsFromDb = await _departmentRepository.getDepartments();

            var departments = _mapper.Map<IEnumerable<DepartmentDto>>(departmentsFromDb);

            return Ok(departments);
        }

        [HttpGet("{departmentId}")]
        public async Task<IActionResult> GetDepartment(Guid departmentId)
        {
            var departmentFromDb = await _departmentRepository.getDepartment(departmentId);

            var department = _mapper.Map<DepartmentDto>(departmentFromDb);

            return Ok(department);
        }
    }
}
