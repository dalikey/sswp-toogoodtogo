using Core.DomainServices.Repos.Intf;
using Core.DomainServices.Services.Impl;
using Core.DomainServices.Services.Intf;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<PackageDbContext>(options => options.UseSqlServer(connectionString));

var userConnectionString = builder.Configuration.GetConnectionString("Security");
builder.Services.AddDbContext<SecurityDbContext>(options => options.UseSqlServer(userConnectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<SecurityDbContext>();

builder.Services.AddAuthorization(options =>
    options.AddPolicy("CanteenEmployeeOnly", policy => policy.RequireClaim("CanteenEmployee")));

builder.Services.AddAuthorization(options =>
    options.AddPolicy("StudentOnly", policy => policy.RequireClaim("Student")));

builder.Services.AddScoped<ICanteenEmployeeRepository, CanteenEmployeeEFRepository>();
builder.Services.AddScoped<IPackageRepository, PackageEFRepository>();
builder.Services.AddScoped<ICanteenRepository, CanteenEFRepository>();
builder.Services.AddScoped<IStudentRepository, StudentEFRepository>();
builder.Services.AddScoped<IProductRepository, ProductEFRepository>();
builder.Services.AddScoped<IPackageService, PackageServiceBasic>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseMigrationsEndPoint();
} else {
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "package",
    pattern: "{controller=Package}/{action=List}/{canteen?}");

app.Run();