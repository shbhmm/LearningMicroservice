using EnrollmentService.Interfaces;
using EnrollmentService.Models;

namespace EnrollmentService.Data
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly List<Enrollment> _enrollments = new();
        private int _nextId = 1;

        public List<Enrollment> GetAll() => _enrollments;

        public Enrollment? GetById(int id) => _enrollments.FirstOrDefault(e => e.Id == id);

        public Enrollment Add(Enrollment enrollment)
        {
            enrollment.Id = _nextId++;
            _enrollments.Add(enrollment);
            return enrollment;
        }

        public bool Delete(int id)
        {
            var existing = GetById(id);
            if (existing == null) return false;
            _enrollments.Remove(existing);
            return true;
        }

        public List<Enrollment> GetByStudentId(int studentId) => _enrollments.Where(e => e.StudentId == studentId).ToList();
        public List<Enrollment> GetByCourseId(int courseId) => _enrollments.Where(e => e.CourseId == courseId).ToList();

        public void Seed()
        {
            if (_enrollments.Any()) return;
            Add(new Enrollment { StudentId = 1, CourseId = 1, EnrollmentDate = DateTime.UtcNow.AddDays(-10) });
            Add(new Enrollment { StudentId = 2, CourseId = 1, EnrollmentDate = DateTime.UtcNow.AddDays(-8) });
            Add(new Enrollment { StudentId = 3, CourseId = 2, EnrollmentDate = DateTime.UtcNow.AddDays(-5) });
            Add(new Enrollment { StudentId = 1, CourseId = 3, EnrollmentDate = DateTime.UtcNow.AddDays(-2) });
        }
    }
}
