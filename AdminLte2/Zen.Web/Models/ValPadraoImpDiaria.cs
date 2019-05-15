using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{
    public class ValPadraoImpDiaria
    {
        public int Tipo { get; set; }
        public double Larg { get; set; }
        public double Alt { get; set; }
        public double Marg { get; set; }
        public double Dist { get; set; }
        public int NumCart { get; set; }
        public int NumImp { get; set; }
        public double ValorImp { get; set; }

    }
}