using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaGestionEventos.Data;
using SistemaGestionEventos.Models;

namespace SistemaGestionEventos.Controllers
{
    public class EventoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Evento
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Eventos
                .Include(p => p.Lugar)
                .Include(p => p.Organizador);

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Evento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var EventoModel = await _context.Eventos
                .Include(p => p.Lugar)
                .Include(p => p.Organizador)
                .FirstOrDefaultAsync(m => m.EventoId == id);

            if (EventoModel == null)
            {
                return NotFound();
            }

            return View(EventoModel);
        }

        // GET: Evento/Create
        public IActionResult Create()
        {
            ViewData["LugarId"] = new SelectList(_context.Lugares, "LugarId", "Nombre");

            ViewData["OrganizadorId"] = new SelectList(
                _context.Organizadores
                    .Select(p => new
                    {
                        p.OrganizadorId,
                        Texto = p.Nombre + " - " + p.Especialidad
                    }),
                "OrganizadorId",
                "Texto"
            );

            return View();
        }

        // POST: Evento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventoId,Titulo,FechaEvento,LugarId,OrganizadorId")] EventoModel EventoModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(EventoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["LugarId"] = new SelectList(_context.Lugares, "LugarId", "Nombre", EventoModel.LugarId);

            ViewData["OrganizadorId"] = new SelectList(
                _context.Organizadores
                    .Select(p => new
                    {
                        p.OrganizadorId,
                        Texto = p.Nombre + " - " + p.Especialidad
                    }),
                "OrganizadorId",
                "Texto",
                EventoModel.OrganizadorId
            );

            return View(EventoModel);
        }

        // GET: Evento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var EventoModel = await _context.Eventos.FindAsync(id);

            if (EventoModel == null)
            {
                return NotFound();
            }

            ViewData["LugarId"] = new SelectList(_context.Lugares, "LugarId", "Nombre", EventoModel.LugarId);

            ViewData["OrganizadorId"] = new SelectList(
                _context.Organizadores
                    .Select(p => new
                    {
                        p.OrganizadorId,
                        Texto = p.Nombre + " - " + p.Especialidad
                    }),
                "OrganizadorId",
                "Texto",
                EventoModel.OrganizadorId
            );

            return View(EventoModel);
        }

        // POST: Evento/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventoId,Titulo,FechaEvento,LugarId,OrganizadorId")] EventoModel EventoModel)
        {
            if (id != EventoModel.EventoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(EventoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoModelExists(EventoModel.EventoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["LugarId"] = new SelectList(_context.Lugares, "LugarId", "Nombre", EventoModel.LugarId);

            ViewData["OrganizadorId"] = new SelectList(
                _context.Organizadores
                    .Select(p => new
                    {
                        p.OrganizadorId,
                        Texto = p.Nombre + " - " + p.Especialidad
                    }),
                "OrganizadorId",
                "Texto",
                EventoModel.OrganizadorId
            );

            return View(EventoModel);
        }

        // GET: Evento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var EventoModel = await _context.Eventos
                .Include(p => p.Lugar)
                .Include(p => p.Organizador)
                .FirstOrDefaultAsync(m => m.EventoId == id);

            if (EventoModel == null)
            {
                return NotFound();
            }

            return View(EventoModel);
        }

        // POST: Evento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var EventoModel = await _context.Eventos.FindAsync(id);

            if (EventoModel != null)
            {
                _context.Eventos.Remove(EventoModel);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool EventoModelExists(int id)
        {
            return _context.Eventos.Any(e => e.EventoId == id);
        }
    }
}
