using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.Helper
{
    public static class DateTimeOffsetExtention
    {
        public static int GetCurrentAge(this DateTimeOffset dataTimeOffset)
        {
            var currentDate = DateTime.UtcNow;
            int age = currentDate.Year - dataTimeOffset.Year;
            if (currentDate < dataTimeOffset.AddYears(age))
            {
                age--;
            }
            return age;
        }
    }
}
