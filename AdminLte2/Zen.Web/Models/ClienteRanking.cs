using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{
    public class ClienteRanking
    {        
        public int IdCliente { get; set; }

        public string Nome { get; set; }

        public double Valor { get; set; }

        public int Posicao { get; set; }
    }
}