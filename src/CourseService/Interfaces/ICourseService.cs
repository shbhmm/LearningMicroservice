using CourseService.Models;

namespace CourseService.Interfaces
{
    public interface ICourseService
    {
        Task<List<Course>> GetAllAsync();
        Task<Course?> GetByIdAsync(int id);
        Task<Course> CreateAsync(Course course);
        Task<bool> UpdateAsync(int id, Course course);
        Task<bool> DeleteAsync(int id);
    }
}
