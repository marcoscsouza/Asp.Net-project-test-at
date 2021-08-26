using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.Data;
using Domain.Model.Models;

namespace Asp.NetAT.Controllers
{
    public class MusicoController : Controller
    {
        private readonly AspNetATContext _context;

        public MusicoController(AspNetATContext context)
        {
            _context = context;
        }

        // GET: Musico
        public async Task<IActionResult> Index()
        {
            var aspNetATContext = _context.MusicoModel.Include(m => m.Banda);
            return View(await aspNetATContext.ToListAsync());
        }

        // GET: Musico/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musicoModel = await _context.MusicoModel
                .Include(m => m.Banda)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (musicoModel == null)
            {
                return NotFound();
            }

            return View(musicoModel);
        }

        // GET: Musico/Create
        public IActionResult Create()
        {
            ViewData["BandaId"] = new SelectList(_context.BandaModel, "Id", "Id");
            return View();
        }

        // POST: Musico/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(/*[Bind("Id,Nome,SobreNome,Nascimento,BandaId")]*/ MusicoModel musicoModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(musicoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BandaId"] = new SelectList(_context.BandaModel, "Id", "Id", musicoModel.BandaId);
            return View(musicoModel);
        }

        // GET: Musico/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musicoModel = await _context.MusicoModel.FindAsync(id);
            if (musicoModel == null)
            {
                return NotFound();
            }
            ViewData["BandaId"] = new SelectList(_context.BandaModel, "Id", "Id", musicoModel.BandaId);
            return View(musicoModel);
        }

        // POST: Musico/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, /*[Bind("Id,Nome,SobreNome,Nascimento,BandaId")]*/ MusicoModel musicoModel)
        {
            if (id != musicoModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(musicoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MusicoModelExists(musicoModel.Id))
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
            ViewData["BandaId"] = new SelectList(_context.BandaModel, "Id", "Id", musicoModel.BandaId);
            return View(musicoModel);
        }

        // GET: Musico/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musicoModel = await _context.MusicoModel
                .Include(m => m.Banda)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (musicoModel == null)
            {
                return NotFound();
            }

            return View(musicoModel);
        }

        // POST: Musico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var musicoModel = await _context.MusicoModel.FindAsync(id);
            _context.MusicoModel.Remove(musicoModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MusicoModelExists(int id)
        {
            return _context.MusicoModel.Any(e => e.Id == id);
        }
    }
}
