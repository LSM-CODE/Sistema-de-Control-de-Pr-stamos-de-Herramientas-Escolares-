using ControldePrestamos.Domain.Entidades;
using ControldePrestamos.Domain.Repositorio;
using ControldePrestamos.Infrastructure.Contexto;

namespace ControldePrestamos.Infrastructure.Repositorios
{
    public class EstudianteRepositorio : IEstudianteRepositorio
    {
        private readonly PrestamosContext _context;

        public EstudianteRepositorio(PrestamosContext context)
        {
            _context = context;
        }

        public List<Estudiante> ObtenerTodos()
        {
            return _context.Estudiantes.ToList();
        }

        public Estudiante ObtenerPorId(int id)
        {
            return _context.Estudiantes.Find(id)!;
        }

        public void Agregar(Estudiante estudiante)
        {
            _context.Estudiantes.Add(estudiante);
            _context.SaveChanges();
        }

        public void Actualizar(Estudiante estudiante)
        {
            _context.Estudiantes.Update(estudiante);
            _context.SaveChanges();
        }

        public void Eliminar(int id)
        {
            var estudiante = _context.Estudiantes.Find(id);

            if (estudiante != null)
            {
                _context.Estudiantes.Remove(estudiante);
                _context.SaveChanges();
            }
        }
    }
}