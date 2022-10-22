using AutoMapper;
using MediatR;
using StudyBuddy.Application.Dtos;
using StudyBuddy.Application.Interfaces;
using StudyBuddy.Application.Interfaces.Repositories;
using StudyBuddy.Application.Wrappers;

namespace StudyBuddy.Application.Features.Commands.Classroom;

public class CreateClassroomCommand : IRequest<Response<ClassroomDto>>
{
    public string Name { get; set; }
    public List<TagDto> Tags { get; set; }
}

public class CreateClassroomCommandHandler : IRequestHandler<CreateClassroomCommand, Response<ClassroomDto>>
{
    private readonly IGenericRepository<Domain.Entities.Classroom> _repository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateClassroomCommandHandler(IGenericRepository<Domain.Entities.Classroom> repository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<ClassroomDto>> Handle(CreateClassroomCommand request, CancellationToken cancellationToken)
    {
        var classroom = _mapper.Map<Domain.Entities.Classroom>(request);
        var result=await _repository.CreateAsync(classroom);
        await _unitOfWork.CommitAsync();
        var classroomDto = _mapper.Map<ClassroomDto>(result);
        return Response<ClassroomDto>.Success(classroomDto, 201);
    }
}