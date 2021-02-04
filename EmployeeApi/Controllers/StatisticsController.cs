using EmployeeApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EmployeeApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsRepository _statisticsRepository;

        public StatisticsController(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository ??
                throw new ArgumentNullException(nameof(statisticsRepository));
        }

        [HttpGet("Departments/Statistics")]
        [HttpHead("Departments/Statistics")]

        public async Task<IActionResult> GetEmployeesStatistics()
        {
            return Ok(await _statisticsRepository.GetDepartmentsStatisics());
        }


    }
}
