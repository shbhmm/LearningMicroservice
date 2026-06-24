using StudentService.Models;

namespace StudentService.Interfaces
{
    public interface IStudentRepository
    {
        List<Student> GetAll();
        Student? GetById(int id);
        Student Add(Student student);
        bool Update(Student student);
        bool Delete(int id);
        void Seed();
    }
}
