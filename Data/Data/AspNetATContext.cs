using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Model.Models;

namespace Data.Data
{
    public class AspNetATContext : DbContext
    {
        public AspNetATContext (DbContextOptions<AspNetATContext> options)
            : base(options)
        {
        }


        //TODO: mudar os nomes para funcionar conforme a convenção ao inves de BandaModel ser Bandas e etc
        public DbSet<Domain.Model.Models.BandaModel> BandaModel { get; set; }

        public DbSet<Domain.Model.Models.MusicoModel> MusicoModel { get; set; }
    }
}
