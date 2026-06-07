using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaGestionEventos.Data;
using SistemaGestionEventos.Models;

namespace SistemaGestionEventos.Controllers
{
    public class ReservaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reserva
        public async Task<IActionResult> Index()
        {
            var Reservas = _context.Reservas
                .Include(v => v.Cliente)
                .Include(v => v.Evento);

            return View(await Reservas.ToListAsync());
        }

        // GET: Reserva/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var ReservaModel = await _context.Reservas
                .Include(v => v.Cliente)
                .Include(v => v.Evento)
                .FirstOrDefaultAsync(m => m.ReservaId == id);

            if (ReservaModel == null) return NotFound();

            return View(ReservaModel);
        }

        // GET: Reserva/Create
        public IActionResult Create()
        {
            var Reserva = new ReservaModel
            {
                FechaReserva = DateTime.Now,
                Estado = "Pendiente"
            };

            ViewData["ClienteId"] = new SelectList(
                _context.Clientes.Select(c => new
                {
                    c.ClienteId,
                    Texto = c.Nombre + " - " + c.Telefono + " - " + c.Correo
                }),
                "ClienteId",
                "Texto"
            );

            ViewData["EventoId"] = new SelectList(
                _context.Eventos
                    .Include(p => p.Lugar)
                    .Select(p => new
                    {
                        p.EventoId,
                        Texto = p.Titulo + " - " + p.FechaEvento.ToString("dd/MM/yyyy") + " - " + (p.Lugar != null ? p.Lugar.Nombre : "")
                    }),
                "EventoId",
                "Texto"
            );

            return View(Reserva);
        }

        // POST: Reserva/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReservaModel ReservaModel)
        {
            if (!ModelState.IsValid)
            {
                RecargarCombos(ReservaModel);
                return View(ReservaModel);
            }

            _context.Add(ReservaModel);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Reserva/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var ReservaModel = await _context.Reservas.FindAsync(id);
            if (ReservaModel == null) return NotFound();

            RecargarCombos(ReservaModel);

            return View(ReservaModel);
        }

        // POST: Reserva/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ReservaModel ReservaModel)
        {
            if (id != ReservaModel.ReservaId) return NotFound();

            if (!ModelState.IsValid)
            {
                RecargarCombos(ReservaModel);
                return View(ReservaModel);
            }

            _context.Update(ReservaModel);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Reserva/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var ReservaModel = await _context.Reservas
                .Include(v => v.Cliente)
                .Include(v => v.Evento)
                .FirstOrDefaultAsync(m => m.ReservaId == id);

            if (ReservaModel == null) return NotFound();

            return View(ReservaModel);
        }

        // POST: Reserva/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ReservaModel = await _context.Reservas.FindAsync(id);

            if (ReservaModel != null)
            {
                _context.Reservas.Remove(ReservaModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private void RecargarCombos(ReservaModel ReservaModel)
        {
            ViewData["ClienteId"] = new SelectList(
                _context.Clientes.Select(c => new
                {
                    c.ClienteId,
                    Texto = c.Nombre + " - " + c.Telefono + " - " + c.Correo
                }),
                "ClienteId",
                "Texto",
                ReservaModel.ClienteId
            );

            ViewData["EventoId"] = new SelectList(
                _context.Eventos
                    .Include(p => p.Lugar)
                    .Select(p => new
                    {
                        p.EventoId,
                        Texto = p.Titulo + " - " + p.FechaEvento.ToString("dd/MM/yyyy")
                    }),
                "EventoId",
                "Texto",
                ReservaModel.EventoId
            );
        }

        private bool ReservaModelExists(int id)
        {
            return _context.Reservas.Any(e => e.ReservaId == id);
        }
    }
}
