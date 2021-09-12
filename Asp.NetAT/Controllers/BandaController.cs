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
    public class BandaController : Controller
    {
        private readonly IBandaHttpService _bandaHttpService;


        public BandaController(IBandaHttpService bandaHttpService)
        {
            _bandaHttpService = bandaHttpService;
        }

        // GET: Banda
        
        public async Task<IActionResult> Index(BandaIndexViewModel bandaIndexRequest)
        {   //sem usar o BandaIndexViewModel
            /*var lista = await _bandaService.GetAllAsync(true, null);*/

            var bandaIndexViewModel = new BandaIndexViewModel
            {
                Search = bandaIndexRequest.Search,
                OrderAscendant = bandaIndexRequest.OrderAscendant,
                Bandas = await _bandaHttpService.GetAllAsync(bandaIndexRequest.OrderAscendant, bandaIndexRequest.Search)
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

            var bandaViewModel = await _bandaHttpService.GetByIdAsync(id.Value);

            if (bandaViewModel == null)
            {
                return NotFound();
            }


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
        public async Task<IActionResult> Create( BandaViewModel bandaViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(bandaViewModel);
            }


            var criarBanda = await _bandaHttpService.CreateAsync(bandaViewModel);
            
            return RedirectToAction(nameof(Details), new { id = criarBanda.Id });
        }

        // GET: Banda/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bandaViewModel = await _bandaHttpService.GetByIdAsync(id.Value);
            if (bandaViewModel == null)
            {
                return NotFound();
            }

            return View(bandaViewModel);
        }

        // POST: Banda/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BandaViewModel bandaViewModel)
        {
            if (id != bandaViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(bandaViewModel);
            }

            try
            {
                await _bandaHttpService.EditAsync(bandaViewModel);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!( await BandaModelExistsAsync(bandaViewModel.Id)))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Details), new { id = bandaViewModel.Id });
        }

        // GET: Banda/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bandaViewModel = await _bandaHttpService.GetByIdAsync(id.Value);

            if (bandaViewModel == null)
            {
                return NotFound();
            }


            return View(bandaViewModel);
        }

        // POST: Banda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bandaHttpService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> BandaModelExistsAsync(int id)
        {
            var banda = await _bandaHttpService.GetByIdAsync(id);

            var any = banda != null;
            return any;
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> IsNomeValid(string nome, int id)
        {
            return await _bandaHttpService.IsNomeValidAsync(nome, id)
                ? Json(true)
                : Json($"Nome {nome} já está sendo usado.");
        }
    }
}
