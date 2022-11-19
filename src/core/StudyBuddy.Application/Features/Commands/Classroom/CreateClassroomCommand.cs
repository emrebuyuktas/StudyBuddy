using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using StudyBuddy.Application.Dtos;
using StudyBuddy.Application.Interfaces;
using StudyBuddy.Application.Utils;
using StudyBuddy.Application.Wrappers;
using StudyBuddy.Domain.Entities.MongoDb;

namespace StudyBuddy.Application.Features.Commands.Classroom;

public class CreateClassroomCommand : IRequest<Response<ClassroomDto>>
{
    public string Name { get; set; }
    public List<TagDto> Tags { get; set; }
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
        var classroom = _mapper.Map<Domain.Entities.Classroom>(request);
        var result = await _dbContext.Classrooms.AddAsync(classroom, cancellationToken);
        await _dbContext.SaveChangesAsync();
        await _moderatorRepo.InsertOneAsync(new Moderator { Id = classroom.Id.ToString().ToModeratorId(UserId)});
        var classroomDto = _mapper.Map<ClassroomDto>(result);
        return Response<ClassroomDto>.Success(classroomDto, 201);
    }
}