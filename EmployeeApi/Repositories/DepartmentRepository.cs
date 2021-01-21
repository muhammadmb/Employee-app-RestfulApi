﻿using EmployeeApi.Contexts;
using EmployeeApi.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.Services
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private EmployeeContext _context;

        public DepartmentRepository(EmployeeContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<Department>> getDepartments()
        {
            return await _context.Departments
                .Include(d => d.Employees)
                .ToListAsync();
        }

        public async Task<Department> getDepartment(Guid departmentId)
        {
            return await _context.Departments
                .Where(d => d.DepartmentId == departmentId)
                .Include(d => d.Employees)
                .FirstOrDefaultAsync();
        }

        
    }
}
