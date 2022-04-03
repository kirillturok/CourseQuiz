using CourseQuiz.API.Models.Answers;
using CourseQuiz.API.Models.Quiz;
using Microsoft.AspNetCore.Identity;

namespace CourseQuiz.API.Models;

public class User : IdentityUser
{
    //public UserAdditional UserAdditional { get; set; }
    public List<QuizAnswer> QuizAnswers { get; set; }
}
