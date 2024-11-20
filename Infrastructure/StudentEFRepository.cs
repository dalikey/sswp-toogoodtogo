using Core.Domain;
using Core.DomainServices.Repos.Intf;

namespace Infrastructure {

    public class StudentEFRepository : IStudentRepository {
        private readonly PackageDbContext _context;

        public StudentEFRepository(PackageDbContext context) {
            _context = context;
        }

        public IEnumerable<Student> GetStudents() {
            return _context.Students.ToList();
        }

        public Student GetByEmail(string email) {
            return _context.Students.SingleOrDefault(s => s.Emailadres == email);
        }
        public Student GetById(int id) {
            return _context.Students.SingleOrDefault(s => s.Id == id);
        }

        public async Task AddStudent(Student newStudent) {
            _context.Students.Add(newStudent);
            await _context.SaveChangesAsync();
        }
    }
}