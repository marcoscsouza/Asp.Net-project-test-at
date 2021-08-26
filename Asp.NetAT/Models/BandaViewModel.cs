using Domain.Model.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetAT.Models
{
    public class BandaViewModel
    {
        public int Id { get; set; }
        [StringLength(30)]
        [Remote(action: "IsNomeValid", controller: "Banda", AdditionalFields = "Id")]
        public string Nome { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Inicio da Banda")]
        public DateTime InicioBanda { get; set; }
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        [Display(Name = "Genero Musical")]
        public string GeneroMusical { get; set; }
        [Required]
        [Display(Name = "Pais de Origem")]
        public string Nacionalidade { get; set; }
        [Display(Name = "Continua fazendo show?")]
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
