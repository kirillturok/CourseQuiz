using CourseQuiz.API.Models.Quiz;

namespace CourseQuiz.API.Models.Answers;

public class QuizAnswer
{
    public int Id { get; set; }
    public Quiz.Quiz Quiz { get; set; }
    public List<QuestionAnswer> QuestionAnswers { get; set; }
    public User Participant { get; set; }
    public PrivateAccess Access { get; set; }
    public DateTime EndTime { get; set; }
}
