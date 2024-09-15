using Microsoft.EntityFrameworkCore;
using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Repositories;
using ToyManagementProject.Domain.Interfaces.Services;
using ToyManagementProject.Infra.Data.Context;
using ToyManagementProject.Infra.Data.RepoEF;
using ToyManagementProject.Infra.Data.UoW;
using ToyManagementProject.Services;
using ToyManagementProject.Services.Mappings;
using ToyManagementProject.Services.Validators;
using ToyManagementProject.Services.Validators.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowSpecificOrigin",
		policy =>
		{
			policy.WithOrigins("http://localhost:4200") // Permitir solicitações apenas de http://localhost:4200
				  .AllowAnyHeader()
				  .AllowAnyMethod();
		});
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ToyDbContext>(options =>
	options.UseInMemoryDatabase("ToyCatalogDb"));

builder.Services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
// repositories
builder.Services.AddScoped<IToyRepository,ToyRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IStockRepository, StockRepository>();
//Application
builder.Services.AddScoped<IToyService, ToyService>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<IOrderProcessingService, OrderProcessingService>();

//validators
builder.Services.AddScoped(typeof(IValidator<Order>), typeof(OrderValidator));
builder.Services.AddScoped(typeof(IValidator<Toy>), typeof(ToyValidator));

//AutoMapper
builder.Services.AddAutoMapper(typeof(ToyProfile));
builder.Services.AddAutoMapper(typeof(StockProfile));
builder.Services.AddAutoMapper(typeof(OrderProfile));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
