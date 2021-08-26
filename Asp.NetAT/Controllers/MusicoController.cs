using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.Data;
using Domain.Model.Models;
using Domain.Model.Interfaces.Services;
using Asp.NetAT.Models;

namespace Asp.NetAT.Controllers
{
    public class MusicoController : Controller
    {
        
        private readonly IMusicoService _musicoService;
        private readonly IBandaService _bandaService;

        public MusicoController(IMusicoService musicoService, 
                                IBandaService bandaService)
        {
            
            _musicoService = musicoService;
            _bandaService = bandaService;
        }

        // GET: Musico
        public async Task<IActionResult> Index(MusicoIndexViewModel musicoIndexRequest)
        {   //sem usar o musicoIndexViewModel
            /*var lista = await _musicoService.GetAllAsync(true, null);*/
            
            /*var aspNetATContext = _context.MusicoModel.Include(m => m.Banda);*/

            var musicoIndexViewModel = new MusicoIndexViewModel
            {
                Search = musicoIndexRequest.Search,
                OrderAscendant = musicoIndexRequest.OrderAscendant,
                Musicos = await _musicoService.GetAllAsync(
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

            var musicoModel = await _musicoService.GetByIdAsync(id.Value);

            if (musicoModel == null)
            {
                return NotFound();
            }

            var musicoViewModel = MusicoViewModel.From(musicoModel);

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
        public async Task<IActionResult> Create(/*[Bind("Id,Nome,SobreNome,Nascimento,BandaId")]*/ MusicoViewModel musicoViewModel)
        {
            if (!ModelState.IsValid)
            {
                await PreencherSelectBandas(musicoViewModel.BandaId);

                return View(musicoViewModel);
            }

            var musicoModel = musicoViewModel.ToModel();

            var bandaCriada = await _musicoService.CreateAsync(musicoModel);
            
            return RedirectToAction(nameof(Details), new { id = bandaCriada.Id });
        }

        // GET: Musico/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musicoModel = await _musicoService.GetByIdAsync(id.Value);
            if (musicoModel == null)
            {
                return NotFound();
            }
            /*ViewData["BandaId"] = new SelectList(_context.BandaModel, "Id", "Id", musicoModel.BandaId);*/
            await PreencherSelectBandas(musicoModel.BandaId);

            var musicoViewModel = MusicoViewModel.From(musicoModel);

            return View(musicoViewModel);
        }

        // POST: Musico/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, /*[Bind("Id,Nome,SobreNome,Nascimento,BandaId")]*/ MusicoViewModel musicoViewModel)
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

            var musicoModel = musicoViewModel.ToModel();
            try
            {
                await _musicoService.EditAsync(musicoModel);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(await MusicoModelExistsAsync(musicoModel.Id)))
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

            var musicoModel = await _musicoService.GetByIdAsync(id.Value);

            if (musicoModel == null)
            {
                return NotFound();
            }

            var musicoViewModel = MusicoViewModel.From(musicoModel);
            return View(musicoViewModel);
        }

        // POST: Musico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _musicoService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }


        private async Task<bool> MusicoModelExistsAsync(int id)
        {
            var banda = await _musicoService.GetByIdAsync(id);

            var any = banda != null;

            return any;
        }

        private async Task PreencherSelectBandas(int? bandaId = null)
        {
            var bandas = await _bandaService.GetAllAsync(true);

            ViewBag.Bandas = new SelectList(bandas,
                nameof(BandaModel.Id),
                nameof(BandaModel.Nome),
                bandaId);
        }
    }
}
