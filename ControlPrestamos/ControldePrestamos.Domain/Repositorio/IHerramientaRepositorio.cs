using ControldePrestamos.Domain.Entidades;


namespace ControldePrestamos.Domain.Repositorio
{
    public interface IHerramientaRepositorio
    {
        List<Herramienta> ObtenerTodos();
        Herramienta ObtenerPorId(int id);
        void Agregar(Herramienta herramienta);
        void Actualizar(Herramienta herramienta);
        void Eliminar(int id);
    }
}