using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{
   
    public class ContaPagarObs
    {
   
        [Column(name: "IDTITULO")]
        public int IdTitulo { get; set; }

        [MaxLength(150)]
        [Column(name: "PRODUTO")]
        public string Produto { get; set; }

        [MaxLength(150)]
        [Column(name: "QUANT")]
        public string Quantidade { get; set; }

        [MaxLength(150)]
        [Column(name: "MARCA")]
        public string Marca { get; set; }

        [MaxLength(150)]
        [Column(name: "VALOR")]
        public string Valor { get; set; }
    }
}