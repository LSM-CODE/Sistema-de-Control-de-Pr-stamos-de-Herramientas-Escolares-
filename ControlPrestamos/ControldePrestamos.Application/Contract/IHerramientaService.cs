using ControldePrestamos.Domain.Entidades;

namespace ControldePrestamos.Application.Contract
{
    public interface IHerramientaService
    {
        List<Herramienta> ObtenerTodos();
        Herramienta ObtenerPorId(int id);
        void Agregar(Herramienta herramienta);
        void Actualizar(Herramienta herramienta);
        void Eliminar(int id);
    }
}