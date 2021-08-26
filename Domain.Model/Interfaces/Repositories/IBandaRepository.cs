using Domain.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Interfaces.Repositories
{
    public interface IBandaRepository
    {
        Task<IEnumerable<BandaModel>> GetAllAsync(
            bool orderAscendat,
            string search = null);
        Task<BandaModel> GetByIdAsync(int id);
        Task<BandaModel> CreateAsync(BandaModel bandaModel);
        Task<BandaModel> EditAsync(BandaModel bandaModel);
        Task DeleteAsync(int id);
        Task<BandaModel> GetNomeNotFromThisIdAsync(string nome, int id);
    }
}
