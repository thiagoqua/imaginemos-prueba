using backend.Models;
using backend.Repositories;
using backend.Repositories.Interfaces;
using backend.Services;
using backend.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ImaginemosDBContext>();

//instancias de inyección de dependencias
//servicios
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ISellService, SellService>();
builder.Services.AddScoped<ISellDetailService, SellDetailService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton<DTOMapperService>();
//repositorios
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ISellRepository, SellRepository>();
builder.Services.AddScoped<ISellDetailRepository, SellDetailRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
