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

        [HttpGet("{departmentId}", Name = "GetDepartment")]
        public async Task<IActionResult> GetDepartment(Guid departmentId)
        {

            if (!_departmentRepository.DepartmentExist(departmentId))
            {
                return NotFound();
            }

            var departmentFromDb = await _departmentRepository.getDepartment(departmentId);

            var department = _mapper.Map<DepartmentDto>(departmentFromDb);

            return Ok(department);
        }

        [HttpPost("manager/{managerId}")]
        public async Task<IActionResult> CreateDrepartment(DepartmentCreation departmentCreation, Guid managerId)
        {
            if (departmentCreation == null)
            {
                throw new ArgumentNullException(nameof(departmentCreation));
            }

            var department = _mapper.Map<Department>(departmentCreation);

            _departmentRepository.CreateDepartment(department, managerId);

            await _departmentRepository.SaveChangesAsync();

            return CreatedAtRoute(
                "GetDepartment",
                new { departmentId = department.DepartmentId },
                departmentCreation
                );
        }

        [HttpPut("{departmentId}")]
        public async Task<IActionResult> UpdateDepartment(DepartmentCreation departmentCreation, Guid departmentId)
        {
            if (departmentCreation == null)
            {
                throw new ArgumentNullException(nameof(departmentCreation));
            }

            if (!_departmentRepository.DepartmentExist(departmentId))
            {
                return NotFound("Please Enter right Department ID");
            }

            var departmentFromRepo = await _departmentRepository.getDepartment(departmentId);

            _mapper.Map(departmentCreation, departmentFromRepo);

            _departmentRepository.Update(departmentFromRepo);

            await _departmentRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{departmentId}")]
        public async Task<IActionResult> partialUpdateDepartment(Guid departmentId, JsonPatchDocument<DepartmentCreation> patchDocument)
        {
            if (!_departmentRepository.DepartmentExist(departmentId))
            {
                return NotFound("Please Enter right Department ID");
            }

            var departmentFromRepo = await _departmentRepository.getDepartment(departmentId);

            if (departmentFromRepo == null)
            {
                return NotFound("this department is not exist");
            }

            var department = _mapper.Map<DepartmentCreation>(departmentFromRepo);

            patchDocument.ApplyTo(department, ModelState);

            if (!TryValidateModel(department))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(department, departmentFromRepo);

            _departmentRepository.Update(departmentFromRepo);

            await _departmentRepository.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete("{departmentId}")]
        public IActionResult DeleteDepartment(Guid departmentId)
        {
            if (!_departmentRepository.DepartmentExist(departmentId))
            {
                return NotFound("Please Enter right Department ID");
            }

            _departmentRepository.Delete(departmentId);

            return NoContent();
        }

    }
}
