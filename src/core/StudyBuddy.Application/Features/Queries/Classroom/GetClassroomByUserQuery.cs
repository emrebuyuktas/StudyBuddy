using MediatR;
using Microsoft.EntityFrameworkCore;
using StudyBuddy.Application.Dtos;
using StudyBuddy.Application.Interfaces;
using StudyBuddy.Application.Wrappers;
using IMapper = AutoMapper.IMapper;

namespace StudyBuddy.Application.Features.Queries.Classroom;

public class GetClassroomByUserQuery : IRequest<Response<ClassroomDto>>
{
    public string classroomId { get; set; }
    public string UserId { get; set; }
}

public class GetClassroomByUserQueryHandler : IRequestHandler<GetClassroomByUserQuery, Response<ClassroomDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _dbContext;
    public GetClassroomByUserQueryHandler( IMapper mapper, IApplicationDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<Response<ClassroomDto>> Handle(GetClassroomByUserQuery request, CancellationToken cancellationToken)
    {
        var room =  await _dbContext.Classrooms.Where(x => x.Id.ToString() == request.classroomId).Include(x=>x.Messages).Include(x=>x.Tag).
            Include(x => x.Users).ThenInclude(x => x.AppUser).
            SingleOrDefaultAsync(cancellationToken: cancellationToken);
        
        
        var users = new List<UserDto>();
        var joinDate = room.Users.ToList().Find(x => x.UserId == request.UserId)!.JoinDate;
        var test = room.Users.ToList();
        if(test.Any())
            Console.WriteLine("x");
        room.Users.ToList().ForEach(x =>
        {
            var mapped = _mapper.Map<UserDto>(x.AppUser);
            users.Add(_mapper.Map<UserDto>(x.AppUser));
        });
        var messages = room.Messages.Where(x => x.CreatedDate > joinDate).Select(x=>new MessageDto
        {
            Content = x.Content,
            CreatedDate = x.CreatedDate,
            UserName = x.User.UserName
        }).ToList();
        return Response<ClassroomDto>.Success(new ClassroomDto
        {
            AppUsers = users,
            Messages = messages,
            Tag = room.Tag,
            Id = room.Id,
            Name = room.Name
        },200);
    }
}