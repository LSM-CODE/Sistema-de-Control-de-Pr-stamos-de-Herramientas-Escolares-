

using Microsoft.AspNetCore.Mvc;
using ControldePrestamos.Models;
using ControldePrestamos.Contexto;
using ControldePrestamos.Dtos;

namespace ControldePrestamos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstudiantesController : ControllerBase
    {
        // Conexión con la base de datos.
        private readonly PrestamosContext _context;

        public EstudiantesController(PrestamosContext context)
        {
            _context = context;
        }

        // Consulta todos los estudiantes.
        [HttpGet]
        public IActionResult ObtenerEstudiantes()
        {
            return Ok(_context.Estudiantes.ToList());
        }

        // Registra un estudiante nuevo.
        [HttpPost]
        public IActionResult CrearEstudiante(EstudianteDto estudianteDto)
        {
            var estudiante = new Estudiante
            {
                Nombre = estudianteDto.Nombre,
                Matricula = estudianteDto.Matricula,
                Carrera = estudianteDto.Carrera
            };

            _context.Estudiantes.Add(estudiante);
            _context.SaveChanges();

            return Ok(estudiante);
        }

        // Actualiza los datos de un estudiante.
        [HttpPut("{id}")]
        public IActionResult ActualizarEstudiante(int id, Estudiante estudianteActualizado)
        {
            var estudiante = _context.Estudiantes.Find(id);

            if (estudiante == null)
            {
                return NotFound("Estudiante no encontrado");
            }

            estudiante.Nombre = estudianteActualizado.Nombre;
            estudiante.Matricula = estudianteActualizado.Matricula;
            estudiante.Carrera = estudianteActualizado.Carrera;

            _context.SaveChanges();

            return Ok(estudiante);
        }

        // Elimina un estudiante.
        [HttpDelete("{id}")]
        public IActionResult EliminarEstudiante(int id)
        {
            var estudiante = _context.Estudiantes.Find(id);

            if (estudiante == null)
            {
                return NotFound("Estudiante no encontrado");
            }

            _context.Estudiantes.Remove(estudiante);
            _context.SaveChanges();

            return Ok("Estudiante eliminado correctamente");
        }
    }
}
