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
    public class MusicoService : IMusicoService
    {
        private readonly IMusicoRepository _musicoRepository;
        public MusicoService(IMusicoRepository musicoRepository)
        {
            _musicoRepository = musicoRepository;
        }
        public async Task<MusicoModel> CreateAsync(MusicoModel musicoModel)
        {
            return await _musicoRepository.CreateAsync(musicoModel);
        }

        public async Task DeleteAsync(int id)
        {
            await _musicoRepository.DeleteAsync(id);
        }

        public async Task<MusicoModel> EditAsync(MusicoModel musicoModel)
        {
            return await _musicoRepository.EditAsync(musicoModel);
        }

        public async Task<IEnumerable<MusicoModel>> GetAllAsync(bool orderAscendant, string search = null)
        {
            return await _musicoRepository.GetAllAsync(orderAscendant, search);
        }

        public async Task<MusicoModel> GetByIdAsync(int id)
        {
            return await _musicoRepository.GetByIdAsync(id);
        }
    }
}
