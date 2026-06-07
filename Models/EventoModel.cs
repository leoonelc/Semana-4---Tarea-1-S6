using System.ComponentModel.DataAnnotations;

namespace SistemaGestionEventos.Models
{
    public class EventoModel
    {
        [Key]
        public int EventoId { get; set; }

        [Display(Name = "Titulo")]
        public string Titulo { get; set; } = string.Empty;

        [Display(Name = "Fecha del evento")]
        public DateTime FechaEvento { get; set; }

        [Display(Name = "Lugar")]
        public int LugarId { get; set; }

        public LugarModel? Lugar { get; set; }

        [Display(Name = "Organizador")]
        public int OrganizadorId { get; set; }

        public OrganizadorModel? Organizador { get; set; }

        public ICollection<ReservaModel>? Reservas { get; set; }
    }
}
