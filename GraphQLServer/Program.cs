using Core.DomainServices.Repos.Intf;
using Core.DomainServices.Services.Impl;
using Core.DomainServices.Services.Intf;
using GraphQLServer.GraphQL;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<PackageDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IPackageRepository, PackageEFRepository>();
builder.Services.AddScoped<IProductRepository, ProductEFRepository>();
builder.Services.AddScoped<ICanteenEmployeeRepository, CanteenEmployeeEFRepository>();
builder.Services.AddScoped<IStudentRepository, StudentEFRepository>();
builder.Services.AddScoped<IPackageService, PackageServiceBasic>();
builder.Services.AddScoped<Query>();
builder.Services.AddScoped<GraphQLTypes>();
builder.Services.AddScoped<Mutation>();

builder.Services.AddGraphQLServer().AddType<GraphQLTypes>().AddQueryType<Query>();

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();
app.MapGraphQL();

app.Run();