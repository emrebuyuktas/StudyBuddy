using System.Text;
using System.Text.Json;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using StudyBuddy.Application.Dtos;
using StudyBuddy.Application.Helpers;
using StudyBuddy.Application.Interfaces;
using StudyBuddy.Application.Utils;
using StudyBuddy.Application.Wrappers;
using StudyBuddy.Domain.Entities;

namespace StudyBuddy.Application.Features.Commands.Auth;

public class SignupCommand : IRequest<Response<UserDto>>
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

 public class SignupCommandHandler : IRequestHandler<SignupCommand, Response<UserDto>>
 {
     private readonly UserManager<AppUser> _userManager;
     private readonly IMapper _mapper;
     private readonly ITokenService _tokenService;
     private readonly IDistributedCache _distributedCache;
     private readonly IApplicationDbContext _dbContext;

     //private readonly IHttpClientFactory _httpClientFactory;

     public SignupCommandHandler(UserManager<AppUser> userManager, IMapper mapper, ITokenService tokenService, IDistributedCache distributedCache, 
         IApplicationDbContext dbContext)
     {
         _userManager = userManager;
         _mapper = mapper;
         _tokenService = tokenService;
         _distributedCache = distributedCache;
         _dbContext = dbContext;
     }

     public async Task<Response<UserDto>> Handle(SignupCommand request, CancellationToken cancellationToken)
     {
         var user = _mapper.Map<AppUser>(request);
         var result=await _userManager.CreateAsync(user, request.Password);
         if (!result.Succeeded)
             return Response<UserDto>.Fail(result.Errors.FirstOrDefault().Description, 500);
         await _dbContext.SaveChangesAsync();
         var token = _tokenService.CreateToken(request.Email,user.Id,request.UserName);
         var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(token.RefreshTokenExpiration));
         await _distributedCache.SetAsync($"id:{user.Id}",Encoding.UTF8.GetBytes(token.RefreshToken),options, cancellationToken);
         var userDto = new UserDto
         {
            UserName = request.UserName,
            AccessToken = token.AccessToken,
            AccessTokenExpiration = token.AccessTokenExpiration,
            Email = request.Email
         };
         return Response<UserDto>.Success(userDto, 201);
     }
}