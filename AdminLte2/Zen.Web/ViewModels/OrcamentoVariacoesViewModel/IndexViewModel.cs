using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.ViewModels.OrcamentoVariacoesViewModel
{
    public class IndexViewModel
    {
        public int Idpedido { get; set; }
        public int Item { get; set; }
        public int NrSeq { get; set; }
        public List<OrcVariacao> LstOrcVariacoes { get; set; }
        public string QuantFrente { get; set; }
        public string QuantVerso { get; set; }
        public bool FrenteIgual { get; set; }
        public bool VersoIgual { get; set; }
    }
}