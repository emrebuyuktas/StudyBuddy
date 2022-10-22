using System.Text;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using StudyBuddy.Application.Dtos;
using StudyBuddy.Application.Helpers;
using StudyBuddy.Application.Wrappers;
using StudyBuddy.Domain.Entities;

namespace StudyBuddy.Application.Features.Commands.Auth;

public class SigninCommand : IRequest<Response<UserDto>>
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class SigninCommandHandler : IRequestHandler<SigninCommand, Response<UserDto>>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IDistributedCache _distributedCache;

    public SigninCommandHandler(UserManager<AppUser> userManager, ITokenService tokenService, IDistributedCache distributedCache)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _distributedCache = distributedCache;
    }

    public async Task<Response<UserDto>> Handle(SigninCommand request, CancellationToken cancellationToken)
    {
        var user =await  _userManager.FindByEmailAsync(request.Email);
        if (user is null)
            return Response<UserDto>.Fail("User not found", 404);
        var token = _tokenService.CreateToken(user.Email, user.Id, user.UserName);
        var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(token.RefreshTokenExpiration));
        await _distributedCache.SetAsync($"id:{user.Id}",Encoding.UTF8.GetBytes(token.RefreshToken),options, cancellationToken);
        var userDto = new UserDto
        {
            UserName = user.UserName,
            AccessToken = token.AccessToken,
            AccessTokenExpiration = token.AccessTokenExpiration,
            Email = request.Email
        };
        return Response<UserDto>.Success(userDto, 201);
    }
}