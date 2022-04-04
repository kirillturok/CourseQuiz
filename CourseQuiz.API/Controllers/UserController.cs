using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseQuiz.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpGet("Get")]
    [Authorize]
    public IActionResult Get()
    {
        return Ok("YYYYes");
    }

    [HttpGet("Gett")]
    public IActionResult Gett()
    {
        return Ok("YYYYes");
    }
}
