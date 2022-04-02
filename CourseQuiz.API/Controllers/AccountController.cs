﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using CourseQuiz.API.Models;
using CourseQuiz.API.ViewModels;
using CourseQuiz.API.Services;

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

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest("Some properties are not valid");

        User user = new User { Email = model.Email, UserName = model.Email };
        // добавляем пользователя
        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            return BadRequest(result.Errors);

        // генерация токена для пользователя
        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var callbackUrl = Url.Action(
        "ConfirmEmail",
        "Account",
        new { userId = user.Id, code = code },
        protocol: HttpContext.Request.Scheme);

        EmailService emailService = new EmailService();
        await emailService.SendEmailAsync(model.Email, "Confirm your account",
        $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>");

        return Content("Для завершения регистрации проверьте электронную почту и перейдите по ссылке, указанной в письме");
    }

    [HttpGet]
    [AllowAnonymous]
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
    [AllowAnonymous]
    //[ValidateAntiForgeryToken]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
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
                $"Для сброса пароля пройдите по ссылке: <a href='{callbackUrl}'>link</a>");
            return Ok("Sent");
        }
        return BadRequest("Invalid data");
    }

    [HttpGet("ResetPassword")]
    [AllowAnonymous]
    public IActionResult ResetPassword(string code = null)
    {
        return code == null ? BadRequest("code=null") : Ok("Hooray");
    }

    [HttpPost("ResetPassword")]
    [AllowAnonymous]
//    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Not valid data");
        }
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return BadRequest("ResetPasswordConfirmation");
        }
        var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
        if (result.Succeeded)
        {
            return Ok();
        }
        
        return BadRequest(result.Errors);
    }

    /*
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user != null)
            {
                // проверяем, подтвержден ли email
                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                    ModelState.AddModelError(string.Empty, "Вы не подтвердили свой email");
                    return View(model);
                }
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Неправильный логин и (или) пароль");
            }
        }
        return View(model);
    }*/


    /*[HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LogOff()
    {
        // удаляем аутентификационные куки
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }*/
}