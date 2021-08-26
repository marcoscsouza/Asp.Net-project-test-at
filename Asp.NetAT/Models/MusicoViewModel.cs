using Domain.Model.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Asp.NetAT.Models
{
    public class MusicoViewModel
    {
        public int Id { get; set; }
        [StringLength(20)]
        [Display(Name = "PrimeiroNome")]
        public string Nome { get; set; }
        [StringLength(20)]
        [Display(Name = "Ultimo Nome")]
        public string SobreNome { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data de nascimento")]
        public DateTime Nascimento { get; set; }
        [Display(Name = "Banda")]
        public int BandaId { get; set; }

        public BandaViewModel Banda { get; set; }

        // transforma modelo em lista estatica
        public static MusicoViewModel From(MusicoModel musicoModel, bool firstMap = true)
        {

            var banda = firstMap    // para interromper recursão
                ? BandaViewModel.From(musicoModel.Banda)
                : null;

            var musicoViewModel = new MusicoViewModel
            {
                Id = musicoModel.Id,
                Nome = musicoModel.Nome,
                SobreNome = musicoModel.SobreNome,
                Nascimento = musicoModel.Nascimento,
                BandaId = musicoModel.BandaId,

                Banda = banda,
            };

            return musicoViewModel;
        }

        //tranforma lista para modelo
        public MusicoModel ToModel(bool firstMap = true)
        {

            var banda = firstMap
                ? Banda?.ToModel()
                : null;

            var musicoModel = new MusicoModel
            {
                Id = Id,
                Nome = Nome,
                SobreNome = SobreNome,
                Nascimento = Nascimento,
                BandaId = BandaId,

                // colocado um break ou "?" depois de Banda para impedir o retorno de null
                Banda = banda,
            };

            return musicoModel;
        }
    }
}
