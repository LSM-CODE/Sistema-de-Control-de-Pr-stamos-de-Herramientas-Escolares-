

using Microsoft.AspNetCore.Mvc;
using ControldePrestamos.Models;
using ControldePrestamos.Contexto;
using ControldePrestamos.Dtos;

namespace ControldePrestamos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrestamosController : ControllerBase
    {
        // Conexión con la base de datos.
        private readonly PrestamosContext _context;

        public PrestamosController(PrestamosContext context)
        {
            _context = context;
        }

        // Consulta todos los préstamos.
        [HttpGet]
        public IActionResult ObtenerPrestamos()
        {
            return Ok(_context.Prestamos.ToList());
        }

        // Registra un préstamo si el estudiante y la herramienta existen.
        [HttpPost]
        public IActionResult CrearPrestamo(PrestamoDto prestamoDto)
        {
            var estudiante = _context.Estudiantes.Find(prestamoDto.EstudianteId);

            if (estudiante == null)
            {
                return NotFound("Estudiante no encontrado");
            }

            var herramienta = _context.Herramientas.Find(prestamoDto.HerramientaId);

            if (herramienta == null)
            {
                return NotFound("Herramienta no encontrada");
            }

            // Impide prestar herramientas no disponibles o dañadas.
            if (!herramienta.Disponible || herramienta.Danada)
            {
                return BadRequest("La herramienta no está disponible para préstamo");
            }

            var prestamo = new Prestamo
            {
                EstudianteId = prestamoDto.EstudianteId,
                HerramientaId = prestamoDto.HerramientaId,
                FechaPrestamo = DateTime.Now,
                FechaDevolucion = null
            };

            herramienta.Disponible = false;

            _context.Prestamos.Add(prestamo);
            _context.SaveChanges();

            return Ok(prestamo);
        }

        // Actualiza los datos de un préstamo.
        [HttpPut("{id}")]
        public IActionResult ActualizarPrestamo(int id, Prestamo prestamoActualizado)
        {
            var prestamo = _context.Prestamos.Find(id);

            if (prestamo == null)
            {
                return NotFound("Préstamo no encontrado");
            }

            prestamo.EstudianteId = prestamoActualizado.EstudianteId;
            prestamo.HerramientaId = prestamoActualizado.HerramientaId;
            prestamo.FechaPrestamo = prestamoActualizado.FechaPrestamo;
            prestamo.FechaDevolucion = prestamoActualizado.FechaDevolucion;

            _context.SaveChanges();

            return Ok(prestamo);
        }

        // Elimina un préstamo.
        [HttpDelete("{id}")]
        public IActionResult EliminarPrestamo(int id)
        {
            var prestamo = _context.Prestamos.Find(id);

            if (prestamo == null)
            {
                return NotFound("Préstamo no encontrado");
            }

            _context.Prestamos.Remove(prestamo);
            _context.SaveChanges();

            return Ok("Préstamo eliminado correctamente");
        }

        // Registra la devolución y vuelve a habilitar la herramienta.
        [HttpPut("{id}/devolver")]
        public IActionResult DevolverHerramienta(int id)
        {
            var prestamo = _context.Prestamos.Find(id);

            if (prestamo == null)
            {
                return NotFound("Préstamo no encontrado");
            }

            if (prestamo.FechaDevolucion != null)
            {
                return BadRequest("Este préstamo ya fue devuelto");
            }

            var herramienta = _context.Herramientas.Find(prestamo.HerramientaId);

            prestamo.FechaDevolucion = DateTime.Now;

            if (herramienta != null)
            {
                herramienta.Disponible = true;
            }

            _context.SaveChanges();

            return Ok(prestamo);
        }
    }
}
