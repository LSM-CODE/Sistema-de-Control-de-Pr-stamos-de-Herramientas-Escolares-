

using Microsoft.AspNetCore.Mvc;
using ControldePrestamos.Domain.Entidades;
using ControldePrestamos.Domain.Repositorio;
using ControldePrestamos.Infrastructure.Modelos;
namespace ControldePrestamos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrestamosController : ControllerBase
    {
        private readonly IPrestamoRepositorio _prestamoRepositorio;
        private readonly IEstudianteRepositorio _estudianteRepositorio;
        private readonly IHerramientaRepositorio _herramientaRepositorio;

        public PrestamosController(
            IPrestamoRepositorio prestamoRepositorio,
            IEstudianteRepositorio estudianteRepositorio,
            IHerramientaRepositorio herramientaRepositorio)
        {
            _prestamoRepositorio = prestamoRepositorio;
            _estudianteRepositorio = estudianteRepositorio;
            _herramientaRepositorio = herramientaRepositorio;
        }

        // Consulta todos los préstamos.
        [HttpGet]
        public IActionResult ObtenerPrestamos()
        {
            return Ok(_prestamoRepositorio.ObtenerTodos());
        }

        // Registra un préstamo.
        [HttpPost]
        public IActionResult CrearPrestamo(PrestamoDto prestamoDto)
        {
            var estudiante = _estudianteRepositorio.ObtenerPorId(prestamoDto.EstudianteId);

            if (estudiante == null)
            {
                return NotFound("Estudiante no encontrado");
            }

            var herramienta = _herramientaRepositorio.ObtenerPorId(prestamoDto.HerramientaId);

            if (herramienta == null)
            {
                return NotFound("Herramienta no encontrada");
            }

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

            _herramientaRepositorio.Actualizar(herramienta);
            _prestamoRepositorio.Agregar(prestamo);

            return Ok(prestamo);
        }

        // Actualiza los datos de un préstamo.
        [HttpPut("{id}")]
        public IActionResult ActualizarPrestamo(int id, Prestamo prestamoActualizado)
        {
            var prestamo = _prestamoRepositorio.ObtenerPorId(id);

            if (prestamo == null)
            {
                return NotFound("Préstamo no encontrado");
            }

            prestamo.EstudianteId = prestamoActualizado.EstudianteId;
            prestamo.HerramientaId = prestamoActualizado.HerramientaId;
            prestamo.FechaPrestamo = prestamoActualizado.FechaPrestamo;
            prestamo.FechaDevolucion = prestamoActualizado.FechaDevolucion;

            _prestamoRepositorio.Actualizar(prestamo);

            return Ok(prestamo);
        }

        // Elimina un préstamo.
        [HttpDelete("{id}")]
        public IActionResult EliminarPrestamo(int id)
        {
            var prestamo = _prestamoRepositorio.ObtenerPorId(id);

            if (prestamo == null)
            {
                return NotFound("Préstamo no encontrado");
            }

            _prestamoRepositorio.Eliminar(id);

            return Ok("Préstamo eliminado correctamente");
        }

        // Registra la devolución.
        [HttpPut("{id}/devolver")]
        public IActionResult DevolverHerramienta(int id)
        {
            var prestamo = _prestamoRepositorio.ObtenerPorId(id);

            if (prestamo == null)
            {
                return NotFound("Préstamo no encontrado");
            }

            if (prestamo.FechaDevolucion != null)
            {
                return BadRequest("Este préstamo ya fue devuelto");
            }

            var herramienta = _herramientaRepositorio.ObtenerPorId(prestamo.HerramientaId);

            prestamo.FechaDevolucion = DateTime.Now;

            if (herramienta != null)
            {
                herramienta.Disponible = true;
                _herramientaRepositorio.Actualizar(herramienta);
            }

            _prestamoRepositorio.Actualizar(prestamo);

            return Ok(prestamo);
        }
    }
}