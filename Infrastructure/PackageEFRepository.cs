using Core.Domain;
using Core.DomainServices.Repos.Intf;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure {

    public class PackageEFRepository : IPackageRepository {
        private readonly PackageDbContext _context;

        public PackageEFRepository(PackageDbContext context) {
            _context = context;
        }

        public async Task AddPackage(Package newPackage) {
            _context.Packages.Add(newPackage);
            await _context.SaveChangesAsync();
        }

        public async Task AsignReserve(Package package, Student student) {
            Package reservePackage = await _context.Packages.FindAsync(package!.Id);
            reservePackage!.ReservedBy = student;
            await _context.SaveChangesAsync();
        }

        public async Task CancelReserve(Package package) {
            package.ReservedBy = null;
            _context.Packages.Update(package);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePackage(Package newPackage) {
            Package editPackage = GetById(newPackage.Id);
            if (editPackage != null) {
                editPackage.Products.Any(p => editPackage.Products.Remove(p));

                editPackage.Name = newPackage.Name;
                editPackage.Products = newPackage.Products;
                editPackage.PickupDate = newPackage.PickupDate;
                editPackage.EndOfPickupTime = newPackage.EndOfPickupTime;
                editPackage.IsEighteenPlus = newPackage.IsEighteenPlus;
                editPackage.Price = newPackage.Price;
                editPackage.MealType = newPackage.MealType;
                editPackage.CityEnum = newPackage.CityEnum;
                editPackage.CanteenLocationEnum = newPackage.CanteenLocationEnum;
                editPackage.CanteenId = newPackage.CanteenId;

                _context.Packages.Update(editPackage);
            }

            await _context.SaveChangesAsync();
        }

        public async Task Delete(Package package) {
            _context.Packages.Remove(package);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Package> GetAll() {
            return _context.Packages.Include(p => p.ReservedBy).Include(p => p.Canteen.CanteenEmployee).Include(p => p.Products).OrderBy(p => p.PickupDate);
        }

        public IEnumerable<Package> GetAllRecommendedPackages() {
            return _context.Packages.Include(p => p.ReservedBy).Include(p => p.Canteen.CanteenEmployee).Include(p => p.Products).Where(p => p.ReservedBy == null);
        }

        public IEnumerable<Package> GetAllReservedPackages() {
            return _context.Packages.Include(p => p.ReservedBy).Include(p => p.Canteen.CanteenEmployee).Include(p => p.Products).Where(p => p.ReservedBy != null);
        }

        public IEnumerable<Package> GetAllIsEighteenPlusPackages() {
            return _context.Packages.Include(p => p.ReservedBy).Include(p => p.Canteen.CanteenEmployee).Include(p => p.Products).Where(p => p.IsEighteenPlus);
        }

        public IEnumerable<Package> GetAllIsNotEighteenPlusPackages() {
            return _context.Packages.Include(p => p.ReservedBy).Include(p => p.Canteen.CanteenEmployee).Include(p => p.Products).Where(p => !p.IsEighteenPlus);
        }

        public Package GetById(int id) {
            return _context.Packages.Include(p => p.ReservedBy).Include(p => p.Canteen.CanteenEmployee).Include(p => p.Products).SingleOrDefault(package => package.Id == id);
        }
    }
}