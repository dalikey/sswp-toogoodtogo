using Core.Domain;

namespace Core.DomainServices.Services.Intf {
    public interface IPackageService {
        IEnumerable<Package> GetAllRecommendedPackages();

        IEnumerable<Package> StartAtRecommended(CityEnum studentStudyCity);

        Package GetDetails(int id);

        IEnumerable<Package> GetAll();

        Package PackageToCreate(
            string name,
            IEnumerable<int> selectedProductsIds,
            DateTime pickupDate,
            DateTime endOfPickupTime,
            decimal price,
            MealType mealType,
            CityEnum cityEnum,
            CanteenLocationEnum canteenLocationEnum,
            int? canteenId);

        Package PackageToUpdate(
            int id,
            string name,
            IEnumerable<int> selectedProductsIds,
            DateTime pickupDate,
            DateTime endOfPickupTime,
            decimal price,
            MealType mealType,
            CityEnum cityEnum,
            CanteenLocationEnum canteenLocationEnum,
            int? canteenId);

        void ReservePackage(int id, Student student);

        void CancelReserve(int id);

        void Delete(int id);
        IEnumerable<Package> AllReserved();
        IEnumerable<Package> StudentReserved(string name);
        IEnumerable<Package> FilterPackages(string filterType, int filterValue);
        IEnumerable<Package> CanteenEmployeeLocationOverview(string name);
        IEnumerable<Package> AllIsEighteenPlusPackages();
        IEnumerable<Package> AllIsNotEighteenPlusPackages();

    }
}
