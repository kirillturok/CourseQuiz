using CourseQuiz.API.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseQuiz.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost("create-quiz")]
    [Authorize(Roles = "user")]
    public IActionResult CreateQuiz([FromBody]QuizForCreationDto quiz)
    {

        return Ok("YYYYes");
    }

    [HttpGet("Gett")]
    public IActionResult Gett()
    {
        return Ok("YYYYes");
    }
}
