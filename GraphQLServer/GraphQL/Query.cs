using Core.Domain;
using Core.DomainServices.Repos.Intf;

namespace GraphQLServer.GraphQL {
    public class Query {
        private readonly IPackageRepository _packageRepository;
        private readonly IProductRepository _productRepository;

        public Query(IPackageRepository packageRepository, IProductRepository productRepository) {
            _packageRepository = packageRepository;
            _productRepository = productRepository;
        }

        public IQueryable<Package> Packages => _packageRepository.GetAll();
        public Package Package(int id) => _packageRepository.GetById(id);
        public IEnumerable<Product> Products => _productRepository.GetProducts();
    }
}
