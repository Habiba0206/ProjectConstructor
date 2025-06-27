using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PageConstructor.Application.Blocks.Models;
using PageConstructor.Application.Blocks.Queries;
using PageConstructor.Application.Blocks.Services;
using PageConstructor.Application.Common.Validators;
using PageConstructor.Domain.Common.Queries;

namespace PageConstructor.Infrastructure.Blocks.QueryHandlers;

public class BlockGetQueryHandler(
    IMapper mapper,
    IBlockService blockService,
    GetQueryValidator validationRules)
    : IQueryHandler<BlockGetQuery, ICollection<BlockDto>>
{
    public async Task<ICollection<BlockDto>> Handle(BlockGetQuery request, CancellationToken cancellationToken)
    {
        var pagination = request.BlockFilter ?? new BlockFilter();
        var validationResult = await validationRules.ValidateAsync(
            pagination);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var result = await blockService.Get(
            request.BlockFilter,
            new QueryOptions()
            {
                QueryTrackingMode = QueryTrackingMode.AsNoTracking
            })
            .ToListAsync(cancellationToken);

        return mapper.Map<ICollection<BlockDto>>(result);
    }
}
