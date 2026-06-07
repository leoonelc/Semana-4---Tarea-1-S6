using System.ComponentModel.DataAnnotations;

namespace SistemaGestionEventos.Models
{
    public class ReservaModel
    {
        [Key]
        public int ReservaId { get; set; }

        [Display(Name = "Fecha de reserva")]
        public DateTime FechaReserva { get; set; }

        [Display(Name = "Estado")]
        public string Estado { get; set; } = string.Empty;

        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }

        public ClienteModel? Cliente { get; set; }

        [Display(Name = "Evento")]
        public int EventoId { get; set; }

        public EventoModel? Evento { get; set; }
    }
}
