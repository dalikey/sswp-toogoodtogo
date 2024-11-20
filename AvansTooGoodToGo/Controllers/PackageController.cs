using Core.Domain;
using Core.DomainServices.Repos.Intf;
using Core.DomainServices.Services.Intf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Models;

namespace Portal {
    public class PackageController : Controller {
        private readonly IPackageRepository _packageRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICanteenEmployeeRepository _canteenEmployeeRepository;
        private readonly IPackageService _packageService;

        public PackageController(
            IPackageRepository packageRepository,
            IStudentRepository studentRepository,
            IProductRepository productRepository,
            ICanteenEmployeeRepository canteenEmployeeRepository,
            IPackageService packageService) {
            _packageRepository = packageRepository;
            _studentRepository = studentRepository;
            _productRepository = productRepository;
            _canteenEmployeeRepository = canteenEmployeeRepository;
            _packageService = packageService;
        }

        public IActionResult Index() {
            ViewData["Status"] = "Alle Locatie";
            return View(_packageService.GetAllRecommendedPackages());
        }

        public IActionResult StartAtRecommended() {
            ViewData["Status"] = "Aanbevolen";
            if (!User.Identity.IsAuthenticated) {
                return View("Index");
            }
            var student = _studentRepository.GetByEmail(User.Identity.Name!);
            return View("Index", _packageService.StartAtRecommended(student.StudyCity));
        }

        public IActionResult Detail(int id) {
            return View(_packageService.GetDetails(id));
        }

        [Authorize(Policy = "CanteenEmployeeOnly")]
        public IActionResult List() {
            ViewData["Status"] = "Alle";
            return View(_packageService.GetAll());
        }

        [Authorize(Policy = "CanteenEmployeeOnly")]
        [HttpGet]
        public IActionResult NewPackage() {
            var products = _productRepository.GetProducts();
            var checkboxes = products.Select(product => new CheckboxViewModel { Product = product, IsChecked = false }).ToList();
            var model = new NewPackageViewModel { ProductsAsCheckboxViewModel = checkboxes };

            return View(model);
        }

        [Authorize(Policy = "CanteenEmployeeOnly")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> NewPackage(NewPackageViewModel newPackage) {
            if (!ModelState.IsValid) {
                var checkboxProducts = _productRepository.GetProducts();
                var checkboxes = checkboxProducts.Select(product => new CheckboxViewModel { Product = product, IsChecked = false }).ToList();
                var model = new NewPackageViewModel { ProductsAsCheckboxViewModel = checkboxes };

                return View(model);
            }

            try {
                var CanteenEmployee = _canteenEmployeeRepository.GetByName(User.Identity!.Name!);
                Package packageToCreate = _packageService.PackageToCreate(
                newPackage.Name,
                newPackage.SelectedProductsIds,
                newPackage.PickupDate,
                newPackage.EndOfPickupTime,
                newPackage.Price,
                newPackage.MealType,
                newPackage.CityEnum,
                CanteenEmployee.CanteenLocationEnum,
                CanteenEmployee.Id);

                if (ModelState.IsValid) {
                    await _packageRepository.AddPackage(packageToCreate);
                    TempData["IsSuccessMessage"] = "Gelukt! Het pakket is succesvol aangemaakt.";
                    return RedirectToAction("List");
                }
            } catch (ArgumentException e) {
                ModelState.AddModelError("", e.Message);

                var checkboxProducts = _productRepository.GetProducts();
                var checkboxes = checkboxProducts.Select(product => new CheckboxViewModel { Product = product, IsChecked = false }).ToList();
                var model = new NewPackageViewModel { ProductsAsCheckboxViewModel = checkboxes };

                return View(model);
            }

            return View(newPackage);
        }

        [Authorize(Policy = "CanteenEmployeeOnly")]
        [HttpGet]
        public IActionResult UpdatePackage(int id) {
            var editPackage = _packageRepository.GetById(id);

            if (editPackage.ReservedBy != null) {
                TempData["ResultMessage"] = "Pakket mag niet bewerkt worden omdat deze al gereserveerd is door een student.";
                return RedirectToAction("List");
            }

            var products = _productRepository.GetProducts();
            var checkboxes = products.Select(product => new CheckboxViewModel { Product = product, IsChecked = editPackage.Products.Contains(product) }).ToList();

            var model = new UpdatePackageViewModel {
                ProductsAsCheckboxViewModel = checkboxes,
                Id = id,
                Name = editPackage.Name,
                PickupDate = (DateTime)editPackage.PickupDate,
                EndOfPickupTime = (DateTime)editPackage.EndOfPickupTime,
                IsEighteenPlus = editPackage.IsEighteenPlus,
                Price = editPackage.Price,
                MealType = editPackage.MealType,
                CityEnum = editPackage.CityEnum,
                CanteenLocationEnum = editPackage.CanteenLocationEnum,
            };

            return View(model);
        }

        [Authorize(Policy = "CanteenEmployeeOnly")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> UpdatePackage(UpdatePackageViewModel newPackage) {
            if (!ModelState.IsValid) {
                var editPackage = _packageRepository.GetById(newPackage.Id);
                var checkboxProducts = _productRepository.GetProducts();
                var checkboxes = checkboxProducts.Select(product => new CheckboxViewModel { Product = product, IsChecked = editPackage.Products.Contains(product) }).ToList();
                var model = new UpdatePackageViewModel { ProductsAsCheckboxViewModel = checkboxes };
                return View(model);
            }

            try {
                var CanteenEmployee = _canteenEmployeeRepository.GetByName(User.Identity!.Name!);
                Package packageToUpdate = _packageService.PackageToUpdate(
                newPackage.Id,
                newPackage.Name,
                newPackage.SelectedProductsIds,
                newPackage.PickupDate,
                newPackage.EndOfPickupTime,
                newPackage.Price,
                newPackage.MealType,
                newPackage.CityEnum,
                CanteenEmployee.CanteenLocationEnum,
                CanteenEmployee.Id);

                if (ModelState.IsValid) {
                    await _packageRepository.UpdatePackage(packageToUpdate);
                    TempData["IsSuccessMessage"] = "Gelukt! Het pakket is met succes bewerkt.";
                    return RedirectToAction("List");
                }
            } catch (ArgumentException e) {
                ModelState.AddModelError("", e.Message);

                var editPackage = _packageRepository.GetById(newPackage.Id);
                var checkboxProducts = _productRepository.GetProducts();
                var checkboxes = checkboxProducts.Select(product => new CheckboxViewModel { Product = product, IsChecked = editPackage.Products.Contains(product) }).ToList();
                var model = new UpdatePackageViewModel { ProductsAsCheckboxViewModel = checkboxes };
                return View(model);
            }

            return View(newPackage);
        }

        [Authorize(Policy = "StudentOnly")]
        public IActionResult Reserve(int id) {
            var student = _studentRepository.GetByEmail(User.Identity!.Name!);
            try {
                _packageService.ReservePackage(id, student);
                TempData["IsSuccessMessage"] = "Gelukt! Het pakket is gereserveerd.";
                return RedirectToAction("Index");
            } catch (ArgumentException e) {
                TempData["ResultMessage"] = e.Message;
                return RedirectToAction("Index");
            }
        }

        [Authorize(Policy = "StudentOnly")]
        public RedirectToActionResult CancelReserve(int id) {
            _packageService.CancelReserve(id);
            TempData["IsSuccessMessage"] = "Gelukt! Uw reservering is geannuleerd.";
            return RedirectToAction("StudentReserved");
        }

        [Authorize(Policy = "CanteenEmployeeOnly")]
        public IActionResult Delete(int id) {
            try {
                _packageService.Delete(id);
                TempData["IsSuccessMessage"] = "Pakket is succesvol verwijderd.";
                return RedirectToAction("List");
            } catch (ArgumentException e) {
                TempData["ResultMessage"] = e.Message;
                return RedirectToAction("List");
            }
        }

        [Authorize(Policy = "CanteenEmployeeOnly")]
        public IActionResult AllReserved() {
            ViewData["Status"] = "Alle Gereserveerde";
            return View(_packageService.AllReserved());
        }

        [Authorize(Policy = "StudentOnly")]
        public IActionResult StudentReserved() {
            ViewData["Status"] = "Gereserveerde";
            return View(_packageService.StudentReserved(User.Identity!.Name!));
        }

        public IActionResult FilterPackages(string filterType, int filterValue) {
            string filterName = string.Empty;

            switch (filterType) {
                case "MealType":
                    filterName = ((MealType)filterValue).ToString().Replace("_", " ");
                    break;
                case "City":
                    filterName = ((CityEnum)filterValue).ToString().Replace("_", " ");
                    break;
            }

            ViewData["Status"] = filterName;
            return View("Index", _packageService.FilterPackages(filterType, filterValue));
        }

        [Authorize(Policy = "CanteenEmployeeOnly")]
        public IActionResult CanteenEmployeeLocationOverview() {
            ViewData["Status"] = "Eigen Locatie";
            return View("List", _packageService.CanteenEmployeeLocationOverview(User.Identity!.Name!));
        }

        public IActionResult AllIsEighteenPlusPackages() {
            ViewData["Status"] = "Alle 18+";
            return View("List", _packageService.AllIsEighteenPlusPackages());
        }

        public IActionResult AllIsNotEighteenPlusPackages() {
            ViewData["Status"] = "Alle leeftijden";
            return View("List", _packageService.AllIsNotEighteenPlusPackages());
        }
    }
}
