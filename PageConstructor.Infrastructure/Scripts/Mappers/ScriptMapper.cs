using AutoMapper;
using PageConstructor.Application.Scripts.Models;
using PageConstructor.Domain.Entities;

namespace PageConstructor.Infrastructure.Scripts.Mappers;

public class ScriptMapper : Profile
{
    public ScriptMapper()
    {
        CreateMap<Script, ScriptDto>().ReverseMap();
    }
}
