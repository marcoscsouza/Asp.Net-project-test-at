using Data.Data;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class BandaRepository : IBandaRepository
    {
        private readonly AspNetATContext _aspNetATContext;
        public BandaRepository(
            AspNetATContext aspNetATContext)
        {
            _aspNetATContext = aspNetATContext;
        }

        public async Task<BandaModel> CreateAsync(BandaModel bandaModel)
        {
            var banda = _aspNetATContext.BandaModel.Add(bandaModel);

            await _aspNetATContext.SaveChangesAsync();

            return banda.Entity;
        }

        public async Task DeleteAsync(int id)
        {
            var banda = await GetByIdAsync(id);

            _aspNetATContext.BandaModel.Remove(banda);

            await _aspNetATContext.SaveChangesAsync();
        }

        public async Task<BandaModel> EditAsync(BandaModel bandaModel)
        {
            var banda = _aspNetATContext.BandaModel.Update(bandaModel);

            await _aspNetATContext.SaveChangesAsync();

            return banda.Entity;
        }

        public async Task<IEnumerable<BandaModel>> GetAllAsync(bool orderAscendat, string search = null)
        {
            var bandas = orderAscendat
                ? _aspNetATContext.BandaModel.OrderBy(x => x.Nome)
                : _aspNetATContext.BandaModel.OrderByDescending(x => x.Nome);

            if (string.IsNullOrWhiteSpace(search))
            {
                return await _aspNetATContext.BandaModel.ToListAsync();
            }

            return await bandas
                .Where(x => x.Nome.Contains(search))
                .ToListAsync();
        }

        public async Task<BandaModel> GetByIdAsync(int id)
        {
            var banda = await _aspNetATContext
                .BandaModel
                .Include(x => x.Musicos)
                .FirstOrDefaultAsync(x => x.Id == id);

            return banda;
        }

        //TODO: Fazer depois de realizar o teste do 2º commit
        public async Task<BandaModel> GetNomeNotFromThisIdAsync(string nome, int id)
        {
            var bandaModel = await _aspNetATContext
                .BandaModel
                .FirstOrDefaultAsync(x => x.Nome == nome && x.Id != id);

            return bandaModel;
        }
    }
}
