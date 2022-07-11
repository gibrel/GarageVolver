using AutoMapper;
using GarageVolver.API.Configurations;
using GarageVolver.Data.Configurations;
using GarageVolver.Data.Context;
using GarageVolver.Data.Repositories;
using GarageVolver.Domain.Entities;
using GarageVolver.Domain.Interfaces;
using GarageVolver.Service.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigureServices(builder.Services);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<SQLiteContext>())
    context?.Database.Migrate();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigureServices(IServiceCollection services)
{
    services.Configure<SQLiteConfiguration>(
        builder.Configuration.GetSection("database:sqlite"));

    services.AddSingleton(new MapperConfiguration(config =>
    {
        config.AddProfile<TruckMapProfile>();
    }).CreateMapper());

    services.AddDbContext<SQLiteContext>();

    builder.Services.AddScoped<IBaseRepository<Truck>, BaseRepository<Truck>>();
    builder.Services.AddScoped<IBaseService<Truck>, BaseService<Truck>>();
    builder.Services.AddScoped<ITruckRepository, TruckRepository>();
    builder.Services.AddScoped<ITruckService, TruckService>();

    services.AddMvc(option => option.EnableEndpointRouting = false)
        .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
}