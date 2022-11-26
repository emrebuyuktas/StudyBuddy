using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StudyBuddy.Application.Dtos;
using StudyBuddy.Application.Interfaces;
using StudyBuddy.Application.Utils;
using StudyBuddy.Application.Wrappers;
using IMapper = AutoMapper.IMapper;

namespace StudyBuddy.Application.Features.Queries.Classroom;

public class GetClassroomByUserQuery : IRequest<Response<ClassroomDto>>
{
    public string classroomId { get; set; }
}

public class GetClassroomByUserQueryHandler : RequestHandlerBase<GetClassroomByUserQuery, Response<ClassroomDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _dbContext;
    public GetClassroomByUserQueryHandler( IMapper mapper, IApplicationDbContext dbContext,IHttpContextAccessor contextAccessor) : base(contextAccessor)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }


    public override async Task<Response<ClassroomDto>> Handle(GetClassroomByUserQuery request, CancellationToken cancellationToken)
    {
        var room =  await _dbContext.Classrooms.Where(x => x.Id.ToString() == request.classroomId).
            Include(x=>x.Messages).Include(x => x.Users).ThenInclude(x => x.AppUser).
            SingleOrDefaultAsync(cancellationToken: cancellationToken);
        
        
        var users = new List<UserDto>();
        var joinDate = room.Users.ToList().Find(x => x.UserId == UserId)!.JoinDate;
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
            Tag = _mapper.Map<List<TagDto>>(room.Tags),
            Id = room.Id,
            Name = room.Name
        },200);
    }
}