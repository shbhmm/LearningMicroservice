using CourseService.Interfaces;
using CourseService.Models;

namespace CourseService.Data
{
    public class CourseRepository : ICourseRepository
    {
        private readonly List<Course> _courses = new();
        private int _nextId = 1;

        public List<Course> GetAll() => _courses;

        public Course? GetById(int id) => _courses.FirstOrDefault(c => c.Id == id);

        public Course Add(Course course)
        {
            course.Id = _nextId++;
            _courses.Add(course);
            return course;
        }

        public bool Update(Course course)
        {
            var existing = GetById(course.Id);
            if (existing == null) return false;
            existing.Title = course.Title;
            existing.Category = course.Category;
            existing.Duration = course.Duration;
            existing.Instructor = course.Instructor;
            return true;
        }

        public bool Delete(int id)
        {
            var existing = GetById(id);
            if (existing == null) return false;
            _courses.Remove(existing);
            return true;
        }

        public void Seed()
        {
            if (_courses.Any()) return;
            Add(new Course { Title = "Introduction to C#", Category = "Programming", Duration = 180, Instructor = "Alice" });
            Add(new Course { Title = "ASP.NET Core Basics", Category = "Web", Duration = 240, Instructor = "Bob" });
            Add(new Course { Title = "Docker for Developers", Category = "DevOps", Duration = 120, Instructor = "Carol" });
            Add(new Course { Title = "RESTful APIs", Category = "Web", Duration = 200, Instructor = "Dave" });
            Add(new Course { Title = "Unit Testing", Category = "Quality", Duration = 160, Instructor = "Eve" });
        }
    }
}
