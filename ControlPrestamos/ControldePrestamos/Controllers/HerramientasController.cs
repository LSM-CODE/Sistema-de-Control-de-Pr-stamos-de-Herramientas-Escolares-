

using Microsoft.AspNetCore.Mvc;
using ControldePrestamos.Domain.Entidades;
using ControldePrestamos.Application.Contract;
using ControldePrestamos.Application.Dtos;

namespace ControldePrestamos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HerramientasController : ControllerBase
    {
        private readonly IHerramientaService _servicio;

        public HerramientasController(IHerramientaService servicio)
        {
            _servicio = servicio;
        }

        // Consulta todas las herramientas.
        [HttpGet]
        public IActionResult ObtenerHerramientas()
        {
            return Ok(_servicio.ObtenerTodos());
        }

        // Registra una herramienta.
        [HttpPost]
        public IActionResult CrearHerramienta(HerramientaDto herramientaDto)
        {
            try
            {
                var herramienta = new Herramienta
                {
                    Nombre = herramientaDto.Nombre,
                    Descripcion = herramientaDto.Descripcion,
                    Disponible = herramientaDto.Disponible,
                    Danada = herramientaDto.Danada
                };

                _servicio.Agregar(herramienta);

                return Ok(herramienta);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Actualiza una herramienta.
        [HttpPut("{id}")]
        public IActionResult ActualizarHerramienta(int id, Herramienta herramientaActualizada)
        {
            try
            {
                var herramienta = _servicio.ObtenerPorId(id);

                if (herramienta == null)
                    return NotFound("Herramienta no encontrada");

                herramienta.Nombre = herramientaActualizada.Nombre;
                herramienta.Descripcion = herramientaActualizada.Descripcion;
                herramienta.Disponible = herramientaActualizada.Disponible;
                herramienta.Danada = herramientaActualizada.Danada;

                _servicio.Actualizar(herramienta);

                return Ok(herramienta);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Elimina una herramienta.
        [HttpDelete("{id}")]
        public IActionResult EliminarHerramienta(int id)
        {
            try
            {
                var herramienta = _servicio.ObtenerPorId(id);

                if (herramienta == null)
                    return NotFound("Herramienta no encontrada");

                _servicio.Eliminar(id);

                return Ok("Herramienta eliminada correctamente");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}