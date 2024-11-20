using Core.Domain;

namespace Core.DomainServices.Repos.Intf {
    public interface ICanteenRepository {
        IEnumerable<Canteen> GetCanteens();
        Canteen GetById(int id);
    }
}