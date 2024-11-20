﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.TGTGEF.Migrations
{
    [DbContext(typeof(PackageDbContext))]
    [Migration("20230120195544_NewDates")]
    partial class NewDates
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Core.Domain.Canteen", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CanteenEmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("CanteenLocationEnum")
                        .HasColumnType("int");

                    b.Property<int>("CityEnum")
                        .HasColumnType("int");

                    b.Property<bool>("OffersHotMeals")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CanteenEmployeeId");

                    b.ToTable("Canteens");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CanteenEmployeeId = 1,
                            CanteenLocationEnum = 0,
                            CityEnum = 0,
                            OffersHotMeals = true
                        },
                        new
                        {
                            Id = 2,
                            CanteenEmployeeId = 2,
                            CanteenLocationEnum = 1,
                            CityEnum = 2,
                            OffersHotMeals = true
                        },
                        new
                        {
                            Id = 3,
                            CanteenEmployeeId = 3,
                            CanteenLocationEnum = 2,
                            CityEnum = 1,
                            OffersHotMeals = false
                        });
                });

            modelBuilder.Entity("Core.Domain.CanteenEmployee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CanteenLocationEnum")
                        .HasColumnType("int");

                    b.Property<string>("EmployeeNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CanteenEmployees");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CanteenLocationEnum = 0,
                            EmployeeNumber = "123456",
                            Name = "firstemployee@hotmail.com"
                        },
                        new
                        {
                            Id = 2,
                            CanteenLocationEnum = 1,
                            EmployeeNumber = "123457",
                            Name = "secondemployee@hotmail.com"
                        },
                        new
                        {
                            Id = 3,
                            CanteenLocationEnum = 2,
                            EmployeeNumber = "456791",
                            Name = "Amanda Brook"
                        });
                });

            modelBuilder.Entity("Core.Domain.Package", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CanteenId")
                        .HasColumnType("int");

                    b.Property<int>("CanteenLocationEnum")
                        .HasColumnType("int");

                    b.Property<int>("CityEnum")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndOfPickupTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsEighteenPlus")
                        .HasColumnType("bit");

                    b.Property<int>("MealType")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("PickupDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("ReservedById")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CanteenId");

                    b.HasIndex("ReservedById");

                    b.ToTable("Packages");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CanteenId = 1,
                            CanteenLocationEnum = 0,
                            CityEnum = 0,
                            EndOfPickupTime = new DateTime(2023, 1, 20, 22, 55, 44, 94, DateTimeKind.Local).AddTicks(7522),
                            IsEighteenPlus = false,
                            MealType = 2,
                            Name = "Speciaal soep",
                            PickupDate = new DateTime(2023, 1, 20, 21, 55, 44, 94, DateTimeKind.Local).AddTicks(7479),
                            Price = 1.50m,
                            ReservedById = 1
                        },
                        new
                        {
                            Id = 2,
                            CanteenId = 1,
                            CanteenLocationEnum = 0,
                            CityEnum = 0,
                            EndOfPickupTime = new DateTime(2023, 1, 20, 22, 55, 44, 94, DateTimeKind.Local).AddTicks(7538),
                            IsEighteenPlus = false,
                            MealType = 3,
                            Name = "Fruitmix",
                            PickupDate = new DateTime(2023, 1, 20, 21, 55, 44, 94, DateTimeKind.Local).AddTicks(7533),
                            Price = 1.20m,
                            ReservedById = 2
                        },
                        new
                        {
                            Id = 3,
                            CanteenId = 1,
                            CanteenLocationEnum = 0,
                            CityEnum = 0,
                            EndOfPickupTime = new DateTime(2023, 1, 21, 22, 55, 44, 94, DateTimeKind.Local).AddTicks(7545),
                            IsEighteenPlus = false,
                            MealType = 0,
                            Name = "Panini mix",
                            PickupDate = new DateTime(2023, 1, 21, 21, 55, 44, 94, DateTimeKind.Local).AddTicks(7543),
                            Price = 5.20m
                        },
                        new
                        {
                            Id = 4,
                            CanteenId = 2,
                            CanteenLocationEnum = 1,
                            CityEnum = 2,
                            EndOfPickupTime = new DateTime(2023, 1, 21, 22, 55, 44, 94, DateTimeKind.Local).AddTicks(7671),
                            IsEighteenPlus = false,
                            MealType = 4,
                            Name = "Ijs mix",
                            PickupDate = new DateTime(2023, 1, 21, 21, 55, 44, 94, DateTimeKind.Local).AddTicks(7667),
                            Price = 1.50m
                        },
                        new
                        {
                            Id = 5,
                            CanteenId = 2,
                            CanteenLocationEnum = 1,
                            CityEnum = 2,
                            EndOfPickupTime = new DateTime(2023, 1, 21, 22, 55, 44, 94, DateTimeKind.Local).AddTicks(7679),
                            IsEighteenPlus = false,
                            MealType = 2,
                            Name = "Coffee mix",
                            PickupDate = new DateTime(2023, 1, 21, 21, 55, 44, 94, DateTimeKind.Local).AddTicks(7676),
                            Price = 3.20m
                        },
                        new
                        {
                            Id = 6,
                            CanteenId = 3,
                            CanteenLocationEnum = 2,
                            CityEnum = 1,
                            EndOfPickupTime = new DateTime(2023, 1, 22, 22, 55, 44, 94, DateTimeKind.Local).AddTicks(7689),
                            IsEighteenPlus = true,
                            MealType = 2,
                            Name = "Drank mix",
                            PickupDate = new DateTime(2023, 1, 22, 21, 55, 44, 94, DateTimeKind.Local).AddTicks(7687),
                            Price = 3.70m
                        },
                        new
                        {
                            Id = 7,
                            CanteenId = 3,
                            CanteenLocationEnum = 2,
                            CityEnum = 1,
                            EndOfPickupTime = new DateTime(2023, 1, 22, 22, 55, 44, 94, DateTimeKind.Local).AddTicks(7696),
                            IsEighteenPlus = false,
                            MealType = 3,
                            Name = "Snack mix",
                            PickupDate = new DateTime(2023, 1, 22, 21, 55, 44, 94, DateTimeKind.Local).AddTicks(7693),
                            Price = 4.20m
                        },
                        new
                        {
                            Id = 8,
                            CanteenId = 3,
                            CanteenLocationEnum = 2,
                            CityEnum = 1,
                            EndOfPickupTime = new DateTime(2023, 1, 22, 22, 55, 44, 94, DateTimeKind.Local).AddTicks(7702),
                            IsEighteenPlus = false,
                            MealType = 3,
                            Name = "Koekjes mix",
                            PickupDate = new DateTime(2023, 1, 22, 21, 55, 44, 94, DateTimeKind.Local).AddTicks(7700),
                            Price = 6.90m
                        });
                });

            modelBuilder.Entity("Core.Domain.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("ContainsAlcohol")
                        .HasColumnType("bit");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ContainsAlcohol = false,
                            ImageUrl = "https://cdn1.sph.harvard.edu/wp-content/uploads/sites/30/2018/08/bananas-1354785_1920-1024x683.jpg",
                            Name = "Banaan"
                        },
                        new
                        {
                            Id = 2,
                            ContainsAlcohol = false,
                            ImageUrl = "https://parade.com/.image/ar_1:1%2Cc_fill%2Ccs_srgb%2Cfl_progressive%2Cq_auto:good%2Cw_1200/MTkwNTgxNDM5MDYzMjA0OTg5/types-of-bread-jpg.jpg",
                            Name = "Brood"
                        },
                        new
                        {
                            Id = 3,
                            ContainsAlcohol = false,
                            ImageUrl = "https://delitraiteur.lu/wp-content/uploads/2022/02/panini-1.png",
                            Name = "Panini"
                        },
                        new
                        {
                            Id = 4,
                            ContainsAlcohol = false,
                            ImageUrl = "https://bakeitwithlove.com/wp-content/uploads/2022/03/Milk-Shakes-vs-Malts.jpg.webp",
                            Name = "Shake"
                        },
                        new
                        {
                            Id = 5,
                            ContainsAlcohol = false,
                            ImageUrl = "https://www.tastingtable.com/img/gallery/coffee-brands-ranked-from-worst-to-best/intro-1645231221.webp",
                            Name = "Coffee"
                        },
                        new
                        {
                            Id = 6,
                            ContainsAlcohol = false,
                            ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/10/Candy_in_Damascus.jpg/375px-Candy_in_Damascus.jpg",
                            Name = "Snoep"
                        },
                        new
                        {
                            Id = 7,
                            ContainsAlcohol = true,
                            ImageUrl = "https://media-cdn.tripadvisor.com/media/photo-s/1b/78/81/ac/jabeerwocky-craft-beer.jpg",
                            Name = "Bier"
                        },
                        new
                        {
                            Id = 8,
                            ContainsAlcohol = false,
                            ImageUrl = "https://mvlstreekvlees.nl/wp-content/uploads/2021/09/MVL2021-086-bewerkt.jpg",
                            Name = "Kroket"
                        },
                        new
                        {
                            Id = 9,
                            ContainsAlcohol = false,
                            ImageUrl = "https://favorflav.com/images/Smartiekoekjes-Favorflav-916x458.jpg",
                            Name = "Koekjes"
                        });
                });

            modelBuilder.Entity("Core.Domain.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Emailadres")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phonenumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudyCity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Students");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Birthdate = new DateTime(2000, 7, 22, 18, 36, 13, 25, DateTimeKind.Unspecified),
                            Emailadres = "firststudent@hotmail.com",
                            Name = "firststudent@hotmail.com",
                            Phonenumber = "0612345611",
                            StudentNumber = "2142135",
                            StudyCity = 0
                        },
                        new
                        {
                            Id = 2,
                            Birthdate = new DateTime(2005, 10, 28, 18, 11, 12, 15, DateTimeKind.Unspecified),
                            Emailadres = "secondstudent@hotmail.com",
                            Name = "secondstudent@hotmail.com",
                            Phonenumber = "0612345622",
                            StudentNumber = "2122346",
                            StudyCity = 1
                        },
                        new
                        {
                            Id = 3,
                            Birthdate = new DateTime(1992, 11, 9, 18, 25, 16, 55, DateTimeKind.Unspecified),
                            Emailadres = "thirdstudent@hotmail.com",
                            Name = "thirdstudent@hotmail.com",
                            Phonenumber = "0612345633",
                            StudentNumber = "2012347",
                            StudyCity = 0
                        },
                        new
                        {
                            Id = 4,
                            Birthdate = new DateTime(2003, 10, 24, 18, 24, 7, 61, DateTimeKind.Unspecified),
                            Emailadres = "albert@hotmail.com",
                            Name = "Albert Macgenzie",
                            Phonenumber = "0612194735",
                            StudentNumber = "2011302",
                            StudyCity = 2
                        });
                });

            modelBuilder.Entity("PackageProduct", b =>
                {
                    b.Property<int>("ProductsId")
                        .HasColumnType("int");

                    b.Property<int>("PackagesId")
                        .HasColumnType("int");

                    b.HasKey("ProductsId", "PackagesId");

                    b.HasIndex("PackagesId");

                    b.ToTable("PackageProduct");

                    b.HasData(
                        new
                        {
                            ProductsId = 1,
                            PackagesId = 1
                        },
                        new
                        {
                            ProductsId = 2,
                            PackagesId = 1
                        },
                        new
                        {
                            ProductsId = 3,
                            PackagesId = 1
                        },
                        new
                        {
                            ProductsId = 5,
                            PackagesId = 1
                        },
                        new
                        {
                            ProductsId = 1,
                            PackagesId = 2
                        },
                        new
                        {
                            ProductsId = 2,
                            PackagesId = 2
                        },
                        new
                        {
                            ProductsId = 3,
                            PackagesId = 2
                        },
                        new
                        {
                            ProductsId = 5,
                            PackagesId = 2
                        },
                        new
                        {
                            ProductsId = 1,
                            PackagesId = 3
                        },
                        new
                        {
                            ProductsId = 2,
                            PackagesId = 3
                        },
                        new
                        {
                            ProductsId = 3,
                            PackagesId = 3
                        },
                        new
                        {
                            ProductsId = 1,
                            PackagesId = 4
                        },
                        new
                        {
                            ProductsId = 2,
                            PackagesId = 4
                        },
                        new
                        {
                            ProductsId = 5,
                            PackagesId = 5
                        },
                        new
                        {
                            ProductsId = 9,
                            PackagesId = 5
                        },
                        new
                        {
                            ProductsId = 1,
                            PackagesId = 6
                        },
                        new
                        {
                            ProductsId = 1,
                            PackagesId = 7
                        },
                        new
                        {
                            ProductsId = 1,
                            PackagesId = 8
                        },
                        new
                        {
                            ProductsId = 7,
                            PackagesId = 6
                        },
                        new
                        {
                            ProductsId = 6,
                            PackagesId = 7
                        },
                        new
                        {
                            ProductsId = 9,
                            PackagesId = 8
                        });
                });

            modelBuilder.Entity("Core.Domain.Canteen", b =>
                {
                    b.HasOne("Core.Domain.CanteenEmployee", "CanteenEmployee")
                        .WithMany()
                        .HasForeignKey("CanteenEmployeeId");

                    b.Navigation("CanteenEmployee");
                });

            modelBuilder.Entity("Core.Domain.Package", b =>
                {
                    b.HasOne("Core.Domain.Canteen", "Canteen")
                        .WithMany()
                        .HasForeignKey("CanteenId");

                    b.HasOne("Core.Domain.Student", "ReservedBy")
                        .WithMany()
                        .HasForeignKey("ReservedById");

                    b.Navigation("Canteen");

                    b.Navigation("ReservedBy");
                });

            modelBuilder.Entity("PackageProduct", b =>
                {
                    b.HasOne("Core.Domain.Package", null)
                        .WithMany()
                        .HasForeignKey("PackagesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Domain.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}