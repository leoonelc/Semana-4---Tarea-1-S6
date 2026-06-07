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
    public class LugarController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LugarController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Lugar
        public async Task<IActionResult> Index()
        {
            return View(await _context.Lugares.ToListAsync());
        }

        // GET: Lugar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var LugarModel = await _context.Lugares
                .FirstOrDefaultAsync(m => m.LugarId == id);
            if (LugarModel == null)
            {
                return NotFound();
            }

            return View(LugarModel);
        }

        // GET: Lugar/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lugar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LugarId,Nombre,Ciudad,Capacidad,Tipo")] LugarModel LugarModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(LugarModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(LugarModel);
        }

        // GET: Lugar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var LugarModel = await _context.Lugares.FindAsync(id);
            if (LugarModel == null)
            {
                return NotFound();
            }
            return View(LugarModel);
        }

        // POST: Lugar/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LugarId,Nombre,Ciudad,Capacidad,Tipo")] LugarModel LugarModel)
        {
            if (id != LugarModel.LugarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(LugarModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LugarModelExists(LugarModel.LugarId))
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
            return View(LugarModel);
        }

        // GET: Lugar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var LugarModel = await _context.Lugares
                .FirstOrDefaultAsync(m => m.LugarId == id);
            if (LugarModel == null)
            {
                return NotFound();
            }

            return View(LugarModel);
        }

        // POST: Lugar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var LugarModel = await _context.Lugares.FindAsync(id);
            if (LugarModel != null)
            {
                _context.Lugares.Remove(LugarModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LugarModelExists(int id)
        {
            return _context.Lugares.Any(e => e.LugarId == id);
        }
    }
}

