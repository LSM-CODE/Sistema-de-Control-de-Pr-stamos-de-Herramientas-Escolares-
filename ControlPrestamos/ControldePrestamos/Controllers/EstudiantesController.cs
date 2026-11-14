

using Microsoft.AspNetCore.Mvc;
using ControldePrestamos.Domain.Entidades;
using ControldePrestamos.Domain.Repositorio;
using ControldePrestamos.Infrastructure.Modelos;

namespace ControldePrestamos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstudiantesController : ControllerBase
    {
        private readonly IEstudianteRepositorio _repositorio;

        public EstudiantesController(IEstudianteRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        // Consulta todos los estudiantes.
        [HttpGet]
        public IActionResult ObtenerEstudiantes()
        {
            return Ok(_repositorio.ObtenerTodos());
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

            _repositorio.Agregar(estudiante);

            return Ok(estudiante);
        }

        // Actualiza los datos de un estudiante.
        [HttpPut("{id}")]
        public IActionResult ActualizarEstudiante(int id, Estudiante estudianteActualizado)
        {
            var estudiante = _repositorio.ObtenerPorId(id);

            if (estudiante == null)
            {
                return NotFound("Estudiante no encontrado");
            }

            estudiante.Nombre = estudianteActualizado.Nombre;
            estudiante.Matricula = estudianteActualizado.Matricula;
            estudiante.Carrera = estudianteActualizado.Carrera;

            _repositorio.Actualizar(estudiante);

            return Ok(estudiante);
        }

        // Elimina un estudiante.
        [HttpDelete("{id}")]
        public IActionResult EliminarEstudiante(int id)
        {
            var estudiante = _repositorio.ObtenerPorId(id);

            if (estudiante == null)
            {
                return NotFound("Estudiante no encontrado");
            }

            _repositorio.Eliminar(id);

            return Ok("Estudiante eliminado correctamente");
        }
    }
}