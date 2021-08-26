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
    public class MusicoRepository : IMusicoRepository
    {
        private readonly AspNetATContext _aspNetATContext;
        public MusicoRepository(AspNetATContext aspNetATContext)
        {
            _aspNetATContext = aspNetATContext;
        }
        public async Task<MusicoModel> CreateAsync(MusicoModel musicoModel)
        {
            var musico = _aspNetATContext.MusicoModel.Add(musicoModel);

            await _aspNetATContext.SaveChangesAsync();

            return musico.Entity;
        }

        public async Task DeleteAsync(int id)
        {
            var musico = await GetByIdAsync(id);

            _aspNetATContext.MusicoModel.Remove(musico);

            await _aspNetATContext.SaveChangesAsync();
        }

        public async Task<MusicoModel> EditAsync(MusicoModel musicoModel)
        {
            var musico = _aspNetATContext.MusicoModel.Update(musicoModel);

            await _aspNetATContext.SaveChangesAsync();

            return musico.Entity;
        }

        public async Task<IEnumerable<MusicoModel>> GetAllAsync(bool orderAscendat, string search = null)
        {
            var musicos = orderAscendat
               ? _aspNetATContext.MusicoModel.OrderBy(x => x.Nome)
               : _aspNetATContext.MusicoModel.OrderByDescending(x => x.Nome);

            if (string.IsNullOrWhiteSpace(search))
            {
                return await _aspNetATContext.MusicoModel.ToListAsync();
            }

            return await musicos
                .Where(x => x.Nome.Contains(search))
                .ToListAsync();

        }

        public async Task<MusicoModel> GetByIdAsync(int id)
        {
            var musico = await _aspNetATContext
                .MusicoModel
                .Include(x => x.Banda)
                .FirstOrDefaultAsync(x => x.Id == id);

            return musico;
        }
    }
}
