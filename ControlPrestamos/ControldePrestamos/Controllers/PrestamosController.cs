using Microsoft.AspNetCore.Mvc;
using ControldePrestamos.Domain.Entidades;
using ControldePrestamos.Application.Contract;
using ControldePrestamos.Application.Dtos;

namespace ControldePrestamos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrestamosController : ControllerBase
    {
        private readonly IPrestamoService _servicio;

        public PrestamosController(IPrestamoService servicio)
        {
            _servicio = servicio;
        }

        // Consulta todos los préstamos.
        [HttpGet]
        public IActionResult ObtenerPrestamos()
        {
            return Ok(_servicio.ObtenerTodos());
        }

        // Registra un préstamo.
        [HttpPost]
        public IActionResult CrearPrestamo(PrestamoDto prestamoDto)
        {
            try
            {
                var prestamo = new Prestamo
                {
                    EstudianteId = prestamoDto.EstudianteId,
                    HerramientaId = prestamoDto.HerramientaId,
                    FechaPrestamo = DateTime.Now,
                    FechaDevolucion = null
                };

                _servicio.Agregar(prestamo);

                return Ok(prestamo);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Actualiza los datos de un préstamo.
        [HttpPut("{id}")]
        public IActionResult ActualizarPrestamo(int id, Prestamo prestamoActualizado)
        {
            try
            {
                var prestamo = _servicio.ObtenerPorId(id);

                if (prestamo == null)
                    return NotFound("Préstamo no encontrado");

                prestamo.EstudianteId = prestamoActualizado.EstudianteId;
                prestamo.HerramientaId = prestamoActualizado.HerramientaId;
                prestamo.FechaPrestamo = prestamoActualizado.FechaPrestamo;
                prestamo.FechaDevolucion = prestamoActualizado.FechaDevolucion;

                _servicio.Actualizar(prestamo);

                return Ok(prestamo);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Elimina un préstamo.
        [HttpDelete("{id}")]
        public IActionResult EliminarPrestamo(int id)
        {
            try
            {
                var prestamo = _servicio.ObtenerPorId(id);

                if (prestamo == null)
                    return NotFound("Préstamo no encontrado");

                _servicio.Eliminar(id);

                return Ok("Préstamo eliminado correctamente");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Registra la devolución.
        [HttpPut("{id}/devolver")]
        public IActionResult DevolverHerramienta(int id)
        {
            try
            {
                _servicio.Devolver(id);

                return Ok("Herramienta devuelta correctamente");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}