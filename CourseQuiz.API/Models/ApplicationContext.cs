using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CourseQuiz.API.Models.Quiz;

namespace CourseQuiz.API.Models;

public class ApplicationContext : IdentityDbContext<User>
{
    public DbSet<Quiz.Quiz> Quizes { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
}
