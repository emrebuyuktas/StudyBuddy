using System.Text;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using StudyBuddy.Application.Dtos;
using StudyBuddy.Application.Helpers;
using StudyBuddy.Application.Wrappers;
using StudyBuddy.Domain.Entities;

namespace StudyBuddy.Application.Features.Commands.Auth;

public class CreateTokenByRefreshTokenCommand: IRequest<Response<TokenDto>>
{
    public string UserId { get; set; }
}

public class CreateTokenByRefreshTokenCommandHandler : IRequestHandler<CreateTokenByRefreshTokenCommand, Response<TokenDto>>
{
    private readonly IDistributedCache _distributedCache;
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;

    public CreateTokenByRefreshTokenCommandHandler(IDistributedCache distributedCache, UserManager<AppUser> userManager, ITokenService tokenService)
    {
        _distributedCache = distributedCache;
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task<Response<TokenDto>> Handle(CreateTokenByRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var rtoken = await _distributedCache.GetStringAsync($"id:{request.UserId}", token: cancellationToken);
        
        if(string.IsNullOrEmpty(rtoken)) return Response<TokenDto>.Fail("Refresh token not found",404);
        
        var user = await _userManager.FindByIdAsync(request.UserId);
        
        if(user ==null) return Response<TokenDto>.Fail("User not found", 404);
        
        var token =_tokenService.CreateToken(user);
        
        var refreshToken = token.RefreshToken;
        var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(token.RefreshTokenExpiration));
        await _distributedCache.SetAsync($"id:{user.Id}",Encoding.UTF8.GetBytes(refreshToken),options, cancellationToken);
        
        return Response<TokenDto>.Success(token, 201);
    }
}