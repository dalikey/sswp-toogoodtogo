using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.TGTGEF.Migrations
{
    public partial class NewDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CanteenEmployees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CanteenLocationEnum = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CanteenEmployees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContainsAlcohol = table.Column<bool>(type: "bit", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudentNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Emailadres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StudyCity = table.Column<int>(type: "int", nullable: false),
                    Phonenumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Canteens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityEnum = table.Column<int>(type: "int", nullable: false),
                    CanteenLocationEnum = table.Column<int>(type: "int", nullable: false),
                    OffersHotMeals = table.Column<bool>(type: "bit", nullable: false),
                    CanteenEmployeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Canteens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Canteens_CanteenEmployees_CanteenEmployeeId",
                        column: x => x.CanteenEmployeeId,
                        principalTable: "CanteenEmployees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PickupDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndOfPickupTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsEighteenPlus = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MealType = table.Column<int>(type: "int", nullable: false),
                    ReservedById = table.Column<int>(type: "int", nullable: true),
                    CanteenId = table.Column<int>(type: "int", nullable: true),
                    CityEnum = table.Column<int>(type: "int", nullable: false),
                    CanteenLocationEnum = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Packages_Canteens_CanteenId",
                        column: x => x.CanteenId,
                        principalTable: "Canteens",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Packages_Students_ReservedById",
                        column: x => x.ReservedById,
                        principalTable: "Students",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PackageProduct",
                columns: table => new
                {
                    ProductsId = table.Column<int>(type: "int", nullable: false),
                    PackagesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackageProduct", x => new { x.ProductsId, x.PackagesId });
                    table.ForeignKey(
                        name: "FK_PackageProduct_Packages_PackagesId",
                        column: x => x.PackagesId,
                        principalTable: "Packages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PackageProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CanteenEmployees",
                columns: new[] { "Id", "CanteenLocationEnum", "EmployeeNumber", "Name" },
                values: new object[,]
                {
                    { 1, 0, "123456", "firstemployee@hotmail.com" },
                    { 2, 1, "123457", "secondemployee@hotmail.com" },
                    { 3, 2, "456791", "Amanda Brook" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "ContainsAlcohol", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 1, false, "https://cdn1.sph.harvard.edu/wp-content/uploads/sites/30/2018/08/bananas-1354785_1920-1024x683.jpg", "Banaan" },
                    { 2, false, "https://parade.com/.image/ar_1:1%2Cc_fill%2Ccs_srgb%2Cfl_progressive%2Cq_auto:good%2Cw_1200/MTkwNTgxNDM5MDYzMjA0OTg5/types-of-bread-jpg.jpg", "Brood" },
                    { 3, false, "https://delitraiteur.lu/wp-content/uploads/2022/02/panini-1.png", "Panini" },
                    { 4, false, "https://bakeitwithlove.com/wp-content/uploads/2022/03/Milk-Shakes-vs-Malts.jpg.webp", "Shake" },
                    { 5, false, "https://www.tastingtable.com/img/gallery/coffee-brands-ranked-from-worst-to-best/intro-1645231221.webp", "Coffee" },
                    { 6, false, "https://upload.wikimedia.org/wikipedia/commons/thumb/1/10/Candy_in_Damascus.jpg/375px-Candy_in_Damascus.jpg", "Snoep" },
                    { 7, true, "https://media-cdn.tripadvisor.com/media/photo-s/1b/78/81/ac/jabeerwocky-craft-beer.jpg", "Bier" },
                    { 8, false, "https://mvlstreekvlees.nl/wp-content/uploads/2021/09/MVL2021-086-bewerkt.jpg", "Kroket" },
                    { 9, false, "https://favorflav.com/images/Smartiekoekjes-Favorflav-916x458.jpg", "Koekjes" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Birthdate", "Emailadres", "Name", "Phonenumber", "StudentNumber", "StudyCity" },
                values: new object[,]
                {
                    { 1, new DateTime(2000, 7, 22, 18, 36, 13, 25, DateTimeKind.Unspecified), "firststudent@hotmail.com", "firststudent@hotmail.com", "0612345611", "2142135", 0 },
                    { 2, new DateTime(2005, 10, 28, 18, 11, 12, 15, DateTimeKind.Unspecified), "secondstudent@hotmail.com", "secondstudent@hotmail.com", "0612345622", "2122346", 1 },
                    { 3, new DateTime(1992, 11, 9, 18, 25, 16, 55, DateTimeKind.Unspecified), "thirdstudent@hotmail.com", "thirdstudent@hotmail.com", "0612345633", "2012347", 0 },
                    { 4, new DateTime(2003, 10, 24, 18, 24, 7, 61, DateTimeKind.Unspecified), "albert@hotmail.com", "Albert Macgenzie", "0612194735", "2011302", 2 }
                });

            migrationBuilder.InsertData(
                table: "Canteens",
                columns: new[] { "Id", "CanteenEmployeeId", "CanteenLocationEnum", "CityEnum", "OffersHotMeals" },
                values: new object[] { 1, 1, 0, 0, true });

            migrationBuilder.InsertData(
                table: "Canteens",
                columns: new[] { "Id", "CanteenEmployeeId", "CanteenLocationEnum", "CityEnum", "OffersHotMeals" },
                values: new object[] { 2, 2, 1, 2, true });

            migrationBuilder.InsertData(
                table: "Canteens",
                columns: new[] { "Id", "CanteenEmployeeId", "CanteenLocationEnum", "CityEnum", "OffersHotMeals" },
                values: new object[] { 3, 3, 2, 1, false });

            migrationBuilder.InsertData(
                table: "Packages",
                columns: new[] { "Id", "CanteenId", "CanteenLocationEnum", "CityEnum", "EndOfPickupTime", "IsEighteenPlus", "MealType", "Name", "PickupDate", "Price", "ReservedById" },
                values: new object[,]
                {
                    { 1, 1, 0, 0, new DateTime(2023, 1, 20, 22, 55, 44, 94, DateTimeKind.Local).AddTicks(7522), false, 2, "Speciaal soep", new DateTime(2023, 1, 20, 21, 55, 44, 94, DateTimeKind.Local).AddTicks(7479), 1.50m, 1 },
                    { 2, 1, 0, 0, new DateTime(2023, 1, 20, 22, 55, 44, 94, DateTimeKind.Local).AddTicks(7538), false, 3, "Fruitmix", new DateTime(2023, 1, 20, 21, 55, 44, 94, DateTimeKind.Local).AddTicks(7533), 1.20m, 2 },
                    { 3, 1, 0, 0, new DateTime(2023, 1, 21, 22, 55, 44, 94, DateTimeKind.Local).AddTicks(7545), false, 0, "Panini mix", new DateTime(2023, 1, 21, 21, 55, 44, 94, DateTimeKind.Local).AddTicks(7543), 5.20m, null },
                    { 4, 2, 1, 2, new DateTime(2023, 1, 21, 22, 55, 44, 94, DateTimeKind.Local).AddTicks(7671), false, 4, "Ijs mix", new DateTime(2023, 1, 21, 21, 55, 44, 94, DateTimeKind.Local).AddTicks(7667), 1.50m, null },
                    { 5, 2, 1, 2, new DateTime(2023, 1, 21, 22, 55, 44, 94, DateTimeKind.Local).AddTicks(7679), false, 2, "Coffee mix", new DateTime(2023, 1, 21, 21, 55, 44, 94, DateTimeKind.Local).AddTicks(7676), 3.20m, null },
                    { 6, 3, 2, 1, new DateTime(2023, 1, 22, 22, 55, 44, 94, DateTimeKind.Local).AddTicks(7689), true, 2, "Drank mix", new DateTime(2023, 1, 22, 21, 55, 44, 94, DateTimeKind.Local).AddTicks(7687), 3.70m, null },
                    { 7, 3, 2, 1, new DateTime(2023, 1, 22, 22, 55, 44, 94, DateTimeKind.Local).AddTicks(7696), false, 3, "Snack mix", new DateTime(2023, 1, 22, 21, 55, 44, 94, DateTimeKind.Local).AddTicks(7693), 4.20m, null },
                    { 8, 3, 2, 1, new DateTime(2023, 1, 22, 22, 55, 44, 94, DateTimeKind.Local).AddTicks(7702), false, 3, "Koekjes mix", new DateTime(2023, 1, 22, 21, 55, 44, 94, DateTimeKind.Local).AddTicks(7700), 6.90m, null }
                });

            migrationBuilder.InsertData(
                table: "PackageProduct",
                columns: new[] { "PackagesId", "ProductsId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 1 },
                    { 6, 1 },
                    { 7, 1 },
                    { 8, 1 },
                    { 1, 2 },
                    { 2, 2 },
                    { 3, 2 },
                    { 4, 2 },
                    { 1, 3 },
                    { 2, 3 },
                    { 3, 3 },
                    { 1, 5 },
                    { 2, 5 },
                    { 5, 5 },
                    { 7, 6 },
                    { 6, 7 },
                    { 5, 9 },
                    { 8, 9 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Canteens_CanteenEmployeeId",
                table: "Canteens",
                column: "CanteenEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PackageProduct_PackagesId",
                table: "PackageProduct",
                column: "PackagesId");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_CanteenId",
                table: "Packages",
                column: "CanteenId");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_ReservedById",
                table: "Packages",
                column: "ReservedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PackageProduct");

            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Canteens");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "CanteenEmployees");
        }
    }
}
