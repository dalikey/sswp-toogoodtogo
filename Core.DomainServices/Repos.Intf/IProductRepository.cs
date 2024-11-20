using Core.Domain;

namespace Core.DomainServices.Repos.Intf {
    public interface IProductRepository {
        IEnumerable<Product> GetProducts();
        Product GetById(int id);
    }
}
