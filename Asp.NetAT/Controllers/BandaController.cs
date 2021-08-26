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
    public class BandaController : Controller
    {
        private readonly IBandaService _bandaService;

        public BandaController(IBandaService bandaService)
        {
            _bandaService = bandaService;
        }

        // GET: Banda
        //TODO: make to IndexViewModel
        public async Task<IActionResult> Index(BandaIndexViewModel bandaIndexRequest)
        {   //sem usar o BandaIndexViewModel
            /*var lista = await _bandaService.GetAllAsync(true, null);*/

            var bandaIndexViewModel = new BandaIndexViewModel
            {
                Search = bandaIndexRequest.Search,
                OrderAscendant = bandaIndexRequest.OrderAscendant,
                Bandas = await _bandaService.GetAllAsync(bandaIndexRequest.OrderAscendant, bandaIndexRequest.Search)
            };
            return View(bandaIndexViewModel);
        }

        // GET: Banda/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bandaModel = await _bandaService.GetByIdAsync(id.Value);

            if (bandaModel == null)
            {
                return NotFound();
            }

            var bandaViewModel = BandaViewModel.From(bandaModel);

            return View(bandaViewModel);
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
        public async Task<IActionResult> Create(/*[Bind("Id,Nome,InicioBanda,GeneroMusical,Nacionalidade,FazendoShow")]*/ BandaViewModel bandaViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(bandaViewModel);
            }

            var bandaModel = bandaViewModel.ToModel();

            var criarBanda = await _bandaService.CreateAsync(bandaModel);
            
            return RedirectToAction(nameof(Details), new { id = criarBanda.Id });
        }

        // GET: Banda/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bandaModel = await _bandaService.GetByIdAsync(id.Value);
            if (bandaModel == null)
            {
                return NotFound();
            }

            var bandaViewModel = BandaViewModel.From(bandaModel);
            return View(bandaViewModel);
        }

        // POST: Banda/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, /*[Bind("Id,Nome,InicioBanda,GeneroMusical,Nacionalidade,FazendoShow")]*/ BandaViewModel bandaViewModel)
        {
            if (id != bandaViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(bandaViewModel);
            }

            var bandaModel = bandaViewModel.ToModel();
            try
            {
                await _bandaService.EditAsync(bandaModel);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!( await BandaModelExistsAsync(bandaModel.Id)))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Details), new { id = bandaModel.Id });
        }

        // GET: Banda/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bandaModel = await _bandaService.GetByIdAsync(id.Value);

            if (bandaModel == null)
            {
                return NotFound();
            }

            var bandaViewModel = BandaViewModel.From(bandaModel);

            return View(bandaViewModel);
        }

        // POST: Banda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bandaService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> BandaModelExistsAsync(int id)
        {
            var banda = await _bandaService.GetByIdAsync(id);

            var any = banda != null;
            return any;
        }
    }
}
