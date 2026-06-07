using System.ComponentModel.DataAnnotations;

namespace SistemaGestionEventos.Models
{
    public class LugarModel
    {
        [Key]
        public int LugarId { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Ciudad { get; set; } = string.Empty;

        public int Capacidad { get; set; }

        public string Tipo { get; set; } = string.Empty;

        public ICollection<EventoModel>? Eventos { get; set; }
    }
}
