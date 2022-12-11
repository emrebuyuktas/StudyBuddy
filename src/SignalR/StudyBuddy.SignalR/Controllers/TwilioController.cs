using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudyBuddy.SignalR.Services;

namespace StudyBuddy.SignalR.Controllers
{
    [Route("api/twilio")]
    [ApiController]
    public class TwilioController : ControllerBase
    {
        [HttpGet("token")]
        public IActionResult GetToken(
            [FromServices] TwilioService twilioService) =>
            new JsonResult(twilioService.GetTwilioJwt(User.Identity.Name));

        [HttpGet("rooms")]
        public async Task<IActionResult> GetRooms(
            [FromServices] TwilioService twilioService) =>
            new JsonResult(await twilioService.GetAllRoomsAsync());
    }
}
