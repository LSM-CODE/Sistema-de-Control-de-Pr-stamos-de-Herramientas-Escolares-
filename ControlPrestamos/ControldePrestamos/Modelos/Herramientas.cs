namespace ControldePrestamos.Models
{
    public class Herramienta
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = "";

        public string Descripcion { get; set; } = "";

        public bool Disponible { get; set; }
    }
} 
