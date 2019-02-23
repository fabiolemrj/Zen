using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.ViewModels.Orcamento
{
    public class CreateEditViewModel
    {

        [Display(Name = "Pedido")]
        public int IdPedido { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "O campo Data do pedido é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Data")]
        public DateTime? DtPedido { get; set; }

        [MaxLength(50, ErrorMessage = "O campo Cliente deve ter até 50 caracter")]
        [Required(ErrorMessage = "O campo Cliente é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Cliente")]
        public string NomeCliente { get; set; }

        [MaxLength(50, ErrorMessage = "O campo Contato deve ter até 50 caracter")]
        [Required(ErrorMessage = "O campo contato é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Contato")]
        public string Contato { get; set; }

        [MaxLength(50, ErrorMessage = "O campo Indicação deve ter até 50 caracter")]
        [Required(ErrorMessage = "O campo Indicação é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Indicação")]
        public string Indicacao { get; set; }

        [MaxLength(50, ErrorMessage = "O campo Referência deve ter até 50 caracter")]
        [Required(ErrorMessage = "O campo referência é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Referência")]
        public string NmReferencia { get; set; }

        [MaxLength(15, ErrorMessage = "O campo Telefone deve ter até 15 caracter")]
        [Display(Name = "Telefone")]
        public string Tel1 { get; set; }

        [MaxLength(10, ErrorMessage = "O campo Ramal deve ter até 10 caracter")]
        [Display(Name = "Ramal")]
        public string Ramal1 { get; set; }

        [MaxLength(15, ErrorMessage = "O campo Telefone deve ter até 15 caracter")]
        [Display(Name = "Telefone")]
        public string Tel2 { get; set; }

        [MaxLength(10, ErrorMessage = "O campo Ramal deve ter até 10 caracter")]
        [Display(Name = "Ramal")]
        public string Ramal2 { get; set; }

        [MaxLength(15, ErrorMessage = "O campo Celular deve ter até 15 caracter")]
        [Display(Name = "Celular")]
        public string Celular { get; set; }

        [MaxLength(15, ErrorMessage = "O campo Fax deve ter até 15 caracter")]     
        [Display(Name = "Fax")]
        public string Fax { get; set; }

        [MaxLength(10, ErrorMessage = "O campo Ramal deve ter até 10 caracter")]
        [Display(Name = "Ramal")]
        public string RamalF { get; set; }

        [MaxLength(50, ErrorMessage = "O campo Email deve ter até 50 caracter")]
        [Display(Name = "Email")]
        public string Email1 { get; set; }

        [MaxLength(50, ErrorMessage = "O campo Email deve ter até 50 caracter")]
        [Display(Name = "Email")]
        public string Email2 { get; set; }

        [Display(Name = "Cliente")]
        public int? IdCliente { get; set; }

        [Display(Name = "Referencia")]
        public int? IdReferencia { get; set; }

        [Display(Name = "Comissão")]
        public double? Comissao { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]     
        [Display(Name = "Prazo")]
        public DateTime? Prazo { get; set; }

        [Display(Name = "Forma de Pagamento")]
        public string FormaPag { get; set; }

        [MaxLength(1, ErrorMessage = "O campo Incompleto deve ter até 1 caracter")]
        [Display(Name = "Incompleto")]
        public string Incompleto { get; set; }

        [Display(Name = "Forma de Observação")]
        public string Obs { get; set; }

        [MaxLength(1, ErrorMessage = "O campo Informar por email deve ter até 1 caracter")]
        [Display(Name = "Email")]
        public string NotifEmail { get; set; }

        [MaxLength(1, ErrorMessage = "O campo Informar por fax deve ter até 1 caracter")]
        [Display(Name = "Fax")]
        public string NotifFax { get; set; }

        [MaxLength(1, ErrorMessage = "O campo Informar por Telefone deve ter até 1 caracter")]
        [Display(Name = "Telefone")]
        public string NotifTel { get; set; }

        [MaxLength(1, ErrorMessage = "O campo Informar por celular deve ter até 1 caracter")]
        [Display(Name = "Celular")]
        public string NotifCel { get; set; }

        [MaxLength(1, ErrorMessage = "O campo Informar por outros deve ter até 1 caracter")]
        [Display(Name = "Outros")]
        public string NotiFOutro { get; set; }

        [Display(Name = "Usuário")]
        public int? IdUsuario { get; set; }

        [Display(Name = "Atualizado por")]
        public int? IdUsuAtu { get; set; }

        [Display(Name = "Urgente")]
        public string Urgente { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data Atual")]
        public DateTime? DtAtual { get; set; }
        
        [Display(Name = "Orçamento")]
        public int? IdOrcamento { get; set; }

        [Display(Name = "Itens enviados")]
        public int? ItensEnv { get; set; }

        [Display(Name = "Itens Aprovados")]
        public int? ItensAprov { get; set; }

        [Display(Name = "Sinal %")]
        public double? SinalPerc { get; set; }

        [Display(Name = "Observação")]
        public string Obs1 { get; set; }

        [MaxLength(1, ErrorMessage = "O campo Pendente por outros deve ter até 1 caracter")]
        [Display(Name = "Pendente")]
        public string Pendente { get; set; }

        [Display(Name = "Itens Pendentes")]
        public int? ItensPend { get; set; }

        public Cliente Cliente { get; set; }
    }
}