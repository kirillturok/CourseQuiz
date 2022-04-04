using System.ComponentModel.DataAnnotations.Schema;

namespace CourseQuiz.API.Models.Quiz;

public class TextAnswer
{
    [ForeignKey("Question")]
    public int Id { get; set; }
    public string Answer { get; set; }
}
