using AutoMapper;
using EmployeeApi.Entities;
using EmployeeApi.Helper;
using EmployeeApi.Models;

namespace EmployeeApi.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => $"{ src.FirstName} {src.LastName }")
                )
                .ForMember(
                    dest => dest.age,
                    opt => opt.MapFrom(src => src.DateOfBirth.GetCurrentAge())
                )
                .ForMember(
                dest => dest.department,
                opt => opt.MapFrom(src => src.department.DepartmentName)
                )
                .ForMember(
                    dest => dest.Projects,
                    opt => opt.MapFrom(src => src.employeeProjects.GetProjectsNames())
                );

            CreateMap<Employee, ReturnEmployee>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => $"{ src.FirstName} {src.LastName }")
                );

            CreateMap<EmployeeCreation, Employee>();
            CreateMap<Employee, EmployeeCreation>();
        }
    }
}
