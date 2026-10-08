using Microsoft.AspNetCore.Mvc;
using ControldePrestamos.Models;

namespace ControldePrestamos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrestamosController : ControllerBase
    {
        private static List<Prestamo> prestamos = new List<Prestamo>
        {
            new Prestamo
            {
                Id = 1,
                EstudianteId = 1,
                HerramientaId = 1,
                FechaPrestamo = DateTime.Now,
                FechaDevolucion = null
            }
        };

        [HttpGet]
        public IActionResult ObtenerPrestamos()
        {
            return Ok(prestamos);
        }
        [HttpPost]
        public IActionResult CrearPrestamo(Prestamo prestamo)
        {
            prestamo.Id = prestamos.Count + 1;

            prestamos.Add(prestamo);

            return Ok(prestamo);
        }
        [HttpPut("{id}")]
        public IActionResult ActualizarPrestamo(int id, Prestamo prestamoActualizado)
        {
            var prestamo = prestamos.FirstOrDefault(p => p.Id == id);

            if (prestamo == null)
            {
                return NotFound("Préstamo no encontrado");
            }

            prestamo.EstudianteId = prestamoActualizado.EstudianteId;
            prestamo.HerramientaId = prestamoActualizado.HerramientaId;
            prestamo.FechaPrestamo = prestamoActualizado.FechaPrestamo;
            prestamo.FechaDevolucion = prestamoActualizado.FechaDevolucion;

            return Ok(prestamo);
        }
        [HttpDelete("{id}")]
        public IActionResult EliminarPrestamo(int id)
        {
            var prestamo = prestamos.FirstOrDefault(p => p.Id == id);

            if (prestamo == null)
            {
                return NotFound("Préstamo no encontrado");
            }

            prestamos.Remove(prestamo);

            return Ok("Préstamo eliminado correctamente");
        }
    }
}