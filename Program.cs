using Microsoft.EntityFrameworkCore;
using Desafio.Data;
using System.Text.Json.Serialization;
using Desafio.Service;


var builder = WebApplication.CreateBuilder(args);

// Agrega los servicios
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.PropertyNamingPolicy = null; // Mantiene nombres originales
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSQL")
        ?? throw new InvalidOperationException("Connection string 'CadenaSQL' not found."));
});

builder.Services.AddScoped<IClienteGuardarService, ClienteGuardarService>();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
