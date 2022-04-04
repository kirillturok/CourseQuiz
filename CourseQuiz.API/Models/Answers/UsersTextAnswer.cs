using System.ComponentModel.DataAnnotations.Schema;

namespace CourseQuiz.API.Models.Answers;

public class UsersTextAnswer
{
    [ForeignKey("QuestionAnswer")]
    public int Id { get; set; }
    public string Answer { get; set; }
}
