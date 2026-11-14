using ControldePrestamos.Domain.Entidades;
using ControldePrestamos.Domain.Repositorio;
using ControldePrestamos.Infrastructure.Contexto;

namespace ControldePrestamos.Infrastructure.Repositorios
{
    public class PrestamoRepositorio : IPrestamoRepositorio
    {
        private readonly PrestamosContext _context;

        public PrestamoRepositorio(PrestamosContext context)
        {
            _context = context;
        }

        public List<Prestamo> ObtenerTodos()
        {
            return _context.Prestamos.ToList();
        }

        public Prestamo ObtenerPorId(int id)
        {
            return _context.Prestamos.Find(id)!;
        }

        public void Agregar(Prestamo prestamo)
        {
            _context.Prestamos.Add(prestamo);
            _context.SaveChanges();
        }

        public void Actualizar(Prestamo prestamo)
        {
            _context.Prestamos.Update(prestamo);
            _context.SaveChanges();
        }

        public void Eliminar(int id)
        {
            var prestamo = _context.Prestamos.Find(id);

            if (prestamo != null)
            {
                _context.Prestamos.Remove(prestamo);
                _context.SaveChanges();
            }
        }
    }
}