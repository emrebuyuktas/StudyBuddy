using System.Text;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using StudyBuddy.Application.Dtos;
using StudyBuddy.Application.Helpers;
using StudyBuddy.Application.Wrappers;
using StudyBuddy.Domain.Entities;

namespace StudyBuddy.Application.Features.Commands.Auth;

public class CreateTokenCommand :IRequest<Response<TokenDto>>
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class CreateTokenCommandHandler : IRequestHandler<CreateTokenCommand, Response<TokenDto>>
{
    private readonly ITokenService _tokenService;
    private readonly UserManager<AppUser> _userManager;
    private readonly IDistributedCache _distributedCache;
    public CreateTokenCommandHandler(ITokenService tokenService, UserManager<AppUser> userManager, IDistributedCache distributedCache)
    {
        _tokenService = tokenService;
        _userManager = userManager;
        _distributedCache = distributedCache;
    }

    public async Task<Response<TokenDto>> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
            return Response<TokenDto>.Fail(new ErrorDto("User not found"), 404);
        if(!(await _userManager.CheckPasswordAsync(user,request.Password)))
            return Response<TokenDto>.Fail(new ErrorDto("E-mail or password is incorrect"), 404);
        var token = _tokenService.CreateToken(user);
        var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(token.RefreshTokenExpiration));
        await _distributedCache.SetAsync($"id:{user.Id}",Encoding.UTF8.GetBytes(token.RefreshToken),options, cancellationToken);
        return Response<TokenDto>.Success(data: token, 201);
    }
}