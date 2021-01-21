using AutoMapper;
using EmployeeApi.Entities;
using EmployeeApi.Helper;
using EmployeeApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeApi.Profiles
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentDto>()
                .ForMember(
                    dest => dest.Employees,
                    opt => opt.MapFrom(src => src.Employees.GetEmployeesName())
                );
        }
    }
}
