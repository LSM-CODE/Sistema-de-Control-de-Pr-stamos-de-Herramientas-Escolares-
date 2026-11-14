

using Microsoft.AspNetCore.Mvc;
using ControldePrestamos.Domain.Entidades;
using ControldePrestamos.Application.Contract;
using ControldePrestamos.Application.Dtos;

namespace ControldePrestamos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstudiantesController : ControllerBase
    {
        private readonly IEstudianteService _servicio;

        public EstudiantesController(IEstudianteService servicio)
        {
            _servicio = servicio;
        }

        // Consulta todos los estudiantes.
        [HttpGet]
        public IActionResult ObtenerEstudiantes()
        {
            return Ok(_servicio.ObtenerTodos());
        }

        // Registra un estudiante nuevo.
        [HttpPost]
        public IActionResult CrearEstudiante(EstudianteDto estudianteDto)
        {
            try
            {
                var estudiante = new Estudiante
                {
                    Nombre = estudianteDto.Nombre,
                    Matricula = estudianteDto.Matricula,
                    Carrera = estudianteDto.Carrera
                };

                _servicio.Agregar(estudiante);

                return Ok(estudiante);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Actualiza los datos de un estudiante.
        [HttpPut("{id}")]
        public IActionResult ActualizarEstudiante(int id, Estudiante estudianteActualizado)
        {
            try
            {
                var estudiante = _servicio.ObtenerPorId(id);

                if (estudiante == null)
                    return NotFound("Estudiante no encontrado");

                estudiante.Nombre = estudianteActualizado.Nombre;
                estudiante.Matricula = estudianteActualizado.Matricula;
                estudiante.Carrera = estudianteActualizado.Carrera;

                _servicio.Actualizar(estudiante);

                return Ok(estudiante);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Elimina un estudiante.
        [HttpDelete("{id}")]
        public IActionResult EliminarEstudiante(int id)
        {
            try
            {
                var estudiante = _servicio.ObtenerPorId(id);

                if (estudiante == null)
                    return NotFound("Estudiante no encontrado");

                _servicio.Eliminar(id);

                return Ok("Estudiante eliminado correctamente");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}