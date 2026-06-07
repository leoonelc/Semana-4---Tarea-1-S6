using System.ComponentModel.DataAnnotations;

namespace SistemaGestionEventos.Models
{
    public class EventoModel
    {
        [Key]
        public int EventoId { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public DateTime FechaEvento { get; set; }

        public int LugarId { get; set; }

        public LugarModel? Lugar { get; set; }

        public int OrganizadorId { get; set; }

        public OrganizadorModel? Organizador { get; set; }

        public ICollection<ReservaModel>? Reservas { get; set; }
    }
}
