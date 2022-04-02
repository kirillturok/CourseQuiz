using System.ComponentModel.DataAnnotations;

namespace CourseQuiz.API.ViewModels;

public class EmailViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}

