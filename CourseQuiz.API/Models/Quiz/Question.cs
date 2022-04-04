namespace CourseQuiz.API.Models.Quiz;

public class Question
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Decription { get; set; }
    public bool HasMultipleChoice { get; set; }
    public bool IsOrdered { get; set; }
    public short Order { get; set; }
    public bool TextAns { get; set; }
    public byte CalculationType { get; set; }
    public byte Points { get; set; }
    public List<Answer> Answers { get; set; }
    public TextAnswer TextAnswer { get; set; }
}
