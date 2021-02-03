using EmployeeApi.Contexts;
using EmployeeApi.Entities;
using EmployeeApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.Repositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly EmployeeContext _context;

        public StatisticsRepository(EmployeeContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<EmployeeStatistic>> GetDepartmentsStatisics()
        {
            var statistics =
                _context.Employees.ToList()
                .GroupBy(e => e.departmentId);

            var returnstatcs = statistics.Select(g =>
               {
                   var result = g.Aggregate(new EmployeeStatistic(), (acc, e) => acc.Accumulate(e), acc => acc.Compute());

                   return new EmployeeStatistic
                   {
                       DepartmentName = GetDepartmentName(g.Key),
                       EmployeesNumber = result.EmployeesNumber,
                       MaxSalary = result.MaxSalary,
                       MinSalary = result.MinSalary,
                       AgeAverage = result.AgeAverage,
                       SalaryAverage = result.SalaryAverage,
                       TotalSalary = result.TotalSalary
                   };
               }).ToList();

            return returnstatcs;
        }

        private string GetDepartmentName(Guid departmentId)
        {
            return _context.Departments
                .Where(d => d.DepartmentId == departmentId)
                .Select(d => d.DepartmentName)
                .FirstOrDefault();
        }

    }
}
