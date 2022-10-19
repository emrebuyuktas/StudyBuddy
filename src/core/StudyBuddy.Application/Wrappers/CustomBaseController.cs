using Microsoft.AspNetCore.Mvc;

namespace StudyBuddy.Application.Wrappers;

public class CustomBaseController: ControllerBase
{
    public IActionResult ActionResult<T>(Response<T> response)where T : class
    {
        return new ObjectResult(response)
        {
            StatusCode = response.StatusCode,
        };
    }
}