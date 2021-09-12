using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Asp.NetAT.Models;
using Microsoft.AspNetCore.Authorization;
using Asp.NetAT.Services;

namespace Asp.NetAT.Controllers
{
    [Authorize] //Identity
    public class MusicoController : Controller
    {
        
        private readonly IMusicoHttpService _musicoHttpService;
        private readonly IBandaHttpService _bandaHttpService;

        public MusicoController(IMusicoHttpService musicoHttpService, 
                                IBandaHttpService bandaHttpService)
        {
            
            _musicoHttpService = musicoHttpService;
            _bandaHttpService = bandaHttpService;
        }

        // GET: Musico
        public async Task<IActionResult> Index(MusicoIndexViewModel musicoIndexRequest)
        {   //sem usar o musicoIndexViewModel
            /*var lista = await _musicoHttpService.GetAllAsync(true, null);*/
            
            /*var aspNetATContext = _context.MusicoViewModel.Include(m => m.Banda);*/

            var musicoIndexViewModel = new MusicoIndexViewModel
            {
                Search = musicoIndexRequest.Search,
                OrderAscendant = musicoIndexRequest.OrderAscendant,
                Musicos = await _musicoHttpService.GetAllAsync(
                    musicoIndexRequest.OrderAscendant,
                    musicoIndexRequest.Search)
            };

            await PreencherSelectBandas();

            return View(musicoIndexViewModel);
        }

        // GET: Musico/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musicoViewModel = await _musicoHttpService.GetByIdAsync(id.Value);

            if (musicoViewModel == null)
            {
                return NotFound();
            }


            return View(musicoViewModel);
        }

        // GET: Musico/Create
        public async Task<IActionResult> CreateAsync()
        {
            await PreencherSelectBandas();

            return View();
        }

        // POST: Musico/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( MusicoViewModel musicoViewModel)
        {
            if (!ModelState.IsValid)
            {
                await PreencherSelectBandas(musicoViewModel.BandaId);

                return View(musicoViewModel);
            }


            var bandaCriada = await _musicoHttpService.CreateAsync(musicoViewModel);
            
            return RedirectToAction(nameof(Details), new { id = bandaCriada.Id });
        }

        // GET: Musico/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musicoViewModel = await _musicoHttpService.GetByIdAsync(id.Value);
            if (musicoViewModel == null)
            {
                return NotFound();
            }
            /*ViewData["BandaId"] = new SelectList(_context.BandaModel, "Id", "Id", musicoViewModel.BandaId);*/
            await PreencherSelectBandas(musicoViewModel.BandaId);


            return View(musicoViewModel);
        }

        // POST: Musico/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MusicoViewModel musicoViewModel)
        {
            if (id != musicoViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                await PreencherSelectBandas(musicoViewModel.BandaId);
                return View(musicoViewModel);
            }

            try
            {
                await _musicoHttpService.EditAsync(musicoViewModel);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(await MusicoViewModelExistsAsync(musicoViewModel.Id)))
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

        // GET: Musico/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musicoViewModel = await _musicoHttpService.GetByIdAsync(id.Value);

            if (musicoViewModel == null)
            {
                return NotFound();
            }

            return View(musicoViewModel);
        }

        // POST: Musico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _musicoHttpService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }


        private async Task<bool> MusicoViewModelExistsAsync(int id)
        {
            var banda = await _musicoHttpService.GetByIdAsync(id);

            var any = banda != null;

            return any;
        }

        private async Task PreencherSelectBandas(int? bandaId = null)
        {
            var bandas = await _bandaHttpService.GetAllAsync(true);

            ViewBag.Bandas = new SelectList(bandas,
                nameof(BandaViewModel.Id),
                nameof(BandaViewModel.Nome),
                bandaId);
        }
    }
}
