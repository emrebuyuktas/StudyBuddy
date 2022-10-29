using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudyBuddy.Application.Features.Commands.Classroom;
using StudyBuddy.Application.Features.Queries.Classroom;
using StudyBuddy.Application.Wrappers;

namespace StudyBuddy.API.Controllers.Classroom
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClassroomController : CustomBaseController
    {
        private readonly IMediator _mediator;

        public ClassroomController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateClass(CreateClassroomCommand createClassroomCommand)
        {
            var result=await _mediator.Send(createClassroomCommand);
            return ActionResult(result);
        }
        [HttpPost]
        public async Task<IActionResult> JoinClass(JoinClassroomCommand joinClassroomCommand)
        {
            var result=await _mediator.Send(joinClassroomCommand);
            return ActionResult(result);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetClassroomsByUser(string userId)
        {
            var result = await _mediator.Send(new GetAllClassroomsByUserQuery { UserId = userId });
            return ActionResult(result);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetClassroom(string classroomId,string userId)
        {
            var result = await _mediator.Send(new GetClassroomByUserQuery { classroomId =  classroomId, UserId = userId});
            return ActionResult(result);
        }
    }
}
