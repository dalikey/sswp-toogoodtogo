using Core.Domain;
using Core.DomainServices.Repos.Intf;
using Core.DomainServices.Services.Impl;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Core.DomainServices.Tests {
    public class PackageServiceBasicTests {

        [Fact]
        public void US_01_RecommendedPackages_Can_Be_Retrieved() {
            // Arrange
            var packageRepoMock = new Mock<IPackageRepository>();
            var productRepoMock = new Mock<IProductRepository>();
            var canteenEmployeeRepoMock = new Mock<ICanteenEmployeeRepository>();

            var sut = new PackageServiceBasic(packageRepoMock.Object, productRepoMock.Object, canteenEmployeeRepoMock.Object);

            packageRepoMock.Setup(packageRepo => packageRepo.GetAllRecommendedPackages()).Returns(CreatePackages());

            // Act
            var retrievedPackages = sut.GetAllRecommendedPackages();
            var hasPickupDate = false;
            foreach (var package in retrievedPackages) {
                if (package.PickupDate != null) {
                    hasPickupDate = true;
                }
            }

            // Assert
            Assert.Equal(8, retrievedPackages.Count());
            Assert.True(hasPickupDate);
        }

        [Fact]
        public void US_01_StudentReservedPackages_Can_Be_Retrieved() {
            // Arrange
            var packageRepoMock = new Mock<IPackageRepository>();
            var productRepoMock = new Mock<IProductRepository>();
            var canteenEmployeeRepoMock = new Mock<ICanteenEmployeeRepository>();
            var studentName = "firststudent@hotmail.com";
            var sut = new PackageServiceBasic(packageRepoMock.Object, productRepoMock.Object, canteenEmployeeRepoMock.Object);

            packageRepoMock.Setup(packageRepo => packageRepo.GetAll()).Returns(CreatePackages()
                .Where(p => p.ReservedBy?.Name == studentName)
                .AsQueryable());

            // Act
            var retrievedPackages = sut.StudentReserved(studentName);
            var hasPickupDate = false;
            foreach (var package in retrievedPackages) {
                if (package.PickupDate != null) {
                    hasPickupDate = true;
                }
            }

            // Assert
            Assert.Equal(2, retrievedPackages.Count());
            Assert.True(hasPickupDate);
        }

        [Fact]
        public void US_02_CanteenEmployeeLocationOverviewPackages_Can_Be_Retrieved_And_Is_Sorted_On_Date() {
            // Arrange
            var packageRepoMock = new Mock<IPackageRepository>();
            var productRepoMock = new Mock<IProductRepository>();
            var canteenEmployeeRepoMock = new Mock<ICanteenEmployeeRepository>();
            var employeeName = "firstemployee@hotmail.com";
            var sut = new PackageServiceBasic(packageRepoMock.Object, productRepoMock.Object, canteenEmployeeRepoMock.Object);

            canteenEmployeeRepoMock.Setup(canteenEmployeeRepo => canteenEmployeeRepo.GetByName(employeeName))
                .Returns(new CanteenEmployee {
                    Id = 1,
                    Name = employeeName,
                    EmployeeNumber = "123456",
                    CanteenLocationEnum = CanteenLocationEnum.Lovensdijkstraat
                });

            packageRepoMock.Setup(packageRepo => packageRepo.GetAll()).Returns(CreatePackages()
            .Where(p => p.Canteen!.CanteenEmployee.CanteenLocationEnum == CanteenLocationEnum.Lovensdijkstraat)
            .AsQueryable());

            // Act
            var retrievedPackages = sut.CanteenEmployeeLocationOverview(employeeName);

            // Assert
            Assert.Equal(3, retrievedPackages.Count());
            Assert.Equal(retrievedPackages.Where(p => p.CanteenLocationEnum == retrievedPackages.First().CanteenLocationEnum),
            retrievedPackages);
            Assert.Equal(retrievedPackages.OrderBy(p => p.PickupDate), retrievedPackages);
        }

        [Fact]
        public void US_02_AllCanteensPackages_Can_Be_Retrieved_And_Is_Sorted_On_Date() {
            // Arrange
            var packageRepoMock = new Mock<IPackageRepository>();
            var productRepoMock = new Mock<IProductRepository>();
            var canteenEmployeeRepoMock = new Mock<ICanteenEmployeeRepository>();

            var sut = new PackageServiceBasic(packageRepoMock.Object, productRepoMock.Object, canteenEmployeeRepoMock.Object);

            packageRepoMock.Setup(packageRepo => packageRepo.GetAll()).Returns(CreatePackages().OrderBy(p => p.PickupDate).AsQueryable());

            // Act
            var retrievedPackages = sut.GetAll();

            // Assert
            Assert.Equal(8, retrievedPackages.Count());
            Assert.Equal(retrievedPackages.OrderBy(p => p.PickupDate).ToList(), retrievedPackages);
        }

        [Fact]
        public void US_03_Package_With_Products_Can_Be_Added_And_Location_Is_From_CanteenEmployee() {
            // Arrange
            var packageRepoMock = new Mock<IPackageRepository>();
            var productRepoMock = new Mock<IProductRepository>();
            var canteenEmployeeRepoMock = new Mock<ICanteenEmployeeRepository>();
            var selectedProductsIds = new List<int>();

            var sut = new PackageServiceBasic(packageRepoMock.Object, productRepoMock.Object, canteenEmployeeRepoMock.Object);

            // Act
            selectedProductsIds.Append(1);
            selectedProductsIds.Append(2);
            var createdPackage = sut.PackageToCreate(
                "Panini kip",
                selectedProductsIds,
                DateTime.Now,
                DateTime.Now,
                5.20m,
                MealType.Brood,
                CityEnum.Breda,
                CanteenLocationEnum.Lovensdijkstraat,
                123456);

            // Assert
            Assert.NotNull(createdPackage);
            Assert.Equal(CanteenLocationEnum.Lovensdijkstraat, createdPackage.CanteenLocationEnum);
        }

        [Fact]
        public void US_03_Adding_Package_pickupDate_Two_Days_Later_Should_Throw_Error() {
            // Arrange
            var packageRepoMock = new Mock<IPackageRepository>();
            var productRepoMock = new Mock<IProductRepository>();
            var canteenEmployeeRepoMock = new Mock<ICanteenEmployeeRepository>();
            var selectedProductsIds = new List<int>();

            var sut = new PackageServiceBasic(packageRepoMock.Object, productRepoMock.Object, canteenEmployeeRepoMock.Object);

            // Act
            selectedProductsIds.Append(1);
            selectedProductsIds.Append(2);
            Action act = () => sut.PackageToCreate(
                "Panini kip",
                selectedProductsIds,
                DateTime.Now,
                DateTime.Now.AddDays(2),
                5.20m,
                MealType.Brood,
                CityEnum.Breda,
                CanteenLocationEnum.Lovensdijkstraat,
                123456);

            // Assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            Assert.Equal("Einde ophaaltijd mag maximaal 2 dagen vooruit gepland worden", exception.Message);
        }

        [Fact]
        public void US_03_Adding_Package_pickupDate_In_The_Past_Should_Throw_Error() {
            // Arrange
            var packageRepoMock = new Mock<IPackageRepository>();
            var productRepoMock = new Mock<IProductRepository>();
            var canteenEmployeeRepoMock = new Mock<ICanteenEmployeeRepository>();
            var selectedProductsIds = new List<int>();

            var sut = new PackageServiceBasic(packageRepoMock.Object, productRepoMock.Object, canteenEmployeeRepoMock.Object);

            // Act
            selectedProductsIds.Append(1);
            selectedProductsIds.Append(2);
            Action act = () => sut.PackageToCreate(
                "Panini kip",
                selectedProductsIds,
                new DateTime(2022, 12, 26, 12, 30, 00),
                new DateTime(2022, 12, 26, 12, 30, 00),
                5.20m,
                MealType.Brood,
                CityEnum.Breda,
                CanteenLocationEnum.Lovensdijkstraat,
                123456);

            // Assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            Assert.Equal("Ophaaldatum kan niet in het verleden worden gemaakt", exception.Message);
        }

        [Fact]
        public void US_03_Adding_Package_endOfPickupTime_Two_Days_Later_Should_Throw_Error() {
            // Arrange
            var packageRepoMock = new Mock<IPackageRepository>();
            var productRepoMock = new Mock<IProductRepository>();
            var canteenEmployeeRepoMock = new Mock<ICanteenEmployeeRepository>();
            var selectedProductsIds = new List<int>();

            var sut = new PackageServiceBasic(packageRepoMock.Object, productRepoMock.Object, canteenEmployeeRepoMock.Object);

            // Act
            selectedProductsIds.Append(1);
            selectedProductsIds.Append(2);
            Action act = () => sut.PackageToCreate(
                "Panini kip",
                selectedProductsIds,
                DateTime.Now,
                DateTime.Now.AddDays(2),
                5.20m,
                MealType.Brood,
                CityEnum.Breda,
                CanteenLocationEnum.Lovensdijkstraat,
                123456);

            // Assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            Assert.Equal("Einde ophaaltijd mag maximaal 2 dagen vooruit gepland worden", exception.Message);
        }

        [Fact]
        public void US_03_Adding_Package_endOfPickupTime_In_The_Past_Should_Throw_Error() {
            // Arrange
            var packageRepoMock = new Mock<IPackageRepository>();
            var productRepoMock = new Mock<IProductRepository>();
            var canteenEmployeeRepoMock = new Mock<ICanteenEmployeeRepository>();
            var selectedProductsIds = new List<int>();

            var sut = new PackageServiceBasic(packageRepoMock.Object, productRepoMock.Object, canteenEmployeeRepoMock.Object);

            // Act
            selectedProductsIds.Append(1);
            selectedProductsIds.Append(2);
            Action act = () => sut.PackageToCreate(
                "Panini kip",
                selectedProductsIds,
                new DateTime(2022, 12, 26, 12, 30, 00),
                new DateTime(2022, 12, 26, 12, 30, 00),
                5.20m,
                MealType.Brood,
                CityEnum.Breda,
                CanteenLocationEnum.Lovensdijkstraat,
                123456);

            // Assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            Assert.Equal("Ophaaldatum kan niet in het verleden worden gemaakt", exception.Message);
        }

        [Fact]
        public void US_03_Package_With_Products_Can_Be_Updated_And_Location_Is_From_CanteenEmployee() {
            // Arrange
            var packageRepoMock = new Mock<IPackageRepository>();
            var productRepoMock = new Mock<IProductRepository>();
            var canteenEmployeeRepoMock = new Mock<ICanteenEmployeeRepository>();
            IEnumerable<int> selectedProductsIds = new List<int>();

            var sut = new PackageServiceBasic(packageRepoMock.Object, productRepoMock.Object, canteenEmployeeRepoMock.Object);

            // Act
            selectedProductsIds.Append(1);
            selectedProductsIds.Append(2);
            var updatedPackage = sut.PackageToUpdate(
                3,
                "Panini kip",
                selectedProductsIds,
                DateTime.Now,
                DateTime.Now,
                5.20m,
                MealType.Brood,
                CityEnum.Breda,
                CanteenLocationEnum.Lovensdijkstraat,
                123456);

            // Assert
            Assert.NotNull(updatedPackage);
            Assert.Equal(CanteenLocationEnum.Lovensdijkstraat, updatedPackage.CanteenLocationEnum);
        }

        [Fact]
        public void US_03_Updating_Package_pickupDate_Two_Days_Later_Should_Throw_Error() {
            // Arrange
            var packageRepoMock = new Mock<IPackageRepository>();
            var productRepoMock = new Mock<IProductRepository>();
            var canteenEmployeeRepoMock = new Mock<ICanteenEmployeeRepository>();
            var selectedProductsIds = new List<int>();

            var sut = new PackageServiceBasic(packageRepoMock.Object, productRepoMock.Object, canteenEmployeeRepoMock.Object);

            // Act
            selectedProductsIds.Append(1);
            selectedProductsIds.Append(2);
            Action act = () => sut.PackageToUpdate(
                3,
                "Panini kip",
                selectedProductsIds,
                DateTime.Now,
                DateTime.Now.AddDays(2),
                5.20m,
                MealType.Brood,
                CityEnum.Breda,
                CanteenLocationEnum.Lovensdijkstraat,
                123456);

            // Assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            Assert.Equal("Einde ophaaltijd mag maximaal 2 dagen vooruit gepland worden", exception.Message);
        }

        [Fact]
        public void US_03_Updating_Package_pickupDate_In_The_Past_Should_Throw_Error() {
            // Arrange
            var packageRepoMock = new Mock<IPackageRepository>();
            var productRepoMock = new Mock<IProductRepository>();
            var canteenEmployeeRepoMock = new Mock<ICanteenEmployeeRepository>();
            var selectedProductsIds = new List<int>();

            var sut = new PackageServiceBasic(packageRepoMock.Object, productRepoMock.Object, canteenEmployeeRepoMock.Object);

            // Act
            selectedProductsIds.Append(1);
            selectedProductsIds.Append(2);
            Action act = () => sut.PackageToUpdate(
                3,
                "Panini kip",
                selectedProductsIds,
                new DateTime(2022, 12, 26, 12, 30, 00),
                new DateTime(2022, 12, 26, 12, 30, 00),
                5.20m,
                MealType.Brood,
                CityEnum.Breda,
                CanteenLocationEnum.Lovensdijkstraat,
                123456);

            // Assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            Assert.Equal("Ophaaldatum kan niet in het verleden worden gemaakt", exception.Message);
        }

        [Fact]
        public void US_03_Updating_Package_endOfPickupTime_Two_Days_Later_Should_Throw_Error() {
            // Arrange
            var packageRepoMock = new Mock<IPackageRepository>();
            var productRepoMock = new Mock<IProductRepository>();
            var canteenEmployeeRepoMock = new Mock<ICanteenEmployeeRepository>();
            var selectedProductsIds = new List<int>();

            var sut = new PackageServiceBasic(packageRepoMock.Object, productRepoMock.Object, canteenEmployeeRepoMock.Object);

            // Act
            selectedProductsIds.Append(1);
            selectedProductsIds.Append(2);
            Action act = () => sut.PackageToUpdate(
                3,
                "Panini kip",
                selectedProductsIds,
                DateTime.Now,
                DateTime.Now.AddDays(2),
                5.20m,
                MealType.Brood,
                CityEnum.Breda,
                CanteenLocationEnum.Lovensdijkstraat,
                123456);

            // Assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            Assert.Equal("Einde ophaaltijd mag maximaal 2 dagen vooruit gepland worden", exception.Message);
        }

        [Fact]
        public void US_03_Updating_Package_endOfPickupTime_In_The_Past_Should_Throw_Error() {
            // Arrange
            var packageRepoMock = new Mock<IPackageRepository>();
            var productRepoMock = new Mock<IProductRepository>();
            var canteenEmployeeRepoMock = new Mock<ICanteenEmployeeRepository>();
            var selectedProductsIds = new List<int>();

            var sut = new PackageServiceBasic(packageRepoMock.Object, productRepoMock.Object, canteenEmployeeRepoMock.Object);

            // Act
            selectedProductsIds.Append(1);
            selectedProductsIds.Append(2);
            Action act = () => sut.PackageToUpdate(
                3,
                "Panini kip",
                selectedProductsIds,
                new DateTime(2022, 12, 26, 12, 30, 00),
                new DateTime(2022, 12, 26, 12, 30, 00),
                5.20m,
                MealType.Brood,
                CityEnum.Breda,
                CanteenLocationEnum.Lovensdijkstraat,
                123456);

            // Assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            Assert.Equal("Ophaaldatum kan niet in het verleden worden gemaakt", exception.Message);
        }

        [Fact]
        public void US_03_Package_Can_Be_Deleted() {
            // Arrange
            var packageRepoMock = new Mock<IPackageRepository>();
            var productRepoMock = new Mock<IProductRepository>();
            var canteenEmployeeRepoMock = new Mock<ICanteenEmployeeRepository>();
            var id = 8;
            var sut = new PackageServiceBasic(packageRepoMock.Object, productRepoMock.Object, canteenEmployeeRepoMock.Object);

            packageRepoMock.Setup(packageRepo => packageRepo.GetById(id)).Returns(CreatePackages().SingleOrDefault(p => p.Id == id));

            // Act
            sut.Delete(id);

            // Assert
            packageRepoMock.Verify(packageRepo => packageRepo.Delete(It.IsAny<Package>()), Times.Once());
        }

        [Fact]
        public void US_03_Deleting_Package_Should_Thow_Error() {
            // Arrange
            var packageRepoMock = new Mock<IPackageRepository>();
            var productRepoMock = new Mock<IProductRepository>();
            var canteenEmployeeRepoMock = new Mock<ICanteenEmployeeRepository>();
            var id = 9;
            var sut = new PackageServiceBasic(packageRepoMock.Object, productRepoMock.Object, canteenEmployeeRepoMock.Object);

            packageRepoMock.Setup(packageRepo => packageRepo.GetById(id)).Returns(CreatePackages().SingleOrDefault(p => p.Id == id));

            // Act
            Action act = () => sut.Delete(id);

            // Assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            Assert.Equal("Pakket mag niet verwijdert worden omdat deze al gereserveerd is door een student.", exception.Message);
        }

        [Fact]
        public void US_03_CanteenEmployeeLocationOverviewPackages_Can_Be_Retrieved_And_Is_Sorted_On_Date() {
            // Arrange
            var packageRepoMock = new Mock<IPackageRepository>();
            var productRepoMock = new Mock<IProductRepository>();
            var canteenEmployeeRepoMock = new Mock<ICanteenEmployeeRepository>();
            var EmployeeName = "firstemployee@hotmail.com";
            var sut = new PackageServiceBasic(packageRepoMock.Object, productRepoMock.Object, canteenEmployeeRepoMock.Object);

            canteenEmployeeRepoMock.Setup(canteenEmployeeRepo => canteenEmployeeRepo.GetByName(EmployeeName))
                .Returns(new CanteenEmployee {
                    Id = 1,
                    Name = EmployeeName,
                    EmployeeNumber = "123456",
                    CanteenLocationEnum = CanteenLocationEnum.Lovensdijkstraat
                });

            packageRepoMock.Setup(packageRepo => packageRepo.GetAll()).Returns(CreatePackages()
            .Where(p => p.Canteen!.CanteenEmployee.CanteenLocationEnum == CanteenLocationEnum.Lovensdijkstraat)
            .AsQueryable());

            // Act
            var retrievedPackages = sut.CanteenEmployeeLocationOverview(EmployeeName);

            // Assert
            Assert.Equal(3, retrievedPackages.Count());
            Assert.Equal(retrievedPackages.Where(p => p.CanteenLocationEnum == retrievedPackages.First().CanteenLocationEnum),
            retrievedPackages);
            Assert.Equal(retrievedPackages.OrderBy(p => p.PickupDate), retrievedPackages);
        }

        [Fact]
        public void US_04_Product_Contains_Property_ContainsAlcohol() {
            // Arrange
            var packageRepoMock = new Mock<IPackageRepository>();
            var productRepoMock = new Mock<IProductRepository>();
            var canteenEmployeeRepoMock = new Mock<ICanteenEmployeeRepository>();

            var sut = new PackageServiceBasic(packageRepoMock.Object, productRepoMock.Object, canteenEmployeeRepoMock.Object);

            packageRepoMock.Setup(packageRepo => packageRepo.GetAll()).Returns(CreatePackages().OrderBy(p => p.PickupDate).AsQueryable());

            // Act
            var retrievedPackage = sut.GetAll().First();

            // Assert
            Assert.True(retrievedPackage.Products.First().ContainsAlcohol);
        }

        [Fact]
        public void US_04_Package_Contains_Property_IsEighteenPlus() {
            // Arrange
            var packageRepoMock = new Mock<IPackageRepository>();
            var productRepoMock = new Mock<IProductRepository>();
            var canteenEmployeeRepoMock = new Mock<ICanteenEmployeeRepository>();

            var sut = new PackageServiceBasic(packageRepoMock.Object, productRepoMock.Object, canteenEmployeeRepoMock.Object);

            packageRepoMock.Setup(packageRepo => packageRepo.GetAll()).Returns(CreatePackages().OrderBy(p => p.PickupDate).AsQueryable());

            // Act
            var retrievedPackage = sut.GetAll().First();

            // Assert
            Assert.True(retrievedPackage.IsEighteenPlus);
        }

        [Fact]
        public void US_04_Student_Cannot_Reserve_Package_If_Under_Age() {
            // Arrange
            var packageRepoMock = new Mock<IPackageRepository>();
            var productRepoMock = new Mock<IProductRepository>();
            var canteenEmployeeRepoMock = new Mock<ICanteenEmployeeRepository>();
            var id = 1;
            var student = new Student { Id = 2, Name = "secondstudent@hotmail.com", Birthdate = new DateTime(2005, 10, 28, 18, 11, 12, 15), StudentNumber = "2122346", Emailadres = "secondstudent@hotmail.com", StudyCity = CityEnum.Den_Bosch, Phonenumber = "0612345622" };
            var sut = new PackageServiceBasic(packageRepoMock.Object, productRepoMock.Object, canteenEmployeeRepoMock.Object);

            packageRepoMock.Setup(packageRepo => packageRepo.GetAll()).Returns(CreatePackages().OrderBy(p => p.PickupDate).AsQueryable());
            packageRepoMock.Setup(packageRepo => packageRepo.GetById(id)).Returns(CreatePackages().SingleOrDefault(p => p.Id == id));

            // Act
            Action act = () => sut.ReservePackage(id, student);

            // Assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            Assert.Equal("Helaas je bent nog geen 18+.", exception.Message);
        }

        [Fact]
        public void US_05_Student_Can_Reserve_Package() {
            // Arrange
            var packageRepoMock = new Mock<IPackageRepository>();
            var productRepoMock = new Mock<IProductRepository>();
            var canteenEmployeeRepoMock = new Mock<ICanteenEmployeeRepository>();
            var id = 5;
            var student = new Student { Id = 1, Name = "newstudent@hotmail.com", Birthdate = new DateTime(2001, 6, 21, 18, 36, 13, 25), StudentNumber = "2142645", Emailadres = "newstudent@hotmail.com", StudyCity = CityEnum.Breda, Phonenumber = "0612345611" };
            var sut = new PackageServiceBasic(packageRepoMock.Object, productRepoMock.Object, canteenEmployeeRepoMock.Object);

            packageRepoMock.Setup(packageRepo => packageRepo.GetAll()).Returns(CreatePackages().OrderBy(p => p.PickupDate).AsQueryable());
            packageRepoMock.Setup(packageRepo => packageRepo.GetById(id)).Returns(CreatePackages().SingleOrDefault(p => p.Id == id));

            // Act
            sut.ReservePackage(id, student);

            // Assert
            packageRepoMock.Verify(packageRepo => packageRepo.AsignReserve(It.IsAny<Package>(), It.IsAny<Student>()), Times.Once());
        }

        [Fact]
        public void US_05_Student_Can_Only_Reserve_One_Package_Per_Pickup_Day() {
            // Arrange
            var packageRepoMock = new Mock<IPackageRepository>();
            var productRepoMock = new Mock<IProductRepository>();
            var canteenEmployeeRepoMock = new Mock<ICanteenEmployeeRepository>();
            var id = 1;
            var student = new Student { Id = 1, Name = "firststudent@hotmail.com", Birthdate = new DateTime(2000, 7, 22, 18, 36, 13, 25), StudentNumber = "2142135", Emailadres = "firststudent@hotmail.com", StudyCity = CityEnum.Breda, Phonenumber = "0612345611" };
            var sut = new PackageServiceBasic(packageRepoMock.Object, productRepoMock.Object, canteenEmployeeRepoMock.Object);

            packageRepoMock.Setup(packageRepo => packageRepo.GetAll()).Returns(CreatePackages().OrderBy(p => p.PickupDate).AsQueryable());
            packageRepoMock.Setup(packageRepo => packageRepo.GetById(id)).Returns(CreatePackages().SingleOrDefault(p => p.Id == id));

            // Act
            Action act = () => sut.ReservePackage(id, student);
            Action act2 = () => sut.ReservePackage(id, student);

            // Assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act2);
            Assert.Equal("Je mag maximaal 1 pakket per afhaaldag reserveren.", exception.Message);
        }

        [Fact]
        public void US_06_Package_Has_Products() {
            // Arrange
            var packageRepoMock = new Mock<IPackageRepository>();
            var productRepoMock = new Mock<IProductRepository>();
            var canteenEmployeeRepoMock = new Mock<ICanteenEmployeeRepository>();
            var id = 1;
            var sut = new PackageServiceBasic(packageRepoMock.Object, productRepoMock.Object, canteenEmployeeRepoMock.Object);

            packageRepoMock.Setup(packageRepo => packageRepo.GetAll()).Returns(CreatePackages().OrderBy(p => p.PickupDate).AsQueryable());
            packageRepoMock.Setup(packageRepo => packageRepo.GetById(id)).Returns(CreatePackages().SingleOrDefault(p => p.Id == id));

            // Act
            var retrievedPackage = sut.GetAll().First();

            // Assert
            Assert.NotNull(retrievedPackage.Products);
        }

        [Fact]
        public void US_07_Student_Can_Only_Reserve_When_Package_Is_Not_Reserved() {
            // Arrange
            var packageRepoMock = new Mock<IPackageRepository>();
            var productRepoMock = new Mock<IProductRepository>();
            var canteenEmployeeRepoMock = new Mock<ICanteenEmployeeRepository>();
            var id = 9;
            var student = new Student { Id = 1, Name = "firststudent@hotmail.com", Birthdate = new DateTime(2000, 7, 22, 18, 36, 13, 25), StudentNumber = "2142135", Emailadres = "firststudent@hotmail.com", StudyCity = CityEnum.Breda, Phonenumber = "0612345611" };
            var sut = new PackageServiceBasic(packageRepoMock.Object, productRepoMock.Object, canteenEmployeeRepoMock.Object);

            packageRepoMock.Setup(packageRepo => packageRepo.GetAll()).Returns(CreatePackages().OrderBy(p => p.PickupDate).AsQueryable());
            packageRepoMock.Setup(packageRepo => packageRepo.GetById(id)).Returns(CreatePackages().SingleOrDefault(p => p.Id == id));

            // Act
            Action act = () => sut.ReservePackage(id, student);

            // Assert
            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            Assert.Equal("Excuses, het pakket is al gereserveerd door een ander student.", exception.Message);
        }

        [Fact]
        public void US_07_Student_Can_Reserve_Package_And_Is_Assigned() {
            // Arrange
            var packageRepoMock = new Mock<IPackageRepository>();
            var productRepoMock = new Mock<IProductRepository>();
            var canteenEmployeeRepoMock = new Mock<ICanteenEmployeeRepository>();
            var id = 5;
            var student = new Student { Id = 1, Name = "newstudent@hotmail.com", Birthdate = new DateTime(2001, 6, 21, 18, 36, 13, 25), StudentNumber = "2142645", Emailadres = "newstudent@hotmail.com", StudyCity = CityEnum.Breda, Phonenumber = "0612345611" };
            var sut = new PackageServiceBasic(packageRepoMock.Object, productRepoMock.Object, canteenEmployeeRepoMock.Object);

            packageRepoMock.Setup(packageRepo => packageRepo.GetAll()).Returns(CreatePackages().OrderBy(p => p.PickupDate).AsQueryable());
            packageRepoMock.Setup(packageRepo => packageRepo.GetById(id)).Returns(CreatePackages().SingleOrDefault(p => p.Id == id));

            // Act
            sut.ReservePackage(id, student);

            // Assert
            packageRepoMock.Verify(packageRepo => packageRepo.AsignReserve(It.IsAny<Package>(), It.IsAny<Student>()), Times.Once());
        }

        [Fact]
        public void US_08_StartAtRecommended_Can_Be_Retrieved_And_Filtered() {
            // Arrange
            var packageRepoMock = new Mock<IPackageRepository>();
            var productRepoMock = new Mock<IProductRepository>();
            var canteenEmployeeRepoMock = new Mock<ICanteenEmployeeRepository>();
            var studentStudyCity = CityEnum.Breda;
            var sut = new PackageServiceBasic(packageRepoMock.Object, productRepoMock.Object, canteenEmployeeRepoMock.Object);

            packageRepoMock.Setup(packageRepo => packageRepo.GetAllRecommendedPackages()).Returns(CreatePackages());

            // Act
            var retrievedPackages = sut.StartAtRecommended(studentStudyCity);

            // Assert
            Assert.Equal(3, retrievedPackages.Count());
        }

        [Fact]
        public void US_08_StartAtRecommended_Can_Be_Easily_Be_Changed_And_Filtered() {
            // Arrange
            var packageRepoMock = new Mock<IPackageRepository>();
            var productRepoMock = new Mock<IProductRepository>();
            var canteenEmployeeRepoMock = new Mock<ICanteenEmployeeRepository>();
            var sut = new PackageServiceBasic(packageRepoMock.Object, productRepoMock.Object, canteenEmployeeRepoMock.Object);

            packageRepoMock.Setup(packageRepo => packageRepo.GetAllRecommendedPackages()).Returns(CreatePackages());

            // Act
            var retrievedAllRecommendedPackages = sut.GetAllRecommendedPackages();

            // Assert
            Assert.Equal(8, retrievedAllRecommendedPackages.Count());
        }

        [Fact]
        public void US_08_Packages_Can_Be_Filtered_On_MealType() {
            // Arrange
            var packageRepoMock = new Mock<IPackageRepository>();
            var productRepoMock = new Mock<IProductRepository>();
            var canteenEmployeeRepoMock = new Mock<ICanteenEmployeeRepository>();
            var sut = new PackageServiceBasic(packageRepoMock.Object, productRepoMock.Object, canteenEmployeeRepoMock.Object);

            packageRepoMock.Setup(packageRepo => packageRepo.GetAllRecommendedPackages()).Returns(CreatePackages());

            // Act
            var retrievedAllRecommendedPackages = sut.GetAllRecommendedPackages();

            // Assert
            Assert.Equal(8, retrievedAllRecommendedPackages.Count());
        }

        private List<Package> CreatePackages() {
            var products = new List<Product> { new Product { Id = 7, Name = "Bier", ContainsAlcohol = true, ImageUrl = "https://media-cdn.tripadvisor.com/media/photo-s/1b/78/81/ac/jabeerwocky-craft-beer.jpg" } };
            return new List<Package> {
            new Package { Id = 1, Name = "Speciaal soep", PickupDate = DateTime.Now.AddHours(1), EndOfPickupTime = DateTime.Now.AddHours(2), IsEighteenPlus = true, Price = 1.50m, MealType = MealType.Drank, ReservedBy = null, Canteen = new Canteen{CanteenEmployee = new CanteenEmployee { Name = "firstemployee@hotmail.com", CanteenLocationEnum = CanteenLocationEnum.Lovensdijkstraat }, CityEnum = CityEnum.Breda, CanteenLocationEnum = CanteenLocationEnum.Lovensdijkstraat }, CityEnum = CityEnum.Breda, CanteenLocationEnum = CanteenLocationEnum.Lovensdijkstraat, Products = products },
            new Package { Id = 2, Name = "Fruitmix" , PickupDate = DateTime.Now.AddHours(1), EndOfPickupTime = DateTime.Now.AddHours(2), IsEighteenPlus = false, Price = 1.20m, MealType = MealType.Snack, ReservedBy = new Student { Name = "secondstudent@hotmail.com"}, ReservedById = 2, Canteen = new Canteen{CanteenEmployee = new CanteenEmployee { Name = "firstemployee@hotmail.com", CanteenLocationEnum = CanteenLocationEnum.Lovensdijkstraat }, CityEnum = CityEnum.Breda, CanteenLocationEnum = CanteenLocationEnum.Lovensdijkstraat }, CityEnum = CityEnum.Breda, CanteenLocationEnum = CanteenLocationEnum.Lovensdijkstraat },
            new Package { Id = 3, Name = "Panini mix" , PickupDate = DateTime.Now.AddHours(1).AddDays(1), EndOfPickupTime = DateTime.Now.AddHours(2).AddDays(1), IsEighteenPlus = false, Price = 5.20m, MealType = MealType.Brood, ReservedBy = null, Canteen = new Canteen{CanteenEmployee = new CanteenEmployee { Name = "firstemployee@hotmail.com", CanteenLocationEnum = CanteenLocationEnum.Lovensdijkstraat }, CityEnum = CityEnum.Breda, CanteenLocationEnum = CanteenLocationEnum.Lovensdijkstraat }, CityEnum = CityEnum.Breda, CanteenLocationEnum = CanteenLocationEnum.Lovensdijkstraat },
            new Package { Id = 5, Name = "Ijs mix" ,  PickupDate = DateTime.Now.AddHours(1).AddDays(1), EndOfPickupTime = DateTime.Now.AddHours(2).AddDays(1), IsEighteenPlus = false, Price = 1.50m, MealType = MealType.Nagerecht, ReservedBy = null, Canteen = new Canteen{CanteenEmployee = new CanteenEmployee { Name = "secondemployee@hotmail.com", CanteenLocationEnum = CanteenLocationEnum.Hogeschoollaan }, CityEnum = CityEnum.Tilburg, CanteenLocationEnum = CanteenLocationEnum.Hogeschoollaan }, CityEnum = CityEnum.Tilburg, CanteenLocationEnum = CanteenLocationEnum.Hogeschoollaan },
            new Package { Id = 6, Name = "Coffee mix" ,  PickupDate = DateTime.Now.AddHours(1).AddDays(1), EndOfPickupTime = DateTime.Now.AddHours(2).AddDays(1), IsEighteenPlus = false, Price = 3.20m, MealType = MealType.Drank, ReservedBy = new Student { Name = "firststudent@hotmail.com"}, Canteen = new Canteen{CanteenEmployee = new CanteenEmployee { Name = "secondemployee@hotmail.com", CanteenLocationEnum = CanteenLocationEnum.Hogeschoollaan }, CityEnum = CityEnum.Tilburg, CanteenLocationEnum = CanteenLocationEnum.Hogeschoollaan }, CityEnum = CityEnum.Tilburg, CanteenLocationEnum = CanteenLocationEnum.Hogeschoollaan },
            new Package { Id = 7, Name = "Drank mix" , PickupDate = DateTime.Now.AddHours(1).AddDays(2), EndOfPickupTime = DateTime.Now.AddHours(2).AddDays(2), IsEighteenPlus = true, Price = 3.70m, MealType = MealType.Drank, ReservedBy = new Student { Name = "secondstudent@hotmail.com"}, Canteen = new Canteen{CanteenEmployee = new CanteenEmployee { Name = "thirdemployee@hotmail.com", CanteenLocationEnum = CanteenLocationEnum.Beukenlaan }, CityEnum = CityEnum.Den_Bosch, CanteenLocationEnum = CanteenLocationEnum.Beukenlaan }, CityEnum = CityEnum.Den_Bosch, CanteenLocationEnum = CanteenLocationEnum.Beukenlaan },
            new Package { Id = 8, Name = "Snack mix" , PickupDate = DateTime.Now.AddHours(1).AddDays(2), EndOfPickupTime = DateTime.Now.AddHours(2).AddDays(2), IsEighteenPlus = false, Price = 4.20m, MealType = MealType.Snack, ReservedBy = null, Canteen = new Canteen{CanteenEmployee = new CanteenEmployee { Name = "thirdemployee@hotmail.com", CanteenLocationEnum = CanteenLocationEnum.Beukenlaan }, CityEnum = CityEnum.Den_Bosch, CanteenLocationEnum = CanteenLocationEnum.Beukenlaan }, CityEnum = CityEnum.Den_Bosch, CanteenLocationEnum = CanteenLocationEnum.Beukenlaan },
            new Package { Id = 9, Name = "Koekjes mix" , PickupDate = DateTime.Now.AddHours(1).AddDays(2), EndOfPickupTime = DateTime.Now.AddHours(2).AddDays(2), IsEighteenPlus = false, Price = 6.90m, MealType = MealType.Snack, ReservedBy = new Student { Name = "firststudent@hotmail.com"}, Canteen = new Canteen{CanteenEmployee = new CanteenEmployee { Name = "thirdemployee@hotmail.com", CanteenLocationEnum = CanteenLocationEnum.Beukenlaan }, CityEnum = CityEnum.Den_Bosch, CanteenLocationEnum = CanteenLocationEnum.Beukenlaan }, CityEnum = CityEnum.Den_Bosch, CanteenLocationEnum = CanteenLocationEnum.Beukenlaan }};
        }
    }
}