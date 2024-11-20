using Core.Domain;
using Core.DomainServices.Repos.Intf;

namespace Infrastructure {

    public class CanteenEmployeeEFRepository : ICanteenEmployeeRepository {
        private readonly PackageDbContext _context;

        public CanteenEmployeeEFRepository(PackageDbContext context) {
            _context = context;
        }

        public IEnumerable<CanteenEmployee> GetCanteenEmployees() {
            return _context.CanteenEmployees.ToList();
        }

        public CanteenEmployee GetById(int id) {
            return _context.CanteenEmployees.SingleOrDefault(canteenEmployee => canteenEmployee.Id == id);
        }

        public CanteenEmployee GetByName(string name) {
            return _context.CanteenEmployees.SingleOrDefault(canteenEmployee => canteenEmployee.Name == name);
        }
    }
}