using Data.Data;
using Data.Repositories;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Interfaces.Services;
using Domain.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crosscutting.IoC
{
    public static class Bootstrapper
    {
        public static void RegisterServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AspNetATContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("AspNetATContext")));

            services.AddTransient<IBandaService, BandaService>();
            services.AddTransient<IBandaRepository, BandaRepository>();

            services.AddTransient<IMusicoService, MusicoService>();
            services.AddTransient<IMusicoRepository, MusicoRepository>();
        }
    }
}
