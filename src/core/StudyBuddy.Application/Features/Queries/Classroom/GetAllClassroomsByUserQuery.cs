using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StudyBuddy.Application.Dtos;
using StudyBuddy.Application.Interfaces;
using StudyBuddy.Application.Utils;
using StudyBuddy.Application.Wrappers;

namespace StudyBuddy.Application.Features.Queries.Classroom;

public class GetAllClassroomsByUserQuery : IRequest<Response<List<UserClassroomDto>>>
{
    public int Take { get; }
    public int Page { get; }
    
    public GetAllClassroomsByUserQuery(int take, int page)
    {
        Take = take;
        Page = page;
    }
}

public class GetAllClassroomsByUserQueryHandler : RequestHandlerBase<GetAllClassroomsByUserQuery, Response<List<UserClassroomDto>>>
{

    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    public GetAllClassroomsByUserQueryHandler(IHttpContextAccessor httpContextAccessor, IApplicationDbContext dbContext, IMapper mapper) : base(httpContextAccessor)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public override async Task<Response<List<UserClassroomDto>>> Handle(GetAllClassroomsByUserQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.UserClassrooms.Where(x => x.UserId == UserId);
        var count = query.Count();
        var rooms = await query.
            Include(x => x.Classroom).ThenInclude(y=>y.Tag).Include(x=>x.AppUser).Skip((request.Page - 1) * request.Take).Take(request.Take).Select(x=>new UserClassroomDto
        {
            ClassroomId = x.ClassroomId.ToString(),
            UserName = x.AppUser.UserName,
            ClassroomName = x.Classroom.Name,
            Tag = _mapper.Map<TagDto>(x.Classroom.Tag),
            UserCount = x.Classroom.Users.Count,
        }).ToListAsync(cancellationToken: cancellationToken);
        return Response<List<UserClassroomDto>>.Success(rooms,200,request.Take,request.Page, (int)Math.Ceiling(((double)count/ request.Take)));
    }
}