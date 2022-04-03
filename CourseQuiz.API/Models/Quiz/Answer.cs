namespace CourseQuiz.API.Models.Quiz;

public class Answer
{
    public long Id { get; set; }
    public string AnswerText { get; set; }
    public bool IsRight { get; set; }
    public short Order { get; set; }
}
