using Domain.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetAT.Models
{
    public class BandaIndexViewModel
    {
        public string Search { get; set; }
        public bool OrderAscendant { get; set; }
        public IEnumerable<BandaModel> Bandas { get; set; }
    }
}
