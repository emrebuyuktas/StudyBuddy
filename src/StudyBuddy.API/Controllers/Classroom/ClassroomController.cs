using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyBuddy.Application.Dtos;
using StudyBuddy.Application.Features.Commands.Classroom;
using StudyBuddy.Application.Features.Queries.Classroom;
using StudyBuddy.Application.Wrappers;

namespace StudyBuddy.API.Controllers.Classroom
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClassroomController : CustomBaseController
    {
        private readonly IMediator _mediator;

        public ClassroomController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost("create")]
        public async Task<IActionResult> CreateClass(CreateClassroomDto createClassroomDto) =>
            ActionResult(await _mediator.Send(new CreateClassroomCommand(createClassroomDto.Name,createClassroomDto.Tag)));
        
        [HttpPost("join")]
        public async Task<IActionResult> JoinClass(JoinClassroomCommand joinClassroomCommand)
        {
            var result=await _mediator.Send(joinClassroomCommand);
            return ActionResult(result);
        }

        [HttpGet("user/{take}/{page}")]
        public async Task<IActionResult> GetClassroomsByUser(int take, int page)
        {
            var result = await _mediator.Send(new GetAllClassroomsByUserQuery(take,page));
            return ActionResult(result);
        }
        
        [HttpGet("enter")]
        public async Task<IActionResult> GetClassroom(string classroomId)
        {
            var result = await _mediator.Send(new GetClassroomByUserQuery { classroomId =  classroomId});
            return ActionResult(result);
        }

        [HttpGet("search/{key}/{take}/{page}")]
        public async Task<IActionResult> SearchClassroom(string key, int take, int page) =>
            ActionResult(await _mediator.Send(new SearchClassroomQuery(take,page,key)));

        [HttpGet("related")]
        public async Task<IActionResult> RelatedWithUser() =>
            ActionResult(await _mediator.Send(new GetRelatedClassWithUserQuery()));
    }
}
