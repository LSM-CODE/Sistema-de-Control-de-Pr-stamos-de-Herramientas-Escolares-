using ControldePrestamos.Domain.Entidades;


namespace ControldePrestamos.Domain.Repositorio
{
    public interface IPrestamoRepositorio
    {
        List<Prestamo> ObtenerTodos();
        Prestamo ObtenerPorId(int id);
        void Agregar(Prestamo prestamo);
        void Actualizar(Prestamo prestamo);
        void Eliminar(int id);
    }
}