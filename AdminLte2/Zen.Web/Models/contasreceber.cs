using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{
    [Table("contasreceber")]
    public class ContasReceber
    {
        [Key]
        [Column(name: "IDTITULO")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [MaxLength(50)]
        [Column(name: "FLG_TIT_DESC")]
        public string FlgTitDesc { get; set; }

        [MaxLength(1)]
        [Column(name: "ESTADO")]
        public string Estado { get; set; }

        [MaxLength(1)]
        [Column(name: "FLGCONF")]
        public string FlgConf { get; set; }

        [MaxLength(64)]
        [Column(name: "HIST")]
        public string Historico { get; set; }

        [Column(name: "IDBANCOCHQ")]
        public int? IdBancoCheque { get; set; }

        [NotMapped]
        public Banco BancoCheque { get; set; }

        [Column(name: "IDCC")]
        public int? IdCc { get; set; }

        [NotMapped]
        public ContaCorrente ContaCorrente { get; set; }

        [Column(name: "IDCHQ")]
        public int? IdCheque { get; set; }

        [Column(name: "IDCLIENTE")]
        public int? IdCliente { get; set; }
        [NotMapped]
        public Cliente Cliente { get; set; }

        [Column(name: "IDDONOCHQ")]
        public int? IdDoNoChq { get; set; }

        [Column(name: "IDFORMAPGM")]
        public int IdFormaPag { get; set; }
        [NotMapped]
        public FormaPag FormaPag { get; set; }

        [Column(name: "IDLINK")]
        public int? IdLink { get; set; }

        [Column(name: "IDSETOR")]
        public int? IdSetor { get; set; }

        [NotMapped]
        public Setor Setor { get; set; }

        [Column(name: "IDTIPODOC")]
        public int? IdTipoDoc { get; set; }

        [NotMapped]
        public TipoDoc TipoDoc { get; set; }

        [Column(name: "IDTIPOREC")]
        public int? IdTipoReceita { get; set; }

        [NotMapped]
        public TipoReceita TipoReceita { get; set; }

        [Column(name: "IDUSUARIO")]
        public int? IdUsuario { get; set; }

        [NotMapped]
        public Usuario Usuario { get; set; }

        [Column(name: "JUROS")]
        [Range(0, 9999999999999999.99)]
        public double Juros { get; set; } = 0;

        [Column(name: "NUM_AG_CHQ")]
        public int? NumAgChq { get; set; }

        [Column(name: "NUM_CHQ")]
        [MaxLength(15)]
        public string NumChq { get; set; }

        [Column(name: "NUM_OSI")]
        [MaxLength(10)]
        public string NumOsi { get; set; }

        [Column(name: "NUM_PARC")]
        [MaxLength(10)]
        public string NumParc { get; set; }

        [Column(name: "OBS")]
        [MaxLength(64)]
        public string Obs { get; set; }

        [Column(name: "PERC_TIT_DESC")]
        [Range(0, 9999999999999999.99)]
        public double PercTitDesc { get; set; } = 0;

        [Column(name: "SIT_TIT_DESC")]
        [MaxLength(1)]
        public string SitTitDesc { get; set; }

        [Column(name: "TIPOPER")]
        [MaxLength(1)]
        public string TipoOper { get; set; }

        [Column(name: "VALOR")]
        [Range(0, 9999999999999999.99)]
        public double Valor { get; set; } = 0;

        [Column(name: "DESCONTO")]
        [Range(0, 9999999999999999.99)]
        public double Desconto { get; set; } = 0;

        [Column(name: "DTPAG")]
        public DateTime? DtPag { get; set; }

        [Column(name: "DTPAG_TIT_DESC")]
        public DateTime? DtPagDesc { get; set; }

        [Column(name: "DTVENC")]
        public DateTime? DtVenc { get; set; }
    }
}