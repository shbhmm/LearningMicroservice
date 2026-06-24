using CourseService.Interfaces;
using CourseService.Models;

namespace CourseService.Services
{
    public class CourseManager : ICourseService
    {
        private readonly ICourseRepository _repo;
        private readonly ILogger<CourseManager> _logger;

        public CourseManager(ICourseRepository repo, ILogger<CourseManager> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public Task<List<Course>> GetAllAsync()
        {
            return Task.FromResult(_repo.GetAll());
        }

        public Task<Course?> GetByIdAsync(int id)
        {
            return Task.FromResult(_repo.GetById(id));
        }

        public Task<Course> CreateAsync(Course course)
        {
            var created = _repo.Add(course);
            _logger.LogInformation("Course added: {Title}", created.Title);
            return Task.FromResult(created);
        }

        public Task<bool> UpdateAsync(int id, Course course)
        {
            course.Id = id;
            var ok = _repo.Update(course);
            if (ok) _logger.LogInformation("Course updated: {Id}", id);
            return Task.FromResult(ok);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var ok = _repo.Delete(id);
            if (ok) _logger.LogInformation("Course deleted: {Id}", id);
            return Task.FromResult(ok);
        }
    }
}
