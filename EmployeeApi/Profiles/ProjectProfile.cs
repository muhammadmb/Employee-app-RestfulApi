using AutoMapper;
using EmployeeApi.Entities;
using EmployeeApi.Models;
using System.Linq;

namespace EmployeeApi.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectDto>()
                .ForMember(
                    dest => dest.Employees,
                    opt => opt.MapFrom(src => src.employeeProjects
                        .Select(ep => ep.EmployeeId
                        ))
                    );
        }
    }
}
