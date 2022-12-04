using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyBuddy.Application.Dtos;
using StudyBuddy.Application.Features.Commands.Message;
using StudyBuddy.Application.Wrappers;

namespace StudyBuddy.API.Controllers.Message
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MessageController : CustomBaseController
    {
        private readonly IMediator _mediator;

        public MessageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("save")]
        public async Task<IActionResult> SaveMessage(SaveMessageDto saveMessageDto)
        {
            return ActionResult(await _mediator.Send(new SaveMessageCommand(saveMessageDto.ClassroomId,saveMessageDto.Content,saveMessageDto.CreatedDate)));
        }
    }
}
