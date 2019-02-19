using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zen.Web.ViewModels.ContaReceberViewModel
{
    public class CreateEditViewModel
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(1, ErrorMessage = "O campo estado deve ter apenas 1 caracter")]
        [Display(Name = "Estado")]
        public string Estado { get; set; }

        [MaxLength(64, ErrorMessage = "O campo histórico deve ter até 64 caracter")]
        [Display(Name= "Histórico")]
        [Required(ErrorMessage = "O campo histórico é obrigatório", AllowEmptyStrings = false)]
        public string Historico { get; set; }

        [Display(Name = "Banco")]
        [Required(ErrorMessage = "O campo banco é obrigatório", AllowEmptyStrings = false)]
        public int? IdBancoCheque { get; set; }

        [Display(Name = "Conta Corrente")]
        [Required(ErrorMessage = "O campo conta corrente é obrigatório", AllowEmptyStrings = false)]
        public int? IdCc { get; set; }

        [Display(Name = "Cheque")] 
        public int? IdCheque { get; set; }

        [Display(Name = "Cliente")]
        public int? IdCliente { get; set; }
     
        [Display(Name= "Forma de Pagamento")]
        [Required(ErrorMessage = "O campo forma de pagamento é obrigatório", AllowEmptyStrings = false)]
        public int? IdFormaPag { get; set; }
        
        [Display(Name= "Setor")]
        public int? IdSetor { get; set; }

        [Display(Name = "Tipo de Documento")]
        [Required(ErrorMessage = "O campo tipo de documento é obrigatório", AllowEmptyStrings = false)]
        public int? IdTipoDoc { get; set; }

        [Display(Name = "Tipo de Receita")]
        [Required(ErrorMessage = "O campo tipo de receita é obrigatório", AllowEmptyStrings = false)]
        public int? IdTipoReceita { get; set; }

        [Display(Name = "Usuário")]
        public int? IdUsuario { get; set; }

        [Display(Name = "Juros")]       
        public double Juros { get; set; }

        [Display(Name = "Agencia")]
        public int? NumAgChq { get; set; }

        [MaxLength(15, ErrorMessage = "O campo Numero do cheque deve ter até 15 caracter")]
        [Display(Name = "Numero Cheque")]
        public string NumChq { get; set; }

        [MaxLength(10, ErrorMessage = "O campo Numero do cheque deve ter até 15 caracter")]
        [Display(Name = "Numero de Documento")]
        public string NumDoc { get; set; }

        [MaxLength(1, ErrorMessage = "O campo FlgConf deve ter até 1 caracter")]
        [Display(Name = "FlgConf")]
        public string FlgConf { get; set; }

        [MaxLength(10, ErrorMessage = "O campo Numero da OSI deve ter até 10 caracter")]
        [Display(Name = "OSI")]
        public string NumOsi { get; set; }

        [MaxLength(10, ErrorMessage = "O campo Parcela deve ter até 10 caracter")]
        [Display(Name = "Parcela")]
        public string NumParc { get; set; }

        [MaxLength(64, ErrorMessage = "O campo Observação deve ter até 64 caracter")]
        [Display(Name = "Observação")]
        public string Obs { get; set; }

        [Display(Name = "Percentual Desconto")]
        [Range(0, 9999999999999999.99)]
        public double PercTitDesc { get; set; } 

        [Display(Name = "Valor")]
        [Required(ErrorMessage = "O campo valor é obrigatório", AllowEmptyStrings = false)]
        public double Valor { get; set; } 

        [Display(Name = "Desconto")]
        public double Desconto { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data de Pagamento")]
        public DateTime? DtPag { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data Pag. Desconto")]
        public DateTime? DtPagDesc { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "O campo data de vencimento é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Data de Vencimento")]
        public DateTime? DtVenc { get; set; }

        [Display(Name = "Total")]
        public double Total { get; set; }
    }
}