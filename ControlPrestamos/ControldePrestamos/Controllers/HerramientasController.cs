using Microsoft.AspNetCore.Mvc;
using ControldePrestamos.Models;

namespace ControldePrestamos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HerramientasController : ControllerBase
    {
        public static List<Herramienta> herramientas = new List<Herramienta>
        {
            new Herramienta
            {
                Id = 1,
                Nombre = "Martillo",
                Descripcion = "Martillo de uso escolar",
                Disponible = true
            },

            new Herramienta
            {
                Id = 2,
                Nombre = "Destornillador",
                Descripcion = "Destornillador Phillips",
                Disponible = true
            }
        };

        [HttpGet]
        public IActionResult ObtenerHerramientas()
        {
            return Ok(herramientas);
        }

        [HttpPost]
        public IActionResult CrearHerramienta(Herramienta herramienta)
        {
            herramienta.Id = herramientas.Count + 1;

            herramientas.Add(herramienta);

            return Ok(herramienta);
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarHerramienta(int id, Herramienta herramientaActualizada)
        {
            var herramienta = herramientas.FirstOrDefault(h => h.Id == id);

            if (herramienta == null)
            {
                return NotFound("Herramienta no encontrada");
            }

            herramienta.Nombre = herramientaActualizada.Nombre;
            herramienta.Descripcion = herramientaActualizada.Descripcion;
            herramienta.Disponible = herramientaActualizada.Disponible;

            return Ok(herramienta);
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarHerramienta(int id)
        {
            var herramienta = herramientas.FirstOrDefault(h => h.Id == id);

            if (herramienta == null)
            {
                return NotFound("Herramienta no encontrada");
            }

            herramientas.Remove(herramienta);

            return Ok("Herramienta eliminada correctamente");
        }
    }
}