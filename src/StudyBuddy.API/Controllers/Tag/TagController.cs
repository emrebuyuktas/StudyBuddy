using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyBuddy.Application.Features.Commands.Tag;
using StudyBuddy.Application.Features.Queries.Tag;
using StudyBuddy.Application.Wrappers;

namespace StudyBuddy.API.Controllers.Tag;


[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class TagController : CustomBaseController
{
    private readonly IMediator _mediator;

    public TagController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateTag() => ActionResult(await _mediator.Send(new CreateTagCommand()));

    [HttpGet("all")]
    public async Task<IActionResult> GetTags() => ActionResult(await _mediator.Send(new GetTagsQuery()));
}