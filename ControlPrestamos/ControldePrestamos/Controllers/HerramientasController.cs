

using Microsoft.AspNetCore.Mvc;
using ControldePrestamos.Models;
using ControldePrestamos.Contexto;
using ControldePrestamos.Dtos;

namespace ControldePrestamos.Controllers
{
    // Este controlador maneja las operaciones de las herramientas.
    [ApiController]
    [Route("api/[controller]")]
    public class HerramientasController : ControllerBase
    {
        // Permite acceder a las tablas de la base de datos.
        private readonly PrestamosContext _context;

        public HerramientasController(PrestamosContext context)
        {
            _context = context;
        }

        // GET: api/Herramientas
        // Obtiene todas las herramientas registradas.
        [HttpGet]
        public IActionResult ObtenerHerramientas()
        {
            return Ok(_context.Herramientas.ToList());
        }

        // POST: api/Herramientas
        // Registra una nueva herramienta usando los datos del DTO.
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

            // Agrega la herramienta y guarda los cambios en SQL Server.
            _context.Herramientas.Add(herramienta);
            _context.SaveChanges();

            return Ok(herramienta);
        }

        // PUT: api/Herramientas/{id}
        // Busca una herramienta y actualiza sus datos.
        [HttpPut("{id}")]
        public IActionResult ActualizarHerramienta(int id, Herramienta herramientaActualizada)
        {
            var herramienta = _context.Herramientas.Find(id);

            // Si no existe devuelve un error 404
            if (herramienta == null)
            {
                return NotFound("Herramienta no encontrada");
            }

            herramienta.Nombre = herramientaActualizada.Nombre;
            herramienta.Descripcion = herramientaActualizada.Descripcion;
            herramienta.Disponible = herramientaActualizada.Disponible;
            herramienta.Danada = herramientaActualizada.Danada;

            // Para guardar la actualización en la base de datos
            _context.SaveChanges();

            return Ok(herramienta);
        }

        // DELETE: api/Herramientas/{id}
        // Es para eliminar una herramienta por su ID.
        [HttpDelete("{id}")]
        public IActionResult EliminarHerramienta(int id)
        {
            var herramienta = _context.Herramientas.Find(id);

            if (herramienta == null)
            {
                return NotFound("Herramienta no encontrada");
            }

            _context.Herramientas.Remove(herramienta);
            _context.SaveChanges();

            return Ok("Herramienta eliminada correctamente");
        }
    }
}
