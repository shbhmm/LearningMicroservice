using EnrollmentService.Models;

namespace EnrollmentService.Interfaces
{
    public interface IEnrollmentRepository
    {
        List<Enrollment> GetAll();
        Enrollment? GetById(int id);
        Enrollment Add(Enrollment enrollment);
        bool Delete(int id);
        List<Enrollment> GetByStudentId(int studentId);
        List<Enrollment> GetByCourseId(int courseId);
        void Seed();
    }
}
