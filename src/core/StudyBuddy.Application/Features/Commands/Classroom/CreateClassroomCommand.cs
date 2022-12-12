using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudyBuddy.Application.Dtos;
using StudyBuddy.Application.Helpers;
using StudyBuddy.Application.Interfaces;
using StudyBuddy.Application.Utils;
using StudyBuddy.Application.Wrappers;
using StudyBuddy.Domain.Entities;
using StudyBuddy.Domain.Entities.MongoDb;
using StudyBuddy.Domain.Entities;

namespace StudyBuddy.Application.Features.Commands.Classroom;

public class CreateClassroomCommand : IRequest<Response<ClassroomDto>>
{
    public string Name { get; set; }
    public TagDto Tag { get; set; }
}

public class CreateClassroomCommandHandler : RequestHandlerBase<CreateClassroomCommand, Response<ClassroomDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _dbContext;
    private readonly IMongoDbRepository<Moderator> _moderatorRepo;
    private readonly UserManager<AppUser> _userManager;
    public CreateClassroomCommandHandler(IMapper mapper, IApplicationDbContext dbContext,IHttpContextAccessor contextAccessor, IMongoDbRepository<Moderator> moderatorRepo, UserManager<AppUser> userManager):base(contextAccessor)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _moderatorRepo = moderatorRepo;
        _userManager = userManager;
    }

    public override async Task<Response<ClassroomDto>> Handle(CreateClassroomCommand request, CancellationToken cancellationToken)
    {
        var classroom = new Domain.Entities.Classroom{Name = request.Name};
        var user = await _userManager.FindByIdAsync(UserId);
        classroom.Tag = await _dbContext.Tags.Where(x =>x.Id==request.Tag.Id).SingleAsync(cancellationToken: cancellationToken);
        var result = await _dbContext.Classrooms.AddAsync(classroom, cancellationToken);
        classroom.Users.Add(new UserClassroom
        {
            UserId = user.Id,
            ClassroomId = classroom.Id,
            Classroom = classroom,
            AppUser = user,
            JoinDate = DateTime.Now
        });
        await _dbContext.SaveChangesAsync();
        await _moderatorRepo.InsertOneAsync(new Moderator { Id = classroom.Id.ToString().ToModeratorId(UserId)});
        var userDtoList = new List<UserDto>();
        foreach (var classroomUser in classroom.Users)
        {
            userDtoList.Add(_mapper.Map<UserDto>(classroomUser.AppUser));
        }
        var classroomDto = new ClassroomDto
        {
            AppUsers =userDtoList,
            Messages = _mapper.Map<List<MessageDto>>(classroom.Messages),
            Id = classroom.Id,
            Name = classroom.Name,
            Tag = _mapper.Map<TagDto>(classroom.Tag)
        };
        return Response<ClassroomDto>.Success(classroomDto, 201);
    }
    
}