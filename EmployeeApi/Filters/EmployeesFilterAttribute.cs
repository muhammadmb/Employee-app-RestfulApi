using AutoMapper;
using EmployeeApi.Helper;
using EmployeeApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.Filters
{
    public class EmployeesFilterAttribute : ResultFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var resultForAction = context.Result as ObjectResult;

            if(resultForAction?.Value == null
                || resultForAction.StatusCode < 200
                || resultForAction.StatusCode >= 300
                )
            {
                await next();
                return;
            }

            var mapper = context.HttpContext.RequestServices.GetRequiredService<IMapper>();

            resultForAction.Value = mapper.Map<IEnumerable<EmployeeDto>>(resultForAction.Value);

            await next();
        }
    }
}
