using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StudyBuddy.Application.Dtos;
using StudyBuddy.Application.Interfaces;
using StudyBuddy.Application.Utils;
using StudyBuddy.Application.Wrappers;

namespace StudyBuddy.Application.Features.Queries.Tag;

public class GetTagsQuery : IRequest<Response<List<TagDto>>>
{
    
}

public class GetTagsQueryHandler : RequestHandlerBase<GetTagsQuery, Response<List<TagDto>>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    public GetTagsQueryHandler(IHttpContextAccessor httpContextAccessor, IApplicationDbContext dbContext, IMapper mapper) : base(httpContextAccessor)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public override async Task<Response<List<TagDto>>> Handle(GetTagsQuery request, CancellationToken cancellationToken)
    {
        var tags=await _dbContext.Tags.ToListAsync(cancellationToken: cancellationToken);
        return Response<List<TagDto>>.Success(_mapper.Map<List<TagDto>>(tags),200);
    }
}