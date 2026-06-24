using EnrollmentService.Interfaces;
using EnrollmentService.Models;
using System.Net.Http.Json;

namespace EnrollmentService.Services
{
    public class EnrollmentManager : IEnrollmentService
    {
        private readonly IEnrollmentRepository _repo;
        private readonly IHttpClientFactory _http;
        private readonly ILogger<EnrollmentManager> _logger;

        public EnrollmentManager(IEnrollmentRepository repo, IHttpClientFactory http, ILogger<EnrollmentManager> logger)
        {
            _repo = repo;
            _http = http;
            _logger = logger;
        }

        public Task<List<Enrollment>> GetAllAsync()
        {
            return Task.FromResult(_repo.GetAll());
        }

        public Task<Enrollment?> GetByIdAsync(int id)
        {
            return Task.FromResult(_repo.GetById(id));
        }

        public Task<Enrollment> CreateAsync(Enrollment enrollment)
        {
            var created = _repo.Add(enrollment);
            _logger.LogInformation("Enrollment added: {Id}", created.Id);
            return Task.FromResult(created);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var ok = _repo.Delete(id);
            if (ok) _logger.LogInformation("Enrollment deleted: {Id}", id);
            return Task.FromResult(ok);
        }

        public Task<List<Enrollment>> GetByStudentAsync(int studentId)
        {
            return Task.FromResult(_repo.GetByStudentId(studentId));
        }

        public Task<List<Enrollment>> GetByCourseAsync(int courseId)
        {
            return Task.FromResult(_repo.GetByCourseId(courseId));
        }

        // Helpers to fetch external data
        public async Task<object?> GetStudentDetailsAsync(int studentId)
        {
            var client = _http.CreateClient("students");
            try
            {
                var res = await client.GetAsync($"/api/students/{studentId}");
                if (!res.IsSuccessStatusCode) return null;
                var obj = await res.Content.ReadFromJsonAsync<object>();
                return obj;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch student {Id}", studentId);
                return null;
            }
        }

        public async Task<object?> GetCourseDetailsAsync(int courseId)
        {
            var client = _http.CreateClient("courses");
            try
            {
                var res = await client.GetAsync($"/api/courses/{courseId}");
                if (!res.IsSuccessStatusCode) return null;
                var obj = await res.Content.ReadFromJsonAsync<object>();
                return obj;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch course {Id}", courseId);
                return null;
            }
        }
    }
}
