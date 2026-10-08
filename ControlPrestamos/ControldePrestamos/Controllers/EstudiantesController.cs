using Microsoft.AspNetCore.Mvc;
using ControldePrestamos.Models;

namespace ControldePrestamos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstudiantesController : ControllerBase
    {
        private static List<Estudiante> estudiantes = new List<Estudiante>
        {
            new Estudiante
            {
                Id = 1,
                Nombre = "Leonardo Soriano",
                Matricula = "2024-1001",
                Carrera = "Desarrollo de Software"
            },

            new Estudiante
            {
                Id = 2,
                Nombre = "María Rodríguez",
                Matricula = "2024-1002",
                Carrera = "Multimedia"
            }
        };

        [HttpGet]
        public IActionResult ObtenerEstudiantes()
        {
            return Ok(estudiantes);
        }
        [HttpPost]
        public IActionResult CrearEstudiante(Estudiante estudiante)
        {
            estudiante.Id = estudiantes.Count + 1;

            estudiantes.Add(estudiante);

            return Ok(estudiante);
        }
        [HttpPut("{id}")]
        public IActionResult ActualizarEstudiante(int id, Estudiante estudianteActualizado)
        {
            var estudiante = estudiantes.FirstOrDefault(e => e.Id == id);

            if (estudiante == null)
            {
                return NotFound("Estudiante no encontrado");
            }

            estudiante.Nombre = estudianteActualizado.Nombre;
            estudiante.Matricula = estudianteActualizado.Matricula;
            estudiante.Carrera = estudianteActualizado.Carrera;

            return Ok(estudiante);
        }
        [HttpDelete("{id}")]
        public IActionResult EliminarEstudiante(int id)
        {
            var estudiante = estudiantes.FirstOrDefault(e => e.Id == id);

            if (estudiante == null)
            {
                return NotFound("Estudiante no encontrado");
            }

            estudiantes.Remove(estudiante);

            return Ok("Estudiante eliminado correctamente");
        }
    }
}