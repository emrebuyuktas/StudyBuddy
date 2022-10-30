using MediatR;
using Microsoft.EntityFrameworkCore;
using StudyBuddy.Application.Dtos;
using StudyBuddy.Application.Interfaces;
using StudyBuddy.Application.Wrappers;

namespace StudyBuddy.Application.Features.Queries.Classroom;

public class GetAllClassroomsByUserQuery : IRequest<Response<List<UserClassroomDto>>>
{
    public string UserId { get; set; }
}

public class GetAllClassroomsByUserQueryHandler : IRequestHandler<GetAllClassroomsByUserQuery, Response<List<UserClassroomDto>>>
{

    private readonly IApplicationDbContext _dbContext;
    public GetAllClassroomsByUserQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response<List<UserClassroomDto>>> Handle(GetAllClassroomsByUserQuery request, CancellationToken cancellationToken)
    {
        var rooms = ( _dbContext.UserClassrooms.Where(x => x.UserId == request.UserId).
                Include(x => x.Classroom).Include(x=>x.AppUser)).Select(x=>new UserClassroomDto
        {
            UserName = x.AppUser.UserName,
            ClassroomName = x.Classroom.Name
        });
        return  Response<List<UserClassroomDto>>.Success(await rooms.ToListAsync(cancellationToken: cancellationToken),200);
    }
}