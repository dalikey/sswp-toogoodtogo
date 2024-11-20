using Core.Domain;
using Core.DomainServices.Repos.Intf;

namespace Infrastructure {
    public class ProductEFRepository : IProductRepository {
        private readonly PackageDbContext _context;

        public ProductEFRepository(PackageDbContext context) {
            _context = context;
        }

        public IEnumerable<Product> GetProducts() {
            return _context.Products.ToList();
        }

        public Product GetById(int id) {
            return _context.Products.SingleOrDefault(product => product.Id == id);
        }
    }
}
