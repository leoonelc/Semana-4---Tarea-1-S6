using System.ComponentModel.DataAnnotations;

namespace SistemaGestionEventos.Models
{
    public class LugarModel
    {
        [Key]
        public int LugarId { get; set; }

        [Display(Name = "Nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Display(Name = "Ciudad")]
        public string Ciudad { get; set; } = string.Empty;

        [Display(Name = "Capacidad")]
        public int Capacidad { get; set; }

        [Display(Name = "Tipo")]
        public string Tipo { get; set; } = string.Empty;

        public ICollection<EventoModel>? Eventos { get; set; }
    }
}
