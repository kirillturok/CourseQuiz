using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using CourseQuiz.API.Models;
using CourseQuiz.API.ViewModels;
using CourseQuiz.API.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CourseQuiz.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest("Some properties are not valid");

        User user = new User { Email = model.Email, UserName = model.Email };
        //add user
        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        if (await SendConfirmationEmail(user))
            return Ok("Check your email");
        else
            return StatusCode(500, "Couldn't send confirmation email");
    }

    [HttpPost("SendConfirmEmail")]
    public async Task<IActionResult> SendConfirmEmail(EmailViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid data");

        var user = await _userManager.FindByNameAsync(model.Email);

        if (user == null)
            return Ok();

        // проверяем, подтвержден ли email
        if (await _userManager.IsEmailConfirmedAsync(user))
        {
            return BadRequest("email is already confirmed");
        }

        // генерация токена для пользователя
        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var callbackUrl = Url.Action(
        "ConfirmEmail",
        "Account",
        new { userId = user.Id, code = code },
        protocol: HttpContext.Request.Scheme);

        EmailService emailService = new EmailService();
        await emailService.SendEmailAsync(model.Email, "Confirm your account",
        $"Confirm your registration <a href='{callbackUrl}'>here</a>");

        return Ok();
    }

    [HttpGet("ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmail(string userId, string code)
    {
        if (userId == null || code == null)
        {
            return BadRequest("Error 1");
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return BadRequest("Error 2");
        }

        var result = await _userManager.ConfirmEmailAsync(user, code);
        if (result.Succeeded)
            return Ok("Confirmed");
        else
            return BadRequest("Error 3");
    }

    [HttpPost("ForgotPassword")]
    public async Task<IActionResult> ForgotPassword(EmailViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                // пользователь с данным email может отсутствовать в бд
                // тем не менее мы выводим стандартное сообщение, чтобы скрыть 
                // наличие или отсутствие пользователя в бд
                return BadRequest("ForgotPasswordConfirmation");
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
            EmailService emailService = new EmailService();
            await emailService.SendEmailAsync(model.Email, "Reset Password",
                $"Для сброса пароля пройдите по ссылке: <a href='{callbackUrl}'>link</a><p>{code}</p>");
            return Ok("Sent");
        }
        return BadRequest("Invalid data");
    }

    [HttpPost("ResetPassword")]
    public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest("Not valid data");

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
            return BadRequest("ResetPasswordConfirmation");

        var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
        if (result.Succeeded)
            return Ok();

        return BadRequest(result.Errors);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid data");

        var user = await _userManager.FindByNameAsync(model.Email);
        if (user == null)
            return BadRequest("Wrong password or user");

        // проверяем, подтвержден ли email
        if (!await _userManager.IsEmailConfirmedAsync(user))
        {
            return BadRequest("email wasn't confirmed");
        }

        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
        if (!result.Succeeded)
            return BadRequest("Wrong password or user");

        var token = GenerateJWT(model.Email,Role.User);
        return Ok(new
        {
            access_token = token
        });
    }

    private string GenerateJWT(string email,Role role)
    {
        var securityKey = AuthOptions.GetSymmetricSecurityKey;
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role,role.ToString())
        };

        var token = new JwtSecurityToken(AuthOptions.ISSUER,
            AuthOptions.AUDIENCE,
            claims,
            expires: DateTime.Now.AddDays(AuthOptions.LIFETIME),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<bool> SendConfirmationEmail(User user)
    {
        try
        {
            //token generation for mail confirmation
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Action(
            "ConfirmEmail",
            "Account",
            new { userId = user.Id, code = code },
            protocol: HttpContext.Request.Scheme);

            EmailService emailService = new EmailService();
            await emailService.SendEmailAsync(user.Email, "Confirm your account",
            $"Thank you for registering. Please click the button to confirm your email address " +
            $"<a href = '{callbackUrl}'>here</a>");
            return true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex);
            return false;
        }
    }
}