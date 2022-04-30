namespace CourseQuiz.API.DTO
{
    public class QuizForCreationDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool IsOrdered { get; set; }
        public List<Question> Questions { get; set; }
        public List<string> Tags { get; set; }
        //public PrivacyConfiguration PrivacyConfig { get; set; }
    }
}
