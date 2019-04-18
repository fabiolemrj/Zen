using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zen.Web.ViewModels.OrcamentoVariacoesViewModel
{
    public class CreateEditViewModel
    {
        [Display(Name = "Pedido")]
        public int IdPedido { get; set; }

        [Display(Name = "Item")]
        public int Item { get; set; }

        [Display(Name = "Sequencial")]
        public int NrSeq { get; set; }


        [Display(Name = "Quantidade")]
        public int? Quant { get; set; }
        
        [Display(Name = "Variação")]
        public string Variacao { get; set; }
    }
}