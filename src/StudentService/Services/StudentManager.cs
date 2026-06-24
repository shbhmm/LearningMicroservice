using StudentService.Interfaces;
using StudentService.Models;

namespace StudentService.Services
{
    public class StudentManager : IStudentService
    {
        private readonly IStudentRepository _repo;
        private readonly ILogger<StudentManager> _logger;

        public StudentManager(IStudentRepository repo, ILogger<StudentManager> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public Task<List<Student>> GetAllAsync()
        {
            return Task.FromResult(_repo.GetAll());
        }

        public Task<Student?> GetByIdAsync(int id)
        {
            return Task.FromResult(_repo.GetById(id));
        }

        public Task<Student> CreateAsync(Student student)
        {
            var created = _repo.Add(student);
            _logger.LogInformation("Student added: {Name}", created.Name);
            return Task.FromResult(created);
        }

        public Task<bool> UpdateAsync(int id, Student student)
        {
            student.Id = id;
            var ok = _repo.Update(student);
            if (ok) _logger.LogInformation("Student updated: {Id}", id);
            return Task.FromResult(ok);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var ok = _repo.Delete(id);
            if (ok) _logger.LogInformation("Student deleted: {Id}", id);
            return Task.FromResult(ok);
        }
    }
}
