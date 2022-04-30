namespace CourseQuiz.API.Models;

public interface IQuizRepository
{
    IEnumerable<Quiz.Quiz> GetUsersQuizzes(User user);
    Quiz.Quiz GetQuiz(int id);
    void CreateQuiz(Quiz.Quiz quiz);
}
