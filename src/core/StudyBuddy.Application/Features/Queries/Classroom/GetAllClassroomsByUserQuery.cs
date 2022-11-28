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
        var rooms = ( _dbContext.UserClassrooms.Where(x => x.UserId == UserId).
            Include(x => x.Classroom).ThenInclude(y=>y.Tag).Include(x=>x.AppUser)).Select(x=>new UserClassroomDto
        {
            UserName = x.AppUser.UserName,
            ClassroomName = x.Classroom.Name,
            Tag = _mapper.Map<TagDto>(x.Classroom.Tag)
        });
        return Response<List<UserClassroomDto>>.Success(await rooms.ToListAsync(cancellationToken: cancellationToken),200);
    }
}