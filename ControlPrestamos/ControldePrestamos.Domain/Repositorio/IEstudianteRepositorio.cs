using ControldePrestamos.Domain.Entidades;


namespace ControldePrestamos.Domain.Repositorio
{
    public interface IEstudianteRepositorio
    {
        List<Estudiante> ObtenerTodos();
        Estudiante ObtenerPorId(int id);
        void Agregar(Estudiante estudiante);
        void Actualizar(Estudiante estudiante);
        void Eliminar(int id);
    }
}