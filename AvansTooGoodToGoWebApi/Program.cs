using Core.DomainServices.Repos.Intf;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<PackageDbContext>(options => options.UseSqlServer(connectionString));

var userConnectionString = builder.Configuration.GetConnectionString("Security");
builder.Services.AddDbContext<SecurityDbContext>(options => options.UseSqlServer(userConnectionString));

builder.Services.AddScoped<IPackageRepository, PackageEFRepository>();
builder.Services.AddScoped<IStudentRepository, StudentEFRepository>();

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler =
        ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
