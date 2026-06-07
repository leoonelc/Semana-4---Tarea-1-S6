using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaGestionEventos.Data;
using SistemaGestionEventos.Models;

namespace SistemaGestionEventos.Controllers
{
    public class OrganizadorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrganizadorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Organizador
        public async Task<IActionResult> Index()
        {
            return View(await _context.Organizadores.ToListAsync());
        }

        // GET: Organizador/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var OrganizadorModel = await _context.Organizadores
                .FirstOrDefaultAsync(m => m.OrganizadorId == id);

            if (OrganizadorModel == null)
            {
                return NotFound();
            }

            return View(OrganizadorModel);
        }

        // GET: Organizador/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Organizador/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrganizadorId,Nombre,Especialidad,Telefono,Activo")] OrganizadorModel OrganizadorModel)
        {
            if (!ModelState.IsValid)
            {
                return View(OrganizadorModel);
            }

            _context.Add(OrganizadorModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Organizador/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var OrganizadorModel = await _context.Organizadores.FindAsync(id);

            if (OrganizadorModel == null)
            {
                return NotFound();
            }

            return View(OrganizadorModel);
        }

        // POST: Organizador/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrganizadorId,Nombre,Especialidad,Telefono,Activo")] OrganizadorModel OrganizadorModel)
        {
            if (id != OrganizadorModel.OrganizadorId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(OrganizadorModel);
            }

            try
            {
                _context.Update(OrganizadorModel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganizadorModelExists(OrganizadorModel.OrganizadorId))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Organizador/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var OrganizadorModel = await _context.Organizadores
                .FirstOrDefaultAsync(m => m.OrganizadorId == id);

            if (OrganizadorModel == null)
            {
                return NotFound();
            }

            return View(OrganizadorModel);
        }

        // POST: Organizador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var OrganizadorModel = await _context.Organizadores.FindAsync(id);

            if (OrganizadorModel != null)
            {
                _context.Organizadores.Remove(OrganizadorModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool OrganizadorModelExists(int id)
        {
            return _context.Organizadores.Any(e => e.OrganizadorId == id);
        }
    }
}
