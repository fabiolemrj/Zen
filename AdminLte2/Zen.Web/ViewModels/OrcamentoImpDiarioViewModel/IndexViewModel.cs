using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.ViewModels.OrcamentoImpDiarioViewModel
{
    public class IndexViewModel
    {
        public int Idpedido { get; set; }
        public int Item { get; set; }
        public int NrSeq { get; set; }
        public List<OrcImpDiaria> LstOrcImpDiaria { get; set; }
    }
}