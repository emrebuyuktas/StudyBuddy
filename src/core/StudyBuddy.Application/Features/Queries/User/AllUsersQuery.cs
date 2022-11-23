using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudyBuddy.Application.Dtos;
using StudyBuddy.Application.Wrappers;
using StudyBuddy.Domain.Entities;

namespace StudyBuddy.Application.Features.Queries.User;

public class AllUsersQuery : IRequest<Response<List<UserDto>>>
{
    public int Take { get; }
    public int Page { get; }

    public AllUsersQuery(int take, int page)
    {
        Take = take;
        Page = page;
    }
}

public class AllUsersQueryHandler : IRequestHandler<AllUsersQuery, Response<List<UserDto>>>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;

    public AllUsersQueryHandler(UserManager<AppUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public  async Task<Response<List<UserDto>>> Handle(AllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userManager.Users.Skip((request.Page - 1) * request.Take).Take(request.Take).ToListAsync(cancellationToken: cancellationToken);
        var userDtos = _mapper.Map<List<UserDto>>(users);
        return Response<List<UserDto>>.Success(userDtos, 200,request.Take,request.Page,users.Count);
    }
}