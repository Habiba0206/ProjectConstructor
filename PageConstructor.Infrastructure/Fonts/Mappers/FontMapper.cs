using AutoMapper;
using PageConstructor.Application.Fonts.Models;
using PageConstructor.Domain.Entities;

namespace PageConstructor.Infrastructure.Fonts.Mappers;

public class FontMapper : Profile
{
    public FontMapper()
    {
        CreateMap<Font, FontDto>().ReverseMap();
        CreateMap<FontWeight, FontWeightDto>().ReverseMap();
    }
}
