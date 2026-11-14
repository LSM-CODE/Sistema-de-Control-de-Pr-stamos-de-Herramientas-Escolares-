using Microsoft.EntityFrameworkCore;
using ControldePrestamos.Infrastructure.Contexto;
using ControldePrestamos.Infrastructure.Repositorios;
using ControldePrestamos.Domain.Repositorio;

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

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();