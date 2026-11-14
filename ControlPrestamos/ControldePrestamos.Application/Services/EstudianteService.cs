using ControldePrestamos.Application.Contract;
using ControldePrestamos.Domain.Entidades;
using ControldePrestamos.Domain.Repositorio;

namespace ControldePrestamos.Application.Services
{
    public class EstudianteService : IEstudianteService
    {
        private readonly IEstudianteRepositorio _repositorio;

        public EstudianteService(IEstudianteRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public List<Estudiante> ObtenerTodos()
        {
            return _repositorio.ObtenerTodos();
        }

        public Estudiante ObtenerPorId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID no es válido.");

            return _repositorio.ObtenerPorId(id);
        }

        public void Agregar(Estudiante estudiante)
        {
            if (string.IsNullOrWhiteSpace(estudiante.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(estudiante.Matricula))
                throw new ArgumentException("La matrícula es obligatoria.");

            if (string.IsNullOrWhiteSpace(estudiante.Carrera))
                throw new ArgumentException("La carrera es obligatoria.");

            _repositorio.Agregar(estudiante);
        }

        public void Actualizar(Estudiante estudiante)
        {
            if (estudiante.Id <= 0)
                throw new ArgumentException("El ID no es válido.");

            if (string.IsNullOrWhiteSpace(estudiante.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(estudiante.Matricula))
                throw new ArgumentException("La matrícula es obligatoria.");

            if (string.IsNullOrWhiteSpace(estudiante.Carrera))
                throw new ArgumentException("La carrera es obligatoria.");

            _repositorio.Actualizar(estudiante);
        }

        public void Eliminar(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID no es válido.");

            _repositorio.Eliminar(id);
        }
    }
}