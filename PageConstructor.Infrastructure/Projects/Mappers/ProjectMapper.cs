using AutoMapper;
using PageConstructor.Application.Pages.Models;
using PageConstructor.Application.Projects.Models;
using PageConstructor.Domain.Entities;

namespace PageConstructor.Infrastructure.Projects.Mappers;

public class ProjectMapper : Profile
{
    public ProjectMapper()
    {
        CreateMap<Project, ProjectDto>().ReverseMap();
        CreateMap<Project, ProjectPatchDto>().ReverseMap();
    }
}

