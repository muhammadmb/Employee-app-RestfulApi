using AutoMapper;
using EmployeeApi.Helper;

namespace EmployeeApi.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Entities.Employee, Models.EmployeeDto>()
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
                )
                ;

        }
    }
}
