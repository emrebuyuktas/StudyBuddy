﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudyBuddy.Application.Dtos;
using StudyBuddy.Application.Interfaces;
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
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _dbContext;
    public JoinClassroomCommandHandler(UserManager<AppUser> userManager, IMapper mapper, IApplicationDbContext dbContext)
    {
        _userManager = userManager;
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<Response<ClassroomDto>> Handle(JoinClassroomCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        var classroom = await _dbContext.Classrooms.Where(x => x.Id == request.ClassroomId).Include(x=>x.Users)
            .Include(x=>x.Messages).FirstOrDefaultAsync(cancellationToken: cancellationToken);
        if (user is null || classroom is null)
            return Response<ClassroomDto>.Fail("Something went wrong", 404);
        classroom.Users.Add(new UserClassroom
        {
            UserId = user.Id,
            ClassroomId = classroom.Id,
            Classroom = classroom,
            AppUser = user
        });
        await _dbContext.SaveChangesAsync();
        var userDtoList = new List<UserDto>();
        foreach (var classroomUser in classroom.Users)
        {
            userDtoList.Add(_mapper.Map<UserDto>(classroomUser.AppUser));
        }
        return Response<ClassroomDto>.Success(new ClassroomDto
        {
            Id = classroom.Id,
            Name = classroom.Name,
            Tags = _mapper.Map<List<TagDto>>(classroom.Tags),
            AppUsers = userDtoList

        }, 200);
    }
}