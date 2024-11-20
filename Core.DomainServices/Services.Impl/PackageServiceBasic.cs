using Core.Domain;
using Core.DomainServices.Repos.Intf;
using Core.DomainServices.Services.Intf;

namespace Core.DomainServices.Services.Impl {
    public class PackageServiceBasic : IPackageService {
        private readonly IPackageRepository _packageRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICanteenEmployeeRepository _canteenEmployeeRepository;

        public PackageServiceBasic(
        IPackageRepository packageRepository,
        IProductRepository productRepository,
        ICanteenEmployeeRepository canteenEmployeeRepository) {
            _packageRepository = packageRepository;
            _productRepository = productRepository;
            _canteenEmployeeRepository = canteenEmployeeRepository;
        }

        public IEnumerable<Package> GetAllRecommendedPackages() {
            return _packageRepository.GetAllRecommendedPackages().ToList();
        }

        public IEnumerable<Package> StartAtRecommended(CityEnum studentStudyCity) {
            return _packageRepository.GetAllRecommendedPackages().Where(p => p.CityEnum == studentStudyCity);
        }

        public Package GetDetails(int id) {
            return _packageRepository.GetById(id);
        }

        public IEnumerable<Package> GetAll() {
            return _packageRepository.GetAll().ToList();
        }

        public Package PackageToCreate(
        string name,
        IEnumerable<int> selectedProductsIds,
        DateTime pickupDate,
        DateTime endOfPickupTime,
        decimal price,
        MealType mealType,
        CityEnum cityEnum,
        CanteenLocationEnum canteenLocationEnum,
        int? canteenId) {
            pickupDate.IsPickupDateTwoDayValid();
            endOfPickupTime.IsEndOfPickupTimeTwoDayValid();

            List<Product> products = new List<Product>();
            var productContainsAlcohol = false;

            foreach (var productId in selectedProductsIds) {
                Product addProduct = _productRepository.GetById(productId);
                products.Add(addProduct);
                if (addProduct.ContainsAlcohol) {
                    productContainsAlcohol = addProduct.ContainsAlcohol;
                }
            }

            Package newPackage = new Package() {
                Name = name,
                Products = products,
                PickupDate = pickupDate,
                EndOfPickupTime = endOfPickupTime,
                IsEighteenPlus = productContainsAlcohol,
                Price = price,
                MealType = mealType,
                CityEnum = cityEnum,
                CanteenLocationEnum = canteenLocationEnum,
                CanteenId = canteenId
            };
            return newPackage;
        }

        public Package PackageToUpdate(
        int id,
        string name,
        IEnumerable<int> selectedProductsIds,
        DateTime pickupDate,
        DateTime endOfPickupTime,
        decimal price,
        MealType mealType,
        CityEnum cityEnum,
        CanteenLocationEnum canteenLocationEnum,
        int? canteenId) {
            pickupDate.IsPickupDateTwoDayValid();
            endOfPickupTime.IsEndOfPickupTimeTwoDayValid();

            List<Product> products = new List<Product>();
            var productContainsAlcohol = false;

            foreach (var productId in selectedProductsIds) {
                Product addProduct = _productRepository.GetById(productId);
                products.Add(addProduct);
                if (addProduct.ContainsAlcohol) {
                    productContainsAlcohol = addProduct.ContainsAlcohol;
                }
            }

            Package updatedPackage = new Package() {
                Id = id,
                Name = name,
                Products = products,
                PickupDate = pickupDate,
                EndOfPickupTime = endOfPickupTime,
                IsEighteenPlus = productContainsAlcohol,
                Price = price,
                MealType = mealType,
                CityEnum = cityEnum,
                CanteenLocationEnum = canteenLocationEnum,
                CanteenId = canteenId
            };
            return updatedPackage;
        }

        public void ReservePackage(int id, Student student) {
            var package = _packageRepository.GetById(id);
            var amountOfReserves = _packageRepository.GetAll().Where(p => p.ReservedBy != null && p.ReservedBy!.Name == student.Emailadres).Count();

            if (package.ReservedBy != null) {
                throw new ArgumentException("Excuses, het pakket is al gereserveerd door een ander student.");
            }
            if (package.IsEighteenPlus) {
                var dateOfAge18 = student.Birthdate.AddYears(18);
                if (dateOfAge18 > package.PickupDate) {
                    throw new ArgumentException("Helaas je bent nog geen 18+.");
                }
            }
            if (amountOfReserves > 0) {
                var studentReservedPackages = _packageRepository.GetAll()
                    .Where(p => p.ReservedBy != null && p.ReservedBy!.Name == student.Emailadres);

                DateTime tempDateMinusOne = (DateTime)package.PickupDate!;
                var OneDayEarlier = tempDateMinusOne.AddDays(-1);

                DateTime tempDate = (DateTime)package.PickupDate;
                var OneDayLater = tempDate.AddDays(1);

                foreach (var reservedPackage in studentReservedPackages) {
                    if (reservedPackage.PickupDate >= OneDayEarlier && reservedPackage.PickupDate <= OneDayLater) {
                        throw new ArgumentException("Je mag maximaal 1 pakket per afhaaldag reserveren.");
                    }
                }
            }
            if (package.PickupDate < DateTime.Now.Date.Subtract(TimeSpan.FromDays(1))) {
                throw new ArgumentException("De ophaaldatum van dit pakket is al verstreken.");
            }

            _packageRepository.AsignReserve(package, student);
        }

        public void CancelReserve(int id) {
            var package = _packageRepository.GetById(id);
            _packageRepository.CancelReserve(package);
        }

        public void Delete(int id) {
            var package = _packageRepository.GetById(id);
            if (package.ReservedBy != null) {
                throw new ArgumentException("Pakket mag niet verwijdert worden omdat deze al gereserveerd is door een student.");
            }
            _packageRepository.Delete(package);
        }

        public IEnumerable<Package> AllReserved() {
            return _packageRepository.GetAllReservedPackages().ToList();
        }

        public IEnumerable<Package> StudentReserved(string name) {
            var studentReservedPackages = _packageRepository.GetAll()
                .Where(p => p.ReservedBy!.Name == name);
            return studentReservedPackages;
        }

        public IEnumerable<Package> FilterPackages(string filterType, int filterValue) {
            IEnumerable<Package> filteredPackages;

            switch (filterType) {
                case "MealType":
                    filteredPackages = _packageRepository.GetAllRecommendedPackages()
                        .Where(p => (int)p.MealType == filterValue);
                    break;
                case "City":
                    filteredPackages = _packageRepository.GetAllRecommendedPackages()
                        .Where(p => (int)p.CityEnum == filterValue);
                    break;
                default:
                    filteredPackages = _packageRepository.GetAllRecommendedPackages();
                    break;
            }

            return filteredPackages;
        }

        public IEnumerable<Package> CanteenEmployeeLocationOverview(string name) {
            var CanteenEmployee = _canteenEmployeeRepository.GetByName(name);
            var CanteenEmployeePackages = _packageRepository.GetAll()
              .Where(p => p.Canteen!.CanteenEmployee.CanteenLocationEnum == CanteenEmployee.CanteenLocationEnum);
            return CanteenEmployeePackages;
        }

        public IEnumerable<Package> AllIsEighteenPlusPackages() {
            return _packageRepository.GetAllIsEighteenPlusPackages().ToList();
        }

        public IEnumerable<Package> AllIsNotEighteenPlusPackages() {
            return _packageRepository.GetAllIsNotEighteenPlusPackages().ToList();
        }
    }
}
