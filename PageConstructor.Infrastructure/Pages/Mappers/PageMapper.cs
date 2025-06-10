using AutoMapper;
using PageConstructor.Application.Pages.Models;
using PageConstructor.Domain.Entities;

namespace PageConstructor.Infrastructure.Pages.Mappers;

public class PageMapper : Profile
{
    public PageMapper()
    {
        CreateMap<Page, PageDto>().ReverseMap();
    }
}
