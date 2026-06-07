using System.ComponentModel.DataAnnotations;

namespace SistemaGestionEventos.Models
{
    public class ClienteModel
    {
        [Key]
        public int ClienteId { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;

        public string Correo { get; set; } = string.Empty;

        public DateTime FechaRegistro { get; set; }

        public ICollection<ReservaModel>? Reservas { get; set; }
    }
}
