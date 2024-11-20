using Core.Domain;

namespace Core.DomainServices.Repos.Intf {
    public interface IPackageRepository {
        Task AddPackage(Package newPackage);
        Task AsignReserve(Package package, Student student);
        Task CancelReserve(Package package);
        Task UpdatePackage(Package package);
        Task Delete(Package package);
        IQueryable<Package> GetAll();
        IEnumerable<Package> GetAllRecommendedPackages();
        IEnumerable<Package> GetAllReservedPackages();
        IEnumerable<Package> GetAllIsEighteenPlusPackages();
        IEnumerable<Package> GetAllIsNotEighteenPlusPackages();
        Package GetById(int id);
    }
}