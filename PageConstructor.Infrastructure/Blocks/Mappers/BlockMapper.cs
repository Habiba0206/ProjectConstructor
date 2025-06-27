using AutoMapper;
using PageConstructor.Application.Blocks.Models;
using PageConstructor.Domain.Entities;

namespace PageConstructor.Infrastructure.Blocks.Mappers;

public class BlockMapper : Profile
{
    public BlockMapper()
    {
        CreateMap<Block, BlockDto>().ReverseMap();
        CreateMap<Block, BlockPatchDto>().ReverseMap();
    }
}
