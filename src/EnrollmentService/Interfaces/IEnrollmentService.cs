using EnrollmentService.Models;

namespace EnrollmentService.Interfaces
{
    public interface IEnrollmentService
    {
        Task<List<Enrollment>> GetAllAsync();
        Task<Enrollment?> GetByIdAsync(int id);
        Task<Enrollment> CreateAsync(Enrollment enrollment);
        Task<bool> DeleteAsync(int id);
        Task<List<Enrollment>> GetByStudentAsync(int studentId);
        Task<List<Enrollment>> GetByCourseAsync(int courseId);
    }
}
