using CourseService.Models;

namespace CourseService.Interfaces
{
    public interface ICourseRepository
    {
        List<Course> GetAll();
        Course? GetById(int id);
        Course Add(Course course);
        bool Update(Course course);
        bool Delete(int id);
        void Seed();
    }
}
