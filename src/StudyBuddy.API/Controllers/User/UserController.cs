using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyBuddy.Application.Dtos;
using StudyBuddy.Application.Features.Commands.User;
using StudyBuddy.Application.Features.Queries.User;
using StudyBuddy.Application.Wrappers;

namespace StudyBuddy.API.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : CustomBaseController
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all/{take}/{page}")]
        public async Task<IActionResult> GetAllUsers(int take,int page) => ActionResult(await _mediator.Send(new AllUsersQuery(take,page)));
        
        [HttpPost("update")]
        public async Task<IActionResult> UpdateUser(UpdateUserDto userDto) => 
            ActionResult(await _mediator.Send(new UserProfileCommand(userDto.Name,userDto.Email)));
    }
}
