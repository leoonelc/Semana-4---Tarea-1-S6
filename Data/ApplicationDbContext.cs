using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SistemaGestionEventos.Models;

namespace SistemaGestionEventos.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<OrganizadorModel> Organizadores { get; set; }

        public DbSet<LugarModel> Lugares { get; set; }

        public DbSet<EventoModel> Eventos { get; set; }

        public DbSet<ClienteModel> Clientes { get; set; }

        public DbSet<ReservaModel> Reservas { get; set; }
    }
}
