namespace CourseQuiz.API.Models.Quiz;

public class Quiz
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public User Owner { get; set; }
    public bool IsPublic { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsOrdered { get; set; }
    public List<Question> Questions { get; set; }
    public List<Tag> Tags { get; set; }
    public PrivacyConfiguration PrivacyConfig { get; set; }
}
