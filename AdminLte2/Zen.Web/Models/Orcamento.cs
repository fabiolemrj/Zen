using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{
    [Table("orcamento")]
    public class Orcamento
    {
        [Key]
        [Column(name: "IDPEDIDO")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdPedido { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Column(name: "DTPEDIDO")]
        public DateTime? DtPedido { get; set; }

        [MaxLength(50)]
        [Column(name: "CLIENTE")]
        public string NomeCliente { get; set; }

        [NotMapped]
        public Cliente Cliente { get; set; }

        [MaxLength(50)]
        [Column(name: "CONTATO")]
        public string Contato { get; set; }

        [MaxLength(50)]
        [Column(name: "INDICACAO")]
        public string Indicacao { get; set; }

        [MaxLength(50)]
        [Column(name: "REFERENCIA")]
        public string NmReferencia { get; set; }

        [NotMapped]
        public Cliente Referencia { get; set; }

        [MaxLength(15)]
        [Column(name: "TEL1")]
        public string Tel1 { get; set; }

        [MaxLength(10)]
        [Column(name: "RAMAL1")]
        public string Ramal1 { get; set; }

        [MaxLength(15)]
        [Column(name: "TEL2")]
        public string Tel2 { get; set; }

        [MaxLength(10)]
        [Column(name: "RAMAL2")]
        public string Ramal2 { get; set; }

        [MaxLength(15)]
        [Column(name: "CELULAR")]
        public string Celular { get; set; }

        [MaxLength(15)]
        [Column(name: "FAX")]
        public string Fax { get; set; }

        [MaxLength(10)]
        [Column(name: "RAMALF")]
        public string RamalF { get; set; }

        [MaxLength(50)]
        [Column(name: "EMAIL1")]
        public string Email1 { get; set; }

        [MaxLength(50)]
        [Column(name: "EMAIL2")]
        public string Email2 { get; set; }

        [Column(name: "IDCLIENTE")]
        public int? IdCliente { get; set; }

        [Column(name: "IDREFERENCIA")]
        public int? IdReferencia { get; set; }

        [Column(name: "COMISSAO")]
        public double? Comissao { get; set; }

        [Column(name: "PRAZO")]
        public DateTime? Prazo { get; set; }

        [Column(name: "FORMA_PAG")]
        public string FormaPag { get; set; }

        [MaxLength(1)]
        [Column(name: "INCOMPLETO")]
        public string Incompleto { get; set; }

        [Column(name: "OBS")]
        public string Obs { get; set; }

        [MaxLength(1)]
        [Column(name: "Notif_Email")]
        public string NotifEmail { get; set; }

        [MaxLength(1)]
        [Column(name: "Notif_Fax")]
        public string NotifFax { get; set; }

        [MaxLength(1)]
        [Column(name: "Notif_Tel")]
        public string NotifTel { get; set; }

        [MaxLength(1)]
        [Column(name: "NOTIF_CEL")]
        public string NotifCel { get; set; }

        [MaxLength(1)]
        [Column(name: "NOTIF_OUTRO")]
        public string NotiFOutro { get; set; }

        [Column(name: "IDUSUARIO")]
        public int? IdUsuario { get; set; }

        [Column(name: "IDUSUATU")]
        public int? IdUsuAtu { get; set; }

        [MaxLength(1)]
        [Column(name: "URGENTE")]
        public string Urgente { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Column(name: "DTATUAL")]
        public DateTime? DtAtual { get; set; }

        [Column(name: "IDORCAMENTO")]
        public int? IdOrcamento { get; set; }

        [Column(name: "ITENS_ENV")]
        public int? ItensEnv { get; set; }

        [Column(name: "ITENS_APROV")]
        public int? ItensAprov { get; set; }

        [Column(name: "SINAL_PERC")]
        public double? SinalPerc { get; set; }

        [MaxLength(10)]
        [Column(name: "EDITANDO")]
        public string Editando { get; set; }

        [MaxLength(1)]
        [Column(name: "PENDENTE")]
        public string Pendente { get; set; }

        [Column(name: "ITENS_PEND")]
        public int? ItensPend { get; set; }

    }
}