using Microsoft.EntityFrameworkCore;
using ControldePrestamos.Infrastructure.Contexto;
using ControldePrestamos.Infrastructure.Repositorios;
using ControldePrestamos.Domain.Repositorio;
using ControldePrestamos.Application.Contract;
using ControldePrestamos.Application.Services;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PrestamosContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ConexionSQL")));

// Registrar repositorios
builder.Services.AddScoped<IHerramientaRepositorio, HerramientaRepositorio>();
builder.Services.AddScoped<IEstudianteRepositorio, EstudianteRepositorio>();
builder.Services.AddScoped<IPrestamoRepositorio, PrestamoRepositorio>();

builder.Services.AddScoped<IHerramientaService, HerramientaService>();
builder.Services.AddScoped<IEstudianteService, EstudianteService>();
builder.Services.AddScoped<IPrestamoService, PrestamoService>();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();