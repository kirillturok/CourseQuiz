namespace CourseQuiz.API.Models.Quiz;

public class Tag
{
    public int Id { get; set; }
    public string TagName { get; set; }
    public List<Quiz> Quizes { get; set; }
}
