using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
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
    public List<Tags> Tags { get; set; }
}

public class CreateClassroomCommandHandler : RequestHandlerBase<CreateClassroomCommand, Response<ClassroomDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _dbContext;
    private readonly IMongoDbRepository<Moderator> _moderatorRepo;
    public CreateClassroomCommandHandler(IMapper mapper, IApplicationDbContext dbContext,IHttpContextAccessor contextAccessor, IMongoDbRepository<Moderator> moderatorRepo):base(contextAccessor)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _moderatorRepo = moderatorRepo;
    }

    public override async Task<Response<ClassroomDto>> Handle(CreateClassroomCommand request, CancellationToken cancellationToken)
    {
        var classroom = new Domain.Entities.Classroom{Name = request.Name};
        classroom.Tags = await _dbContext.Tags.Where(x=>request.Tags.Contains((Tags)x.Id)).ToListAsync(cancellationToken: cancellationToken);
        var result = await _dbContext.Classrooms.AddAsync(classroom, cancellationToken);
        await _dbContext.SaveChangesAsync();
        await _moderatorRepo.InsertOneAsync(new Moderator { Id = classroom.Id.ToString().ToModeratorId(UserId)});
        var classroomDto = new ClassroomDto
        {
            AppUsers = _mapper.Map<List<UserDto>>(classroom.Users),
            Messages = _mapper.Map<List<MessageDto>>(classroom.Messages),
            Id = classroom.Id,
            Name = classroom.Name,
            Tag = _mapper.Map<List<TagDto>>(classroom.Tags)
        };
        return Response<ClassroomDto>.Success(classroomDto, 201);
    }
    
}