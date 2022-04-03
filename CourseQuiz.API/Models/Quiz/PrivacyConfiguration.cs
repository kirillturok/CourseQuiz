namespace CourseQuiz.API.Models.Quiz;

public class PrivacyConfiguration
{
    public int Id { get; set; }
    public bool IsUnique { get; set; }
    public string Password { get; set; }
    public List<PrivateAccess> PrivateAccesses { get; set; }
}
