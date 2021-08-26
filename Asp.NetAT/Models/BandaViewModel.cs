using Domain.Model.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetAT.Models
{
    public class BandaViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime InicioBanda { get; set; }
        public string GeneroMusical { get; set; }
        public string Nacionalidade { get; set; }
        public bool FazendoShow { get; set; }

        public List<MusicoViewModel> Musicos { get; set; }


        public static BandaViewModel From(BandaModel bandaModel)
        {       //cuidar com a referencia ciclica!
            var bandaViewModel = new BandaViewModel
            {
                Id = bandaModel.Id,
                Nome = bandaModel.Nome,
                InicioBanda = bandaModel.InicioBanda,
                GeneroMusical = bandaModel.GeneroMusical,
                Nacionalidade = bandaModel.Nacionalidade,
                FazendoShow = bandaModel.FazendoShow,

                //comentada a linha de baixo e colocada "?" ou break no bandaModel
                Musicos = bandaModel?.Musicos.Select(x => MusicoViewModel.From(x, false)).ToList(),
            };

            return bandaViewModel;
        }
        public BandaModel ToModel()
        {
            var bandaModel = new BandaModel
            {
                Id = Id,
                Nome = Nome,
                InicioBanda = InicioBanda,
                GeneroMusical = GeneroMusical,
                Nacionalidade = Nacionalidade,
                FazendoShow = FazendoShow,

                //comentada a linha de baixo e colocada "?" ou break no bandaModel
                Musicos = Musicos?.Select(x => x.ToModel(false)).ToList(),
            };

            return bandaModel;
        }
    }
}
