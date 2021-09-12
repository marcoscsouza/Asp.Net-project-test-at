using Asp.NetAT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetAT.Services.Implementations
{
    public class BandaHttpService : IBandaHttpService
    {
        public Task<IEnumerable<BandaViewModel>> GetAllAsync(bool orderAscendat, string search = null)
        {
            throw new NotImplementedException();
        }

        public Task<BandaViewModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BandaViewModel> CreateAsync(BandaViewModel bandaViewModel)
        {
            throw new NotImplementedException();
        }

        public Task<BandaViewModel> EditAsync(BandaViewModel bandaViewModel)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsNomeValidAsync(string nome, int id)
        {
            throw new NotImplementedException();
        }
    }
}
