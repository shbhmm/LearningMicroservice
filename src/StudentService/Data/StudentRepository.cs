using StudentService.Interfaces;
using StudentService.Models;

namespace StudentService.Data
{
    public class StudentRepository : IStudentRepository
    {
        private readonly List<Student> _students = new();
        private int _nextId = 1;

        public List<Student> GetAll() => _students;

        public Student? GetById(int id) => _students.FirstOrDefault(s => s.Id == id);

        public Student Add(Student student)
        {
            student.Id = _nextId++;
            _students.Add(student);
            return student;
        }

        public bool Update(Student student)
        {
            var existing = GetById(student.Id);
            if (existing == null) return false;
            existing.Name = student.Name;
            existing.Email = student.Email;
            existing.Phone = student.Phone;
            return true;
        }

        public bool Delete(int id)
        {
            var existing = GetById(id);
            if (existing == null) return false;
            _students.Remove(existing);
            return true;
        }

        public void Seed()
        {
            if (_students.Any()) return;
            Add(new Student { Name = "John Doe", Email = "john@example.com", Phone = "123-456-7890" });
            Add(new Student { Name = "Jane Smith", Email = "jane@example.com", Phone = "234-567-8901" });
            Add(new Student { Name = "Sam Wilson", Email = "sam@example.com", Phone = "345-678-9012" });
            Add(new Student { Name = "Lisa Brown", Email = "lisa@example.com", Phone = "456-789-0123" });
            Add(new Student { Name = "Tom Hanks", Email = "tom@example.com", Phone = "567-890-1234" });
        }
    }
}
