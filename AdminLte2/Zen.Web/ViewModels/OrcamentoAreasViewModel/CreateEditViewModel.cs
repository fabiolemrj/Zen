using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zen.Web.ViewModels.OrcamentoAreasViewModel
{
    public class CreateEditViewModel
    {
        [Display(Name = "Pedido")]
        public int IdPedido { get; set; }

        [Display(Name = "Item")]
        public int Item { get; set; }

        [Display(Name = "Sequencial")]
        public int NrSeq { get; set; }

        [Display(Name = "Altura")]
        public double? Altura { get; set; }

        [Display(Name = "Largura")]
        public double? Largura { get; set; }

        [Display(Name = "Quantidade")]
        public int? Quant { get; set; }

    }
}