using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StudyBuddy.Application.Dtos;
using StudyBuddy.Application.Interfaces;
using StudyBuddy.Application.Utils;
using StudyBuddy.Application.Wrappers;

namespace StudyBuddy.Application.Features.Queries.Classroom;

public class SearchClassroomQuery : IRequest<Response<List<ClassroomListDto>>>
{
    public int Take { get; }
    public int Page { get; }

    public string Key { get; set; }

    public SearchClassroomQuery(int take, int page, string key)
    {
        Take = take;
        Page = page;
        Key = key;
    }
}

public class SearchClassroomQueryHandler : RequestHandlerBase<SearchClassroomQuery, Response<List<ClassroomListDto>>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    public SearchClassroomQueryHandler(IHttpContextAccessor httpContextAccessor, IApplicationDbContext dbContext, IMapper mapper) : base(httpContextAccessor)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public override async Task<Response<List<ClassroomListDto>>> Handle(SearchClassroomQuery request, CancellationToken cancellationToken)
    {
        var query =
            _dbContext.Classrooms.Where(x =>
                x.Name.Contains(request.Key) || x.Tag.Name.Contains(request.Key));
        var rooms = await query
            .Skip(request.Take * (request.Page-1))
            .Take(request.Take).Select(x => new ClassroomListDto
        {
            Id = x.Id.ToString(),
            Name = x.Name,
            Tag = _mapper.Map<TagDto>(x.Tag)
        }).ToListAsync(cancellationToken);
        //var classrooms = await query.ToListAsync(cancellationToken: cancellationToken);
        return Response<List<ClassroomListDto>>.Success(rooms,200,request.Take,request.Page,query.Count());
    }
}