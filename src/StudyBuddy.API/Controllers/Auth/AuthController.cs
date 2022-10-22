using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyBuddy.Application.Features.Commands.Auth;
using StudyBuddy.Application.Wrappers;

namespace StudyBuddy.API.Controllers.Auth
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : CustomBaseController
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        public async Task<IActionResult> Signup(SignupCommand signupCommand)
        {
            var result=await _mediator.Send(signupCommand);
            return ActionResult(result);
        }
        [HttpPost]
        public async Task<IActionResult> Signin(SigninCommand signinCommand)
        {
            var result=await _mediator.Send(signinCommand);
            return ActionResult(result);
        }
        [HttpDelete]
        public async Task<IActionResult> Signout(SignoutCommand signoutCommand)
        {
            var result=await _mediator.Send(signoutCommand);
            return ActionResult(result);
        }
    }
}
