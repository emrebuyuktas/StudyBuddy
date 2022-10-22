using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using StudyBuddy.Application.Dtos;
using StudyBuddy.Application.Interfaces;
using StudyBuddy.Application.Interfaces.Repositories;
using StudyBuddy.Application.Wrappers;
using StudyBuddy.Domain.Entities;

namespace StudyBuddy.Application.Features.Commands.Classroom;

public class JoinClassroomCommand : IRequest<Response<ClassroomDto>>
{
    public Guid ClassroomId { get; set; }
    public string UserId { get; set; }
}

public class JoinClassroomCommandHandler : IRequestHandler<JoinClassroomCommand, Response<ClassroomDto>>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IGenericRepository<Domain.Entities.Classroom> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public JoinClassroomCommandHandler(UserManager<AppUser> userManager, IGenericRepository<Domain.Entities.Classroom> repository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _userManager = userManager;
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Response<ClassroomDto>> Handle(JoinClassroomCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        var classroom = await _repository.GetAsync(x => x.Id == request.ClassroomId,x=>x.Users);
        if (user is null || classroom is null)
            return Response<ClassroomDto>.Fail("Something went wrong", 404);
        classroom.Users.Add(user);
        await _unitOfWork.CommitAsync();
        return Response<ClassroomDto>.Success(_mapper.Map<ClassroomDto>(classroom), 200);
    }
}