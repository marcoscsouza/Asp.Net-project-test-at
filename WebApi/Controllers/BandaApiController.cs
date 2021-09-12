using Domain.Model.Interfaces.Services;
using Domain.Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BandaApiController : ControllerBase
    {
        private readonly IBandaService _bandaService;

        public BandaApiController(IBandaService bandaService)
        {
            _bandaService = bandaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BandaModel>>> Get() /* ajustar parametros de filtro */
        {
            var bandas = await _bandaService.GetAllAsync(orderAscendat: true);

            return Ok(bandas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BandaModel>> Get(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var bandaModel = await _bandaService.GetByIdAsync(id);

            if (bandaModel == null)
            {
                return NotFound();
            }

            return Ok(bandaModel);
        }

        [HttpPost]
        public async Task<ActionResult<BandaModel>> Post([FromBody] BandaModel bandaModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(bandaModel);
            }

            var criarBanda = await _bandaService.CreateAsync(bandaModel);

            return Ok(criarBanda);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BandaModel>> Put(int id, [FromBody] BandaModel bandaModel)
        {
            if (id != bandaModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(bandaModel);
            }

            try
            {
                await _bandaService.EditAsync(bandaModel);
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(409);
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            await _bandaService.DeleteAsync(id);

            return Ok();
        }
    }
}
