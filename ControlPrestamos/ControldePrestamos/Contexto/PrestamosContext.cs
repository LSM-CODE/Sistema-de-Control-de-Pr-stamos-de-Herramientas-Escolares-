

using Microsoft.EntityFrameworkCore;
using ControldePrestamos.Models;

namespace ControldePrestamos.Contexto
{
    public class PrestamosContext : DbContext
    {
        public PrestamosContext(DbContextOptions<PrestamosContext> options)
            : base(options)
        {
        }

        // Tablas de la base de datos.
        public DbSet<Herramienta> Herramientas { get; set; }

        public DbSet<Estudiante> Estudiantes { get; set; }

        public DbSet<Prestamo> Prestamos { get; set; }
    }
}
