using AutoMapper;
using PageConstructor.Application.Components.Models;
using PageConstructor.Domain.Entities;

namespace PageConstructor.Infrastructure.Components.Mappers;

public class ComponentMapper : Profile
{
    public ComponentMapper()
    {
        CreateMap<Component, ComponentDto>().ReverseMap();
        CreateMap<Component, ComponentPatchDto>().ReverseMap();
    }
}