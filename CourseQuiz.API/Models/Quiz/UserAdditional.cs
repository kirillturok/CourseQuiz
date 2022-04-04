using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseQuiz.API.Models.Quiz;

public class UserAdditional
{
    [Key]
    [ForeignKey("User")]
    public string Id { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string SecondName { get; set; }
    public User User { get; set; }
}
