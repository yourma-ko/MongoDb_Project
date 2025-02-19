using BLL.Interfaces;
using BLL.Services;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// ���������� ������������
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ��������� ������ ����������� � MongoDB
var mongoConnectionString = Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING")
    ?? builder.Configuration.GetConnectionString("Mongo");

var mongoClient = new MongoClient(mongoConnectionString);
var dbName = "LaptopStore";
var mongoDb = mongoClient.GetDatabase(dbName);

// ����������� MongoDB � DI-����������
builder.Services.AddSingleton<IMongoDatabase>(mongoDb);

// ����������� ������������
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
// ����������� ��������
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<ICartService, CartService>();

var app = builder.Build();

// ��������� middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
