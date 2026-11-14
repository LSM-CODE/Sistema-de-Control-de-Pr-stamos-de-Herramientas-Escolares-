using ControldePrestamos.Domain.Entidades;

namespace ControldePrestamos.Application.Contract
{
    public interface IEstudianteService
    {
        List<Estudiante> ObtenerTodos();
        Estudiante ObtenerPorId(int id);
        void Agregar(Estudiante estudiante);
        void Actualizar(Estudiante estudiante);
        void Eliminar(int id);
    }
}