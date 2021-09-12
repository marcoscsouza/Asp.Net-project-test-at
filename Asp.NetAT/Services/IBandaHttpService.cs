using Asp.NetAT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetAT.Services
{
    public interface IBandaHttpService
    {
        Task<IEnumerable<BandaViewModel>> GetAllAsync(
            bool orderAscendat,
            string search = null);
        Task<BandaViewModel> GetByIdAsync(int id);
        Task<BandaViewModel> CreateAsync(BandaViewModel bandaViewModel);
        Task<BandaViewModel> EditAsync(BandaViewModel bandaViewModel);
        Task DeleteAsync(int id);
        Task<bool> IsNomeValidAsync(string nome, int id);
    }
}
