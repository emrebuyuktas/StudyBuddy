using MediatR;
using Microsoft.AspNetCore.Identity;
using StudyBuddy.Application.Dtos;
using StudyBuddy.Application.Interfaces.Repositories;
using StudyBuddy.Application.Wrappers;
using StudyBuddy.Domain.Entities;
using IMapper = AutoMapper.IMapper;

namespace StudyBuddy.Application.Features.Queries.Classroom;

public class GetClassroomByUserQuery : IRequest<Response<ClassroomDto>>
{
    public string classroomId { get; set; }
    public string UserId { get; set; }
}

public class GetClassroomByUserQueryHandler : IRequestHandler<GetClassroomByUserQuery, Response<ClassroomDto>>
{
    private readonly IClassroomRepository _classroomRepository;
    private readonly IMapper _mapper;
    public GetClassroomByUserQueryHandler( IMapper mapper, IClassroomRepository classroomRepository)
    {
        _mapper = mapper;
        _classroomRepository = classroomRepository;
    }

    public async Task<Response<ClassroomDto>> Handle(GetClassroomByUserQuery request, CancellationToken cancellationToken)
    {
        var room = await _classroomRepository.GetClassWithUsersAsync(request.classroomId);
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
            Tags = _mapper.Map<List<TagDto>>(room.Tags),
            Id = room.Id,
            Name = room.Name
        },200);
    }
}