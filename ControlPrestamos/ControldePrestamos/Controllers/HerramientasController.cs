

using Microsoft.AspNetCore.Mvc;
using ControldePrestamos.Domain.Entidades;
using ControldePrestamos.Domain.Repositorio;
using ControldePrestamos.Infrastructure.Modelos;
namespace ControldePrestamos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HerramientasController : ControllerBase
    {
        private readonly IHerramientaRepositorio _repositorio;

        public HerramientasController(IHerramientaRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        // GET: api/Herramientas
        [HttpGet]
        public IActionResult ObtenerHerramientas()
        {
            return Ok(_repositorio.ObtenerTodos());
        }

        // POST: api/Herramientas
        [HttpPost]
        public IActionResult CrearHerramienta(HerramientaDto herramientaDto)
        {
            var herramienta = new Herramienta
            {
                Nombre = herramientaDto.Nombre,
                Descripcion = herramientaDto.Descripcion,
                Disponible = herramientaDto.Disponible,
                Danada = herramientaDto.Danada
            };

            _repositorio.Agregar(herramienta);

            return Ok(herramienta);
        }

        // PUT: api/Herramientas/{id}
        [HttpPut("{id}")]
        public IActionResult ActualizarHerramienta(int id, Herramienta herramientaActualizada)
        {
            var herramienta = _repositorio.ObtenerPorId(id);

            if (herramienta == null)
            {
                return NotFound("Herramienta no encontrada");
            }

            herramienta.Nombre = herramientaActualizada.Nombre;
            herramienta.Descripcion = herramientaActualizada.Descripcion;
            herramienta.Disponible = herramientaActualizada.Disponible;
            herramienta.Danada = herramientaActualizada.Danada;

            _repositorio.Actualizar(herramienta);

            return Ok(herramienta);
        }

        // DELETE: api/Herramientas/{id}
        [HttpDelete("{id}")]
        public IActionResult EliminarHerramienta(int id)
        {
            var herramienta = _repositorio.ObtenerPorId(id);

            if (herramienta == null)
            {
                return NotFound("Herramienta no encontrada");
            }

            _repositorio.Eliminar(id);

            return Ok("Herramienta eliminada correctamente");
        }
    }
}