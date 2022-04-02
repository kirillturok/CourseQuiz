using System.ComponentModel.DataAnnotations;

namespace CourseQuiz.API.ViewModels;

public class ForgotPasswordViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}

