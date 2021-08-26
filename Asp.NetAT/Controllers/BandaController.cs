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
    public class BandaController : Controller
    {
        private readonly AspNetATContext _context;

        public BandaController(AspNetATContext context)
        {
            _context = context;
        }

        // GET: Banda
        public async Task<IActionResult> Index()
        {
            return View(await _context.BandaModel.ToListAsync());
        }

        // GET: Banda/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bandaModel = await _context.BandaModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bandaModel == null)
            {
                return NotFound();
            }

            return View(bandaModel);
        }

        // GET: Banda/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Banda/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,InicioBanda,GeneroMusical,Nacionalidade,FazendoShow")] BandaModel bandaModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bandaModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bandaModel);
        }

        // GET: Banda/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bandaModel = await _context.BandaModel.FindAsync(id);
            if (bandaModel == null)
            {
                return NotFound();
            }
            return View(bandaModel);
        }

        // POST: Banda/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,InicioBanda,GeneroMusical,Nacionalidade,FazendoShow")] BandaModel bandaModel)
        {
            if (id != bandaModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bandaModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BandaModelExists(bandaModel.Id))
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
            return View(bandaModel);
        }

        // GET: Banda/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bandaModel = await _context.BandaModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bandaModel == null)
            {
                return NotFound();
            }

            return View(bandaModel);
        }

        // POST: Banda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bandaModel = await _context.BandaModel.FindAsync(id);
            _context.BandaModel.Remove(bandaModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BandaModelExists(int id)
        {
            return _context.BandaModel.Any(e => e.Id == id);
        }
    }
}
