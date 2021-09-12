using Asp.NetAT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetAT.Services
{
    public interface IMusicoHttpService
    {
        Task<IEnumerable<MusicoViewModel>> GetAllAsync(
            bool orderAscendat,
            string search = null);
        Task<MusicoViewModel> GetByIdAsync(int id);
        Task<MusicoViewModel> CreateAsync(MusicoViewModel musicoViewModel);
        Task<MusicoViewModel> EditAsync(MusicoViewModel musicoViewModel);
        Task DeleteAsync(int id);
    }
}
