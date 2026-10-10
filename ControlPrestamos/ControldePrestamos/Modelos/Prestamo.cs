
namespace ControldePrestamos.Models
{
    public class Prestamo
    {
        public int Id { get; set; }

        public int EstudianteId { get; set; }

        public int HerramientaId { get; set; }

        public DateTime FechaPrestamo { get; set; }

        public DateTime? FechaDevolucion { get; set; }

        public Estudiante? Estudiante { get; set; }

        public Herramienta? Herramienta { get; set; }
    }
}
