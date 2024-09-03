

using CRM.CORE;
using CRM.CORE.Repositories.Interfaces;
using CRM.CORE.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllHeaders",
          builder =>
          {
              builder.AllowAnyOrigin()
                     .AllowAnyHeader()
                     .AllowAnyMethod();
          });
});
builder.Services.AddTransient(typeof(DbContext), typeof(CRMDBContext));
builder.Services.AddTransient(typeof(ICustomerRepository), typeof(CustomerRepository));


// Register your DbContext for dependency injection
//builder.Services.AddDbContext<CRMDBContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("CRMConnectionString")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAllHeaders");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
