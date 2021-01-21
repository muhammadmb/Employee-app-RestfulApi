using EmployeeApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.Helper
{
    public static class GetNamesExtension
    {
        public static IEnumerable<string> GetEmployeesName(this IEnumerable<Employee> employees)
        {
            foreach (var emp in employees)
            {
                yield return $"{emp.FirstName} {emp.LastName}"; 
            }
        }
    }
}
