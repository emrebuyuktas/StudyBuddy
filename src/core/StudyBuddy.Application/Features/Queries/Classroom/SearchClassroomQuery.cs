using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StudyBuddy.Application.Dtos;
using StudyBuddy.Application.Interfaces;
using StudyBuddy.Application.Utils;
using StudyBuddy.Application.Wrappers;

namespace StudyBuddy.Application.Features.Queries.Classroom;

public class SearchClassroomQuery : IRequest<Response<List<ClassroomDto>>>
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

public class SearchClassroomQueryHandler : RequestHandlerBase<SearchClassroomQuery, Response<List<ClassroomDto>>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    public SearchClassroomQueryHandler(IHttpContextAccessor httpContextAccessor, IApplicationDbContext dbContext, IMapper mapper) : base(httpContextAccessor)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public override async Task<Response<List<ClassroomDto>>> Handle(SearchClassroomQuery request, CancellationToken cancellationToken)
    {
        var query =
             _dbContext.Classrooms.Where(x =>
                    x.Name.Contains(request.Key))
                .Skip((request.Page - 1) * request.Take).Take(request.Take);
        var classrooms = await query.ToListAsync(cancellationToken: cancellationToken);
        var classroomDto = _mapper.Map<List<ClassroomDto>>(classrooms);
        return Response<List<ClassroomDto>>.Success(classroomDto,200,request.Take,request.Page,classrooms.Count);
    }
}