using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zen.Web.ViewModels.ContaPagarViewModel
{
    public class CreateEditViewModel
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(1, ErrorMessage = "O campo estado deve ter apenas 1 caracter")]
        [Display(Name = "Estado")]
        public string Estado { get; set; }

        [MaxLength(64, ErrorMessage = "O campo histórico deve ter até 64 caracter")]
        [Display(Name = "Histórico")]
        [Required(ErrorMessage = "O campo histórico é obrigatório", AllowEmptyStrings = false)]
        public string Historico { get; set; }
        
        [MaxLength(255, ErrorMessage = "O campo histórico deve ter até 255 caracter")]
        [Display(Name = "Detalhe da Compra")]
        [Required(ErrorMessage = "O campo detalhe da compra é obrigatório", AllowEmptyStrings = false)]
        public string DetCompra { get; set; }

        [Display(Name = "Banco")]
        [Required(ErrorMessage = "O campo banco é obrigatório", AllowEmptyStrings = false)]
        public int? IdBanco { get; set; }

        [Display(Name = "Conta Corrente")]
        [Required(ErrorMessage = "O campo conta corrente é obrigatório", AllowEmptyStrings = false)]
        public int? IdCc { get; set; }

        [Display(Name = "Cheque")]
        public int? IdCheque { get; set; }

        [Display(Name = "Fornecedor")]
        [Required(ErrorMessage = "O campo fornecedor é obrigatório", AllowEmptyStrings = false)]
        public int? IdFornecedor { get; set; }

        [Display(Name = "Forma de Pagamento")]
        [Required(ErrorMessage = "O campo forma de pagamento é obrigatório", AllowEmptyStrings = false)]
        public int? IdFormaPag { get; set; }

        [Display(Name = "Setor")]
        public int? IdSetor { get; set; }

        [Display(Name = "Tipo de Documento")]
        public int? IdTipoDoc { get; set; }

        [Display(Name = "Despesa")]
        [Required(ErrorMessage = "O campo Despesa é obrigatório", AllowEmptyStrings = false)]
        public int? IdSubDesp { get; set; }

        [Display(Name = "Usuário")]
        public int? IdUsuario { get; set; }

        [Display(Name = "Juros")]
        public double Juros { get; set; }

        [MaxLength(10, ErrorMessage = "O campo Numero do cheque deve ter até 15 caracter")]
        [Display(Name = "Numero de Documento")]
        public string NumDoc { get; set; }

        [MaxLength(1, ErrorMessage = "O campo FlgConf deve ter até 1 caracter")]
        [Display(Name = "FlgConf")]
        public string FlgConf { get; set; }

        [MaxLength(15, ErrorMessage = "O campo Numero Cheque deve ter até 1 caracter")]
        [Display(Name = "Numero Cheque")]
        public string NumCheque { get; set; }

        [MaxLength(64, ErrorMessage = "O campo Observação deve ter até 64 caracter")]
        [Display(Name = "Observação")]
        public string Obs { get; set; }

        [Display(Name = "Valor")]
        [Required(ErrorMessage = "O campo valor é obrigatório", AllowEmptyStrings = false)]
        public double Valor { get; set; }

        [Display(Name = "Desconto")]
        [Required(ErrorMessage = "O campo desconto é obrigatório", AllowEmptyStrings = false)]
        public double Desconto { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data de Pagamento")]
        public DateTime? DtPag { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "O campo data de vencimento é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Data de Vencimento")]
        public DateTime? DtVenc { get; set; }

        [Display(Name = "Total")]
        public double Total { get; set; }
    }
}