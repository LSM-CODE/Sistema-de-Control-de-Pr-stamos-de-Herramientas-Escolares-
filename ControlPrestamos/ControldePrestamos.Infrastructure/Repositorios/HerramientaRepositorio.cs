using ControldePrestamos.Domain.Entidades;
using ControldePrestamos.Domain.Repositorio;
using ControldePrestamos.Infrastructure.Contexto;

namespace ControldePrestamos.Infrastructure.Repositorios
{
    public class HerramientaRepositorio : IHerramientaRepositorio
    {
        private readonly PrestamosContext _context;

        public HerramientaRepositorio(PrestamosContext context)
        {
            _context = context;
        }

        public List<Herramienta> ObtenerTodos()
        {
            return _context.Herramientas.ToList();
        }

        public Herramienta ObtenerPorId(int id)
        {
            return _context.Herramientas.Find(id)!;
        }

        public void Agregar(Herramienta herramienta)
        {
            _context.Herramientas.Add(herramienta);
            _context.SaveChanges();
        }

        public void Actualizar(Herramienta herramienta)
        {
            _context.Herramientas.Update(herramienta);
            _context.SaveChanges();
        }

        public void Eliminar(int id)
        {
            var herramienta = _context.Herramientas.Find(id);

            if (herramienta != null)
            {
                _context.Herramientas.Remove(herramienta);
                _context.SaveChanges();
            }
        }
    }
}