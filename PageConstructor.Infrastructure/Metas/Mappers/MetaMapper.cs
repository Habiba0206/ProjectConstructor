using AutoMapper;
using PageConstructor.Application.Metas.Models;
using PageConstructor.Domain.Entities;

namespace PageConstructor.Infrastructure.Metas.Mappers;

public class MetaMapper : Profile
{
    public MetaMapper()
    {
        CreateMap<Meta, MetaDto>().ReverseMap();
        CreateMap<Meta, MetaPatchDto>().ReverseMap();
    }
}
