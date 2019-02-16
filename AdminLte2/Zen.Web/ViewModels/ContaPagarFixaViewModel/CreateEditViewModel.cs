using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zen.Web.ViewModels.ContaPagarFixaViewModel
{
    public class CreateEditViewModel
    {
        [Key]
        [Display(Order =1)]
        public int IdDesp { get; set; }

        [Key]
        [Display(Name = "Despesa",Order =2)]
        [Required(ErrorMessage = "O campo despesa é obrigatório", AllowEmptyStrings = false)]
        public int IdSubDesp { get; set; }

        [MaxLength(1, ErrorMessage = "O campo estado deve ter apenas 1 caracter")]
        [Display(Name = "Estado")]
        public string Estado { get; set; }

        [MaxLength(1, ErrorMessage = "O campo periodicidade deve ter apenas 1 caracter")]
      //  [Required(ErrorMessage = "O campo periodicidade é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Periodocidade")]
        public string Periodicidade { get; set; }

        [MaxLength(64, ErrorMessage = "O campo histórico deve ter até 64 caracter")]
        [Display(Name = "Histórico")]
        public string Historico { get; set; }

        [Display(Name = "Numero Parcela Atual")]
        public int? NuParcAtu { get; set; }

        [Display(Name = "Numero Parcela")]
        [Required(ErrorMessage = "O campo numero parcela é obrigatório", AllowEmptyStrings = false)]
        public int? NumParc  { get; set; }

        [Display(Name = "Dia Vencimento")]
        [Required(ErrorMessage = "O campo dia é obrigatório", AllowEmptyStrings = false)]
        [Range(1, 31)]
        public int? Dia { get; set; }

        [Display(Name = "Fornecedor")]
        public int? IdFornecedor { get; set; }

        [Display(Name = "Forma de Pagamento")]
        [Required(ErrorMessage = "O campo forma de pagamento é obrigatório", AllowEmptyStrings = false)]
        public int? IdFormaPag { get; set; }

        [Display(Name = "Setor")]
        public int? IdSetor { get; set; }

        [Display(Name = "Tipo de Documento")]
        public int? IdTipoDoc { get; set; }

        [Display(Name = "Usuário")]
        public int? IdUsuario { get; set; }

        [Display(Name = "Conta Corrente")]
        public int? IdCc { get; set; }

        [Display(Name = "Valor")]
        [Range(0, 9999999999999999.99)]
        public double Valor { get; set; }
                
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Ultimo Lançamento")]
        public DateTime? DtUltLanc { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Primeiro Pagamento")]
        public DateTime? DtPrimLanc { get; set; }

    }
}