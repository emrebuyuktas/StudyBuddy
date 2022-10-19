using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyBuddy.Application.Features.Commands.Auth;
using StudyBuddy.Application.Wrappers;

namespace StudyBuddy.Identity.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : CustomBaseController
    {
        private readonly IMediator _mediator;
        [HttpPost]
        public async Task<IActionResult> CreateToken(CreateTokenCommand createTokenCommand)
        {
            var result=await _mediator.Send(createTokenCommand);
            return ActionResult(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteToken(DeleteRefreshTokenCommand command)
        {
            var result = await _mediator.Send(command);
            return ActionResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTokenByRefreshToken(CreateTokenByRefreshTokenCommand command)
        {
            var result = await _mediator.Send(command);
            return ActionResult(result);
        }
    }
}
