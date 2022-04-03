namespace CourseQuiz.API.Models.Quiz;

public class PrivateAccess
{
    public int Id { get; set; }
    public User User { get; set; }
    public string GuestName { get; set; }
    public string Password { get; set; }
    public byte Status { get; set; }
}
