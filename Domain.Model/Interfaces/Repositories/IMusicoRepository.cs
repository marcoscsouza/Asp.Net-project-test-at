using Domain.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Interfaces.Repositories
{
    public interface IMusicoRepository
    {
        Task<IEnumerable<MusicoModel>> GetAllAsync(
            bool orderAscendat,
            string search = null);
        Task<MusicoModel> GetByIdAsync(int id);
        Task<MusicoModel> CreateAsync(MusicoModel musicoModel);
        Task<MusicoModel> EditAsync(MusicoModel musicoModel);
        Task DeleteAsync(int id);
    }
}
 