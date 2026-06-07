using System.ComponentModel.DataAnnotations;

namespace SistemaGestionEventos.Models
{
    public class ClienteModel
    {
        [Key]
        public int ClienteId { get; set; }

        [Display(Name = "Nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Display(Name = "Telefono")]
        public string Telefono { get; set; } = string.Empty;

        [Display(Name = "Correo")]
        public string Correo { get; set; } = string.Empty;

        [Display(Name = "Fecha de registro")]
        public DateTime FechaRegistro { get; set; }

        public ICollection<ReservaModel>? Reservas { get; set; }
    }
}
