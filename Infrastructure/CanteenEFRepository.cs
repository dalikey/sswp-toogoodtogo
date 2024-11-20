using Core.Domain;
using Core.DomainServices.Repos.Intf;

namespace Infrastructure {

    public class CanteenEFRepository : ICanteenRepository {
        private readonly PackageDbContext _context;

        public CanteenEFRepository(PackageDbContext context) {
            _context = context;
        }

        public IEnumerable<Canteen> GetCanteens() {
            return _context.Canteens;
        }

        public Canteen GetById(int id) {
            return _context.Canteens.SingleOrDefault(canteen => canteen.Id == id);
        }
    }
}