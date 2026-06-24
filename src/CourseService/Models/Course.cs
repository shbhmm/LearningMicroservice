namespace CourseService.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int Duration { get; set; } // minutes
        public string Instructor { get; set; } = string.Empty;
    }
}
