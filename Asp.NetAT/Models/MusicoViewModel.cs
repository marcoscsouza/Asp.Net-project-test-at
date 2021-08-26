using Domain.Model.Models;
using System;

namespace Asp.NetAT.Models
{
    public class MusicoViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public DateTime Nascimento { get; set; }

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
