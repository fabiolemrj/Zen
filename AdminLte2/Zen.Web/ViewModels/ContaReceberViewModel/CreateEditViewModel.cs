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

        [MaxLength(1, ErrorMessage = "O campo estado deve ter apenas 2 caracter")]
        [Display(Name = "Estado")]
        public string Estado { get; set; }

        [MaxLength(64, ErrorMessage = "O campo histórico deve ter até 64 caracter")]
        [Display(Name= "Histórico")]
        public string Historico { get; set; }

        [Display(Name = "Banco")]
        public int? IdBancoCheque { get; set; }

        [Display(Name = "Conta Corrente")]
        public int? IdCc { get; set; }

        [Display(Name = "Cheque")] 
        public int? IdCheque { get; set; }

        [Display(Name = "Cliente")]
        public int? IdCliente { get; set; }
     
        [Display(Name= "Forma de Pagamento")]
        public int? IdFormaPag { get; set; }
        
        [Display(Name= "Setor")]
        public int? IdSetor { get; set; }

        [Display(Name = "Tipo de Documento")]
        public int? IdTipoDoc { get; set; }

        [Display(Name = "Tipo de Receita")]
        public int? IdTipoReceita { get; set; }

        [Display(Name = "Usuário")]
        public int? IdUsuario { get; set; }

        [Display(Name = "Juros")]
        [Range(0, 9999999999999999.99)]
        public double Juros { get; set; } = 0;

        [Display(Name = "Agencia")]
        public int? NumAgChq { get; set; }

        [MaxLength(15, ErrorMessage = "O campo Numero do cheque deve ter até 15 caracter")]
        [Display(Name = "Histórico")]
        public string NumChq { get; set; }

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
        [Range(0, 9999999999999999.99)]
        public double Valor { get; set; } 

        [Display(Name = "Desconto")]
        [Range(0, 9999999999999999.99)]
        public double Desconto { get; set; } 

        [Display(Name = "Data de Pagamento")]
        public DateTime? DtPag { get; set; }

        [Display(Name = "Data Pag. Desconto")]
        public DateTime? DtPagDesc { get; set; }

        [Display(Name = "Data de Vencimento")]
        public DateTime? DtVenc { get; set; }
    }
}