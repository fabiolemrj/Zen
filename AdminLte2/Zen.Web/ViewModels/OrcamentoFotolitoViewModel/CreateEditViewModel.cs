using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.ViewModels.OrcamentoFotolitoViewModel
{
    public class CreateEditViewModel
    {
        public List<OrcFotolitos> LstFotolito { get; set; }

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

        [Display(Name = "Valor")]
        public double? Valor { get; set; }

        [Display(Name = "Fornecedor")]
        public string Fornecedor { get; set; }

        [Display(Name = "Fixo")]
        public string Fixo { get; set; }

        [Display(Name = "Total")]
        public double Total { get; set; }
    }
}