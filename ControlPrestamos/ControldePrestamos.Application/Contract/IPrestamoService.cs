using ControldePrestamos.Domain.Entidades;

namespace ControldePrestamos.Application.Contract
{
    public interface IPrestamoService
    {
        List<Prestamo> ObtenerTodos();
        Prestamo ObtenerPorId(int id);
        void Agregar(Prestamo prestamo);
        void Actualizar(Prestamo prestamo);
        void Eliminar(int id);
        void Devolver(int id);
    }
}