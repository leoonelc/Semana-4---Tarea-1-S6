using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SistemaGestionEventos.Data;
using SistemaGestionEventos.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();

    if (!context.Reservas.Any())
    {
        var lugares = new List<LugarModel>
        {
            new() { Nombre = "Salon Principal", Ciudad = "Santo Domingo", Capacidad = 120, Tipo = "Salon" },
            new() { Nombre = "Auditorio Central", Ciudad = "Quito", Capacidad = 200, Tipo = "Auditorio" },
            new() { Nombre = "Terraza Garden", Ciudad = "Guayaquil", Capacidad = 80, Tipo = "Terraza" },
            new() { Nombre = "Sala Ejecutiva", Ciudad = "Santo Domingo", Capacidad = 45, Tipo = "Sala" },
            new() { Nombre = "Centro Cultural", Ciudad = "Manta", Capacidad = 150, Tipo = "Centro" }
        };

        var organizadores = new List<OrganizadorModel>
        {
            new() { Nombre = "Andrea Molina", Especialidad = "Eventos sociales", Telefono = "0991112223", Activo = true },
            new() { Nombre = "Carlos Zambrano", Especialidad = "Conferencias", Telefono = "0982223344", Activo = true },
            new() { Nombre = "Daniela Perez", Especialidad = "Capacitaciones", Telefono = "0973334455", Activo = true },
            new() { Nombre = "Luis Cedeno", Especialidad = "Ferias", Telefono = "0964445566", Activo = true },
            new() { Nombre = "Maria Torres", Especialidad = "Eventos academicos", Telefono = "0955556677", Activo = true }
        };

        var clientes = new List<ClienteModel>
        {
            new() { Nombre = "Juan Lopez", Telefono = "0991000001", Correo = "juan.lopez@example.com", FechaRegistro = DateTime.Today.AddDays(-30) },
            new() { Nombre = "Sofia Ramirez", Telefono = "0991000002", Correo = "sofia.ramirez@example.com", FechaRegistro = DateTime.Today.AddDays(-29) },
            new() { Nombre = "Pedro Garcia", Telefono = "0991000003", Correo = "pedro.garcia@example.com", FechaRegistro = DateTime.Today.AddDays(-28) },
            new() { Nombre = "Camila Vera", Telefono = "0991000004", Correo = "camila.vera@example.com", FechaRegistro = DateTime.Today.AddDays(-27) },
            new() { Nombre = "Mateo Silva", Telefono = "0991000005", Correo = "mateo.silva@example.com", FechaRegistro = DateTime.Today.AddDays(-26) },
            new() { Nombre = "Valeria Rojas", Telefono = "0991000006", Correo = "valeria.rojas@example.com", FechaRegistro = DateTime.Today.AddDays(-25) },
            new() { Nombre = "Diego Castro", Telefono = "0991000007", Correo = "diego.castro@example.com", FechaRegistro = DateTime.Today.AddDays(-24) },
            new() { Nombre = "Paula Mendoza", Telefono = "0991000008", Correo = "paula.mendoza@example.com", FechaRegistro = DateTime.Today.AddDays(-23) },
            new() { Nombre = "Kevin Ortiz", Telefono = "0991000009", Correo = "kevin.ortiz@example.com", FechaRegistro = DateTime.Today.AddDays(-22) },
            new() { Nombre = "Nicole Herrera", Telefono = "0991000010", Correo = "nicole.herrera@example.com", FechaRegistro = DateTime.Today.AddDays(-21) }
        };

        context.Lugares.AddRange(lugares);
        context.Organizadores.AddRange(organizadores);
        context.Clientes.AddRange(clientes);
        context.SaveChanges();

        var eventos = new List<EventoModel>
        {
            new() { Titulo = "Conferencia de emprendimiento", FechaEvento = DateTime.Today.AddDays(5), LugarId = lugares[1].LugarId, OrganizadorId = organizadores[1].OrganizadorId },
            new() { Titulo = "Taller de liderazgo", FechaEvento = DateTime.Today.AddDays(8), LugarId = lugares[3].LugarId, OrganizadorId = organizadores[2].OrganizadorId },
            new() { Titulo = "Feria academica", FechaEvento = DateTime.Today.AddDays(12), LugarId = lugares[4].LugarId, OrganizadorId = organizadores[4].OrganizadorId },
            new() { Titulo = "Encuentro empresarial", FechaEvento = DateTime.Today.AddDays(15), LugarId = lugares[0].LugarId, OrganizadorId = organizadores[0].OrganizadorId },
            new() { Titulo = "Expo tecnologia", FechaEvento = DateTime.Today.AddDays(18), LugarId = lugares[2].LugarId, OrganizadorId = organizadores[3].OrganizadorId },
            new() { Titulo = "Seminario de marketing", FechaEvento = DateTime.Today.AddDays(22), LugarId = lugares[1].LugarId, OrganizadorId = organizadores[1].OrganizadorId }
        };

        context.Eventos.AddRange(eventos);
        context.SaveChanges();

        var estados = new[] { "Pendiente", "Confirmada", "Cancelada" };
        var reservas = Enumerable.Range(1, 30)
            .Select(i => new ReservaModel
            {
                FechaReserva = DateTime.Today.AddDays(-i),
                Estado = estados[i % estados.Length],
                ClienteId = clientes[(i - 1) % clientes.Count].ClienteId,
                EventoId = eventos[(i - 1) % eventos.Count].EventoId
            })
            .ToList();

        context.Reservas.AddRange(reservas);
        context.SaveChanges();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
