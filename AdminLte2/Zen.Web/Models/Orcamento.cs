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

        [Column(name: "LARG_F")]
        public double? LargF { get; set; }

        [Column(name: "ALT_F")]
        public double? AltF { get; set; }

        [Column(name: "COMP_F")]
        public double? CompF { get; set; }

        [Column(name: "LARG_A")]
        public double? LargA { get; set; }

        [Column(name: "ALT_A")]
        public double? AltA { get; set; }

        [Column(name: "QTD")]
        public int? Qtd { get; set; }

        [Column(name: "IMP_F")]
        public int? ImpF { get; set; }

        [Column(name: "IMP_V")]
        public int? ImpV { get; set; }

        [MaxLength(1)]
        [Column(name: "ARTE_FINAL")]
        public string ArteFinal { get; set; }

        [MaxLength(1)]
        [Column(name: "FOTO_TRACO")]
        public string FotoTraco { get; set; }

        [MaxLength(1)]
        [Column(name: "FOTO_TRACO_FORNEC")]
        public string FotoTracoFornec { get; set; }

        [MaxLength(1)]
        [Column(name: "FOTO_RET")]
        public string FotoRet { get; set; }

        [MaxLength(1)]
        [Column(name: "FOTO_RET_FORNEC")]
        public string FotoRetFornec { get; set; }

        [MaxLength(1)]
        [Column(name: "FOTO_POLI")]
        public string FotoPoli { get; set; }

        [MaxLength(1)]
        [Column(name: "FOTO_POLI_FORNEC")]
        public string FotoPoliFornec { get; set; }

        [MaxLength(2)]
        [Column(name: "SIT_FOTOLITO")]
        public string SitFotolito { get; set; }

        [Column(name: "OBS1")]
        public string Obs1 { get; set; }
    }
}