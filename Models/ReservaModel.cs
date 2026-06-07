using System.ComponentModel.DataAnnotations;

namespace SistemaGestionEventos.Models
{
    public class ReservaModel
    {
        [Key]
        public int ReservaId { get; set; }

        public DateTime FechaReserva { get; set; }

        public string Estado { get; set; } = string.Empty;

        public int ClienteId { get; set; }

        public ClienteModel? Cliente { get; set; }

        public int EventoId { get; set; }

        public EventoModel? Evento { get; set; }
    }
}
