﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.Data;
using Domain.Model.Models;
using Domain.Model.Interfaces.Services;

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
        public async Task<IActionResult> Index(MusicoModel musicoModel)
        {
            var lista = await _musicoService.GetAllAsync(true, null);

            await PreencherSelectBandas(musicoModel.BandaId);

            /*var aspNetATContext = _context.MusicoModel.Include(m => m.Banda);*/

            return View(/*await aspNetATContext.ToListAsync()*/ lista);
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

            return View(musicoModel);
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
        public async Task<IActionResult> Create(/*[Bind("Id,Nome,SobreNome,Nascimento,BandaId")]*/ MusicoModel musicoModel)
        {
            if (!ModelState.IsValid)
            {
                await PreencherSelectBandas(musicoModel.BandaId);

                return View(musicoModel);
            }

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

            if (!ModelState.IsValid)
            {
                await PreencherSelectBandas(musicoModel.BandaId);
                return View(musicoModel);
            }

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

            return View(musicoModel);
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
