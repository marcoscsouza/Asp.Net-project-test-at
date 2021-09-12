using Asp.NetAT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetAT.Services.Implementations
{
    public class MusicoHttpService : IMusicoHttpService
    {
        public Task<IEnumerable<MusicoViewModel>> GetAllAsync(bool orderAscendat, string search = null)
        {
            throw new NotImplementedException();
        }

        public Task<MusicoViewModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<MusicoViewModel> CreateAsync(MusicoViewModel musicoViewModel)
        {
            throw new NotImplementedException();
        }

        public Task<MusicoViewModel> EditAsync(MusicoViewModel musicoViewModel)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
