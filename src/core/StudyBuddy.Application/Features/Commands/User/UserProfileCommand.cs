using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using StudyBuddy.Application.Dtos;
using StudyBuddy.Application.Helpers;
using StudyBuddy.Application.Utils;
using StudyBuddy.Application.Wrappers;
using StudyBuddy.Domain.Entities;

namespace StudyBuddy.Application.Features.Commands.User;

public class UserProfileCommand : IRequest<Response<UserDto>>
{
    public string UserName { get; set; }
    public string Email { get; set; }

    public UserProfileCommand(string userName, string email)
    {
        UserName = userName;
        Email = email;
    }
}

public class UserProfileCommandHandler : RequestHandlerBase<UserProfileCommand, Response<UserDto>>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;
    
    public UserProfileCommandHandler(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, IMapper mapper, ITokenService tokenService) : base(httpContextAccessor)
    {
        _userManager = userManager;
        _mapper = mapper;
        _tokenService = tokenService;
    }

    public override async Task<Response<UserDto>> Handle(UserProfileCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(UserId);
        if(user is null) return Response<UserDto>.Fail("User not found", 404);

        user.UserName = request.UserName;
        user.Email = request.Email;
        var result=await _userManager.UpdateAsync(user);
        var userDto = _mapper.Map<UserDto>(user);
        var token = _tokenService.CreateToken(user.Email, user.Id, user.UserName);
        userDto.AccessToken = token.AccessToken;
        userDto.AccessTokenExpiration = token.AccessTokenExpiration;
        return result.Succeeded ? Response<UserDto>.Success(userDto,201) : 
            Response<UserDto>.Fail("Something went wrong", 401);
    }
}