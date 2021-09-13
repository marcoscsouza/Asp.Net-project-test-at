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

        [HttpGet("{orderAscendat:bool}/{search?}")]
        public async Task<ActionResult<IEnumerable<BandaModel>>> Get(
            bool orderAscendat, 
            string search = null) /* ajustar parametros de filtro */
        {
            var bandas = await _bandaService.GetAllAsync(orderAscendat, search);

            return Ok(bandas);
        }

        [HttpGet("{id:int}")]
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

        [HttpPut("{id:int}")]
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
                var editarBanda = await _bandaService.EditAsync(bandaModel);
                return Ok(editarBanda);
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(409);
            }
            
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            await _bandaService.DeleteAsync(id);

            return Ok();
        }

        [HttpGet("IsNomeValid/{nome}/{id}")]
        public async Task<IActionResult> IsNomeValid(string nome, int id)
        {
            var isValid = await _bandaService.IsNomeValidAsync(nome, id);

            return Ok(isValid);
        }
    }
}
