using System.ComponentModel.DataAnnotations;

namespace SistemaGestionEventos.Models
{
    public class OrganizadorModel
    {
        [Key]
        public int OrganizadorId { get; set; }

        [Display(Name = "Nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Display(Name = "Especialidad")]
        public string Especialidad { get; set; } = string.Empty;

        [Display(Name = "Telefono")]
        public string Telefono { get; set; } = string.Empty;

        [Display(Name = "Activo")]
        public bool Activo { get; set; }

        public ICollection<EventoModel>? Eventos { get; set; }
    }
}
