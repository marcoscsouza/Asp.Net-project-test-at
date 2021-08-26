using Domain.Model.Interfaces.Repositories;
using Domain.Model.Interfaces.Services;
using Domain.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Services
{
    public class BandaService : IBandaService
    {
        private readonly IBandaRepository _bandaRepository;
        public BandaService(IBandaRepository bandaRepository)
        {
            _bandaRepository = bandaRepository;
        }

        public async Task<BandaModel> CreateAsync(BandaModel bandaModel)
        {
            return await _bandaRepository.CreateAsync(bandaModel);
        }

        public async Task DeleteAsync(int id)
        {
            await _bandaRepository.DeleteAsync(id);
        }

        public async Task<BandaModel> EditAsync(BandaModel bandaModel)
        {
            return await _bandaRepository.EditAsync(bandaModel);
        }

        public async Task<IEnumerable<BandaModel>> GetAllAsync(bool orderAscendant, string search = null)
        {
            return await _bandaRepository.GetAllAsync(orderAscendant, search);
        }

        public async Task<BandaModel> GetByIdAsync(int id)
        {
            return await _bandaRepository.GetByIdAsync(id);
        }

        //TODO: depois do teste para realizar 2º commit
        public Task<bool> IsNomeValidAsync(string nome, int id)
        {
            throw new NotImplementedException();
        }
    }
}
