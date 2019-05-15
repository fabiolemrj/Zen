using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zen.Web.ViewModels.OrcamentoImpDiarioViewModel
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

        [Display(Name = "Valor")]
        public double? Valor { get; set; }

        [Display(Name = "Tipo")]
        public int? TpImpDia { get; set; }

        [Display(Name = "Fixo")]
        public string Fixo { get; set; }

        [Display(Name = "Impressões por dia")]
        public int? QuantImpDia { get; set; }

        [Display(Name = "Total")]
        public double Total { get; set; }

        [Display(Name = "Valor Dia")]
        public double? ValorDia { get; set; }
    }
}