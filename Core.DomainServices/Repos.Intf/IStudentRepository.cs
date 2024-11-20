using Core.Domain;

namespace Core.DomainServices.Repos.Intf {
    public interface IStudentRepository {
        IEnumerable<Student> GetStudents();
        Student GetByEmail(string email);
        Student GetById(int id);
        Task AddStudent(Student newStudent);
    }
}