using System.Text.Json;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using StudyBuddy.Application.Dtos;
using StudyBuddy.Application.Wrappers;
using StudyBuddy.Domain.Entities;

namespace StudyBuddy.Application.Features.Commands.User;

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
     //private readonly IHttpClientFactory _httpClientFactory;

     public SignupCommandHandler(UserManager<AppUser> userManager, IMapper mapper)
     {
         _userManager = userManager;
         _mapper = mapper;
         //_httpClientFactory = httpClientFactory;
     }

     public async Task<Response<UserDto>> Handle(SignupCommand request, CancellationToken cancellationToken)
     {
         var user = _mapper.Map<AppUser>(request);
         var result=await _userManager.CreateAsync(user, request.Password);
         if (!result.Succeeded)
             return Response<UserDto>.Fail("An error occurred while registering the user.", 500);
         // var client = _httpClientFactory.CreateClient();
         // var res = (await client.SendAsync(new HttpRequestMessage(HttpMethod.Post, "api/Auth/CreateToken"), cancellationToken));
         // var response = await JsonSerializer.DeserializeAsync<Response<TokenDto>>
         //     (await res.Content.ReadAsStreamAsync(cancellationToken), cancellationToken: cancellationToken);
         // if (response.Error.Errors.Any())
         //     return Response<UserDto>.Fail(errorDto: response.Error, response.StatusCode);
         // var userDto = new UserDto
         // {
         //     AccessToken = response.Data.AccessToken,
         //     Email = request.Email,
         //     AccessTokenExpiration = response.Data.AccessTokenExpiration,
         //     UserName = request.Email
         // };
         // return Response<UserDto>.Success(userDto, 201);
         return null;
     }
}