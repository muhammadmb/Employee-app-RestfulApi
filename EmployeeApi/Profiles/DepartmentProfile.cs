using AutoMapper;
using EmployeeApi.Entities;
using EmployeeApi.Helper;
using EmployeeApi.Models;

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

            CreateMap<DepartmentCreation, Department>();
            CreateMap<Department, DepartmentCreation>();
        }
    }
}
