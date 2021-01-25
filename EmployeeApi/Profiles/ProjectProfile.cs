using AutoMapper;
using EmployeeApi.Entities;
using EmployeeApi.Helper;
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
                        .Select(ep => new ReturnEmployee
                        {
                            Name = ep.employee.FirstName + " " + ep.employee.LastName,
                            Email = ep.employee.Email,
                            JobTitle = ep.employee.JobTitle,
                            PhoneNumber = ep.employee.PhoneNumber,
                            Age = ep.employee.DateOfBirth.GetCurrentAge()

                        }))
                    );
            CreateMap<ProjectCreation, Project>();
            CreateMap<Project, ProjectCreation>();
        }
    }
}
