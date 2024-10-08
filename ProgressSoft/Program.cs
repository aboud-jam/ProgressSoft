using Core.IServices;
using Core.IServicesl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Presistence.context;
using Presistence.IRepository;
using Presistence.Repository;
using Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddScoped<IBusinessCardServices, BusinessCardServices>();
builder.Services.AddScoped<IExportService, ExportService>();
builder.Services.AddScoped(typeof(IRepository<,>), typeof(BaseRepository<,>));
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
