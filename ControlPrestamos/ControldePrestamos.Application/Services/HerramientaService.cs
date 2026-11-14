using ControldePrestamos.Application.Contract;
using ControldePrestamos.Domain.Entidades;
using ControldePrestamos.Domain.Repositorio;

namespace ControldePrestamos.Application.Services
{
    public class HerramientaService : IHerramientaService
    {
        private readonly IHerramientaRepositorio _repositorio;

        public HerramientaService(IHerramientaRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public List<Herramienta> ObtenerTodos()
        {
            return _repositorio.ObtenerTodos();
        }

        public Herramienta ObtenerPorId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID no es válido.");

            return _repositorio.ObtenerPorId(id);
        }

        public void Agregar(Herramienta herramienta)
        {
            if (string.IsNullOrWhiteSpace(herramienta.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(herramienta.Descripcion))
                throw new ArgumentException("La descripción es obligatoria.");

            _repositorio.Agregar(herramienta);
        }

        public void Actualizar(Herramienta herramienta)
        {
            if (herramienta.Id <= 0)
                throw new ArgumentException("El ID no es válido.");

            if (string.IsNullOrWhiteSpace(herramienta.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(herramienta.Descripcion))
                throw new ArgumentException("La descripción es obligatoria.");

            _repositorio.Actualizar(herramienta);
        }

        public void Eliminar(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID no es válido.");

            _repositorio.Eliminar(id);
        }
    }
}