using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudyBuddy.Application.Dtos;
using StudyBuddy.Application.Interfaces.Repositories;
using StudyBuddy.Application.Wrappers;
using StudyBuddy.Domain.Entities;
using IMapper = AutoMapper.IMapper;

namespace StudyBuddy.Application.Features.Queries.Classroom;

public class GetAllClassroomsByUserQuery : IRequest<Response<List<UserClassroomDto>>>
{
    public string UserId { get; set; }
}

public class GetAllClassroomsByUserQueryHandler : IRequestHandler<GetAllClassroomsByUserQuery, Response<List<UserClassroomDto>>>
{
    private readonly IGenericRepository<UserClassroom> _genericRepository;

    public GetAllClassroomsByUserQueryHandler(IGenericRepository<UserClassroom> genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<Response<List<UserClassroomDto>>> Handle(GetAllClassroomsByUserQuery request, CancellationToken cancellationToken)
    {
        var rooms = (await _genericRepository.GetAllAsync(x => x.UserId == request.UserId, 
            x => x.Classroom,x=>x.AppUser)).Select(x=>new UserClassroomDto
        {
            UserName = x.AppUser.UserName,
            ClassroomName = x.Classroom.Name
        });
        return  Response<List<UserClassroomDto>>.Success(rooms.ToList(),200);
    }
}