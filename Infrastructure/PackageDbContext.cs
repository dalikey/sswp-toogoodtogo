using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure {
    public class PackageDbContext : DbContext {
        public DbSet<CanteenEmployee> CanteenEmployees { get; set; }
        public DbSet<Canteen> Canteens { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Student> Students { get; set; }

        public PackageDbContext(DbContextOptions<PackageDbContext> contextOptions) : base(contextOptions) {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            IEnumerable<CanteenEmployee> canteenEmployees = new List<CanteenEmployee> {
            new CanteenEmployee { Id = 1, Name = "firstemployee@hotmail.com", EmployeeNumber = "123456", CanteenLocationEnum = CanteenLocationEnum.Lovensdijkstraat },
            new CanteenEmployee { Id = 2, Name = "secondemployee@hotmail.com", EmployeeNumber = "123457", CanteenLocationEnum = CanteenLocationEnum.Hogeschoollaan },
            new CanteenEmployee { Id = 3, Name = "Amanda Brook", EmployeeNumber = "456791", CanteenLocationEnum = CanteenLocationEnum.Beukenlaan }};

            IEnumerable<Canteen> canteens = new List<Canteen> {
            new Canteen { Id = 1, CityEnum = CityEnum.Breda, CanteenLocationEnum = CanteenLocationEnum.Lovensdijkstraat, OffersHotMeals = true, CanteenEmployeeId = 1 },
            new Canteen { Id = 2, CityEnum = CityEnum.Tilburg, CanteenLocationEnum = CanteenLocationEnum.Hogeschoollaan, OffersHotMeals = true, CanteenEmployeeId = 2 },
            new Canteen { Id = 3, CityEnum = CityEnum.Den_Bosch, CanteenLocationEnum = CanteenLocationEnum.Beukenlaan, OffersHotMeals = false, CanteenEmployeeId = 3 }};

            IEnumerable<Package> packages = new List<Package> {
            new Package { Id = 1, Name = "Speciaal soep", PickupDate = DateTime.Now.AddHours(1), EndOfPickupTime = DateTime.Now.AddHours(2), IsEighteenPlus = false, Price = 1.50m, MealType = MealType.Drank, ReservedById = 1, CanteenId = 1, CityEnum = CityEnum.Breda, CanteenLocationEnum = CanteenLocationEnum.Lovensdijkstraat },
            new Package { Id = 2, Name = "Fruitmix" , PickupDate = DateTime.Now.AddHours(1), EndOfPickupTime = DateTime.Now.AddHours(2), IsEighteenPlus = false, Price = 1.20m, MealType = MealType.Snack, ReservedById = 2, CanteenId = 1, CityEnum = CityEnum.Breda, CanteenLocationEnum = CanteenLocationEnum.Lovensdijkstraat },
            new Package { Id = 3, Name = "Panini mix" , PickupDate = DateTime.Now.AddHours(1).AddDays(1), EndOfPickupTime = DateTime.Now.AddHours(2).AddDays(1), IsEighteenPlus = false, Price = 5.20m, MealType = MealType.Brood, CanteenId = 1, CityEnum = CityEnum.Breda, CanteenLocationEnum = CanteenLocationEnum.Lovensdijkstraat },
            new Package { Id = 4, Name = "Ijs mix" , PickupDate = DateTime.Now.AddHours(1).AddDays(1), EndOfPickupTime = DateTime.Now.AddHours(2).AddDays(1), IsEighteenPlus = false, Price = 1.50m, MealType = MealType.Nagerecht, CanteenId = 2, CityEnum = CityEnum.Tilburg, CanteenLocationEnum = CanteenLocationEnum.Hogeschoollaan },
            new Package { Id = 5, Name = "Coffee mix" , PickupDate = DateTime.Now.AddHours(1).AddDays(1), EndOfPickupTime = DateTime.Now.AddHours(2).AddDays(1), IsEighteenPlus = false, Price = 3.20m, MealType = MealType.Drank, CanteenId = 2, CityEnum = CityEnum.Tilburg, CanteenLocationEnum = CanteenLocationEnum.Hogeschoollaan },
            new Package { Id = 6, Name = "Drank mix" , PickupDate = DateTime.Now.AddHours(1).AddDays(2), EndOfPickupTime = DateTime.Now.AddHours(2).AddDays(2), IsEighteenPlus = true, Price = 3.70m, MealType = MealType.Drank, CanteenId = 3, CityEnum = CityEnum.Den_Bosch, CanteenLocationEnum = CanteenLocationEnum.Beukenlaan },
            new Package { Id = 7, Name = "Snack mix" , PickupDate = DateTime.Now.AddHours(1).AddDays(2), EndOfPickupTime = DateTime.Now.AddHours(2).AddDays(2), IsEighteenPlus = false, Price = 4.20m, MealType = MealType.Snack, CanteenId = 3, CityEnum = CityEnum.Den_Bosch, CanteenLocationEnum = CanteenLocationEnum.Beukenlaan },
            new Package { Id = 8, Name = "Koekjes mix" , PickupDate = DateTime.Now.AddHours(1).AddDays(2), EndOfPickupTime = DateTime.Now.AddHours(2).AddDays(2), IsEighteenPlus = false, Price = 6.90m, MealType = MealType.Snack, CanteenId = 3, CityEnum = CityEnum.Den_Bosch, CanteenLocationEnum = CanteenLocationEnum.Beukenlaan }};

            IEnumerable<Product> products = new List<Product> {
            new Product { Id = 1, Name = "Banaan", ContainsAlcohol = false, ImageUrl= "https://cdn1.sph.harvard.edu/wp-content/uploads/sites/30/2018/08/bananas-1354785_1920-1024x683.jpg" },
            new Product { Id = 2, Name = "Brood", ContainsAlcohol = false, ImageUrl= "https://parade.com/.image/ar_1:1%2Cc_fill%2Ccs_srgb%2Cfl_progressive%2Cq_auto:good%2Cw_1200/MTkwNTgxNDM5MDYzMjA0OTg5/types-of-bread-jpg.jpg" },
            new Product { Id = 3, Name = "Panini", ContainsAlcohol = false, ImageUrl= "https://delitraiteur.lu/wp-content/uploads/2022/02/panini-1.png"},
            new Product { Id = 4, Name = "Shake", ContainsAlcohol = false, ImageUrl= "https://bakeitwithlove.com/wp-content/uploads/2022/03/Milk-Shakes-vs-Malts.jpg.webp"},
            new Product { Id = 5, Name = "Coffee", ContainsAlcohol = false, ImageUrl= "https://www.tastingtable.com/img/gallery/coffee-brands-ranked-from-worst-to-best/intro-1645231221.webp" },
            new Product { Id = 6, Name = "Snoep", ContainsAlcohol = false, ImageUrl= "https://upload.wikimedia.org/wikipedia/commons/thumb/1/10/Candy_in_Damascus.jpg/375px-Candy_in_Damascus.jpg" },
            new Product { Id = 7, Name = "Bier", ContainsAlcohol = true, ImageUrl= "https://media-cdn.tripadvisor.com/media/photo-s/1b/78/81/ac/jabeerwocky-craft-beer.jpg" },
            new Product { Id = 8, Name = "Kroket", ContainsAlcohol = false, ImageUrl= "https://mvlstreekvlees.nl/wp-content/uploads/2021/09/MVL2021-086-bewerkt.jpg" },
            new Product { Id = 9, Name = "Koekjes", ContainsAlcohol = false, ImageUrl = "https://favorflav.com/images/Smartiekoekjes-Favorflav-916x458.jpg" }};

            IEnumerable<Student> students = new List<Student> {
            new Student { Id = 1, Name = "firststudent@hotmail.com", Birthdate = new DateTime(2000, 7, 22, 18, 36, 13, 25), StudentNumber = "2142135", Emailadres = "firststudent@hotmail.com", StudyCity = CityEnum.Breda, Phonenumber = "0612345611"},
            new Student { Id = 2, Name = "secondstudent@hotmail.com",  Birthdate = new DateTime(2005, 10, 28, 18, 11, 12, 15), StudentNumber = "2122346", Emailadres = "secondstudent@hotmail.com" , StudyCity = CityEnum.Den_Bosch, Phonenumber = "0612345622"},
            new Student { Id = 3, Name = "thirdstudent@hotmail.com", Birthdate = new DateTime(1992, 11, 9, 18, 25, 16, 55),  StudentNumber = "2012347", Emailadres = "thirdstudent@hotmail.com" , StudyCity = CityEnum.Breda, Phonenumber = "0612345633" },
            new Student { Id = 4, Name = "Albert Macgenzie", Birthdate = new DateTime(2003, 10, 24, 18, 24, 7, 61),  StudentNumber = "2011302", Emailadres = "albert@hotmail.com" , StudyCity = CityEnum.Tilburg, Phonenumber = "0612194735" }};

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CanteenEmployee>().HasData(canteenEmployees);

            modelBuilder.Entity<Canteen>().HasData(canteens);

            modelBuilder.Entity<Package>().HasData(packages);

            modelBuilder.Entity<Product>().HasData(products);

            modelBuilder.Entity<Student>().HasData(students);

            modelBuilder.Entity<Package>()
                    .HasMany(pr => pr.Products)
                    .WithMany(pa => pa.Packages)
                    .UsingEntity<Dictionary<string, object>>(
                    "PackageProduct",
                    pro => pro.HasOne<Product>().WithMany().HasForeignKey("ProductsId"),
                    pac => pac.HasOne<Package>().WithMany().HasForeignKey("PackagesId"),
                    pp => {
                        pp.HasKey("ProductsId", "PackagesId");
                        pp.HasData(
                            new { ProductsId = 1, PackagesId = 1 },
                            new { ProductsId = 2, PackagesId = 1 },
                            new { ProductsId = 3, PackagesId = 1 },
                            new { ProductsId = 5, PackagesId = 1 },
                            new { ProductsId = 1, PackagesId = 2 },
                            new { ProductsId = 2, PackagesId = 2 },
                            new { ProductsId = 3, PackagesId = 2 },
                            new { ProductsId = 5, PackagesId = 2 },
                            new { ProductsId = 1, PackagesId = 3 },
                            new { ProductsId = 2, PackagesId = 3 },
                            new { ProductsId = 3, PackagesId = 3 },
                            new { ProductsId = 1, PackagesId = 4 },
                            new { ProductsId = 2, PackagesId = 4 },
                            new { ProductsId = 5, PackagesId = 5 },
                            new { ProductsId = 9, PackagesId = 5 },
                            new { ProductsId = 1, PackagesId = 6 },
                            new { ProductsId = 1, PackagesId = 7 },
                            new { ProductsId = 1, PackagesId = 8 },
                            new { ProductsId = 7, PackagesId = 6 },
                            new { ProductsId = 6, PackagesId = 7 },
                            new { ProductsId = 9, PackagesId = 8 });
                    });
        }
    }
}