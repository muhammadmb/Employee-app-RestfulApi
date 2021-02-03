using EmployeeApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeApi.Repositories
{
    public interface IStatisticsRepository
    {
        Task<IEnumerable<EmployeeStatistic>> GetDepartmentsStatisics();
    }
}
