using CourseQuiz.API.Models.Quiz;

namespace CourseQuiz.API.Models.Answers;    

public class QuestionAnswer
{
    public int Id { get; set; }
    public Question Question { get; set; }
    public List<UsersAnswer> Answers { get; set; }
    public UsersTextAnswer TextAnswer { get; set; }
}
