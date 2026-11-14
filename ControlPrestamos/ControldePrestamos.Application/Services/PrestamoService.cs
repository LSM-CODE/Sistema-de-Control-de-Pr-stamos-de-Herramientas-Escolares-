using ControldePrestamos.Application.Contract;
using ControldePrestamos.Domain.Entidades;
using ControldePrestamos.Domain.Repositorio;

namespace ControldePrestamos.Application.Services
{
    public class PrestamoService : IPrestamoService
    {
        private readonly IPrestamoRepositorio _prestamoRepositorio;
        private readonly IEstudianteRepositorio _estudianteRepositorio;
        private readonly IHerramientaRepositorio _herramientaRepositorio;

        public PrestamoService(
            IPrestamoRepositorio prestamoRepositorio,
            IEstudianteRepositorio estudianteRepositorio,
            IHerramientaRepositorio herramientaRepositorio)
        {
            _prestamoRepositorio = prestamoRepositorio;
            _estudianteRepositorio = estudianteRepositorio;
            _herramientaRepositorio = herramientaRepositorio;
        }

        public List<Prestamo> ObtenerTodos()
        {
            return _prestamoRepositorio.ObtenerTodos();
        }

        public Prestamo ObtenerPorId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID no es válido.");

            return _prestamoRepositorio.ObtenerPorId(id);
        }

        public void Agregar(Prestamo prestamo)
        {
            if (prestamo.EstudianteId <= 0)
                throw new ArgumentException("Debe seleccionar un estudiante.");

            if (prestamo.HerramientaId <= 0)
                throw new ArgumentException("Debe seleccionar una herramienta.");

            if (prestamo.FechaPrestamo == default)
                throw new ArgumentException("La fecha del préstamo es obligatoria.");

            var estudiante = _estudianteRepositorio.ObtenerPorId(prestamo.EstudianteId);

            if (estudiante == null)
                throw new ArgumentException("El estudiante no existe.");

            var herramienta = _herramientaRepositorio.ObtenerPorId(prestamo.HerramientaId);

            if (herramienta == null)
                throw new ArgumentException("La herramienta no existe.");

            if (!herramienta.Disponible || herramienta.Danada)
                throw new ArgumentException("La herramienta no está disponible.");

            herramienta.Disponible = false;

            _herramientaRepositorio.Actualizar(herramienta);
            _prestamoRepositorio.Agregar(prestamo);
        }

        public void Actualizar(Prestamo prestamo)
        {
            if (prestamo.Id <= 0)
                throw new ArgumentException("El ID no es válido.");

            if (prestamo.EstudianteId <= 0 || prestamo.HerramientaId <= 0)
                throw new ArgumentException("Debe seleccionar un estudiante y una herramienta.");

            if (prestamo.FechaPrestamo == default)
                throw new ArgumentException("La fecha del préstamo es obligatoria.");

            _prestamoRepositorio.Actualizar(prestamo);
        }

        public void Eliminar(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID no es válido.");

            _prestamoRepositorio.Eliminar(id);
        }

        public void Devolver(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID no es válido.");

            var prestamo = _prestamoRepositorio.ObtenerPorId(id);

            if (prestamo == null)
                throw new ArgumentException("Préstamo no encontrado.");

            if (prestamo.FechaDevolucion != null)
                throw new ArgumentException("Este préstamo ya fue devuelto.");

            var herramienta = _herramientaRepositorio.ObtenerPorId(prestamo.HerramientaId);

            if (herramienta == null)
                throw new ArgumentException("Herramienta no encontrada.");

            prestamo.FechaDevolucion = DateTime.Now;
            herramienta.Disponible = true;

            _herramientaRepositorio.Actualizar(herramienta);
            _prestamoRepositorio.Actualizar(prestamo);
        }
    }
}