using System;

namespace Domain.Model.Models
{
    public class MusicoModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public DateTime Nascimento { get; set; }

        public int BandaId { get; set; }

        public BandaModel Banda { get; set; }
    }
}
