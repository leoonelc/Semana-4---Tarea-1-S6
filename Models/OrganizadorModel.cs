using System.ComponentModel.DataAnnotations;

namespace SistemaGestionEventos.Models
{
    public class OrganizadorModel
    {
        [Key]
        public int OrganizadorId { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Especialidad { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;

        public bool Activo { get; set; }

        public ICollection<EventoModel>? Eventos { get; set; }
    }
}
