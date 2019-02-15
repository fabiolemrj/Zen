using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{
    [Table("contaspagar")]
    public class ContaPagar
    {
        [Key]
        [Column(name: "IDTITULO")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [MaxLength(255)]
        [Column(name: "DETALHE_COMPRA")]
        public string DetalheCompra { get; set; }

        [Column(name: "DESCONTOS")]
        [Range(0, 9999999999999999.99)]
        public double? Desconto { get; set; } = 0;

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Column(name: "DTINS")]
        public DateTime? Dtins { get; set; } = DateTime.Now;

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Column(name: "DTVENC")]
        public DateTime? DtVenc { get; set; } = DateTime.Now;

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Column(name: "DTPAG")]
        public DateTime? DtPag { get; set; }

        [MaxLength(1)]
        [Column(name: "ESTADO")]
        public string Estado { get; set; }

        [MaxLength(1)]
        [Column(name: "FLGCONF")]
        public string FlgConf { get; set; }

        [MaxLength(150)]
        [Column(name: "HIST")]
        public string Historico { get; set; }

        [Column(name: "IDBANCO")]
        public int? IdBanco { get; set; }

        [NotMapped]
        public Banco Banco { get; set; }

        [Column(name: "IDCC")]
        public int? IdCc { get; set; }

        [NotMapped]
        public ContaCorrente ContaCorrente { get; set; }

        [Column(name: "IDDESP")]
        public int? IdDesp { get; set; }

        [NotMapped]
        public Despesa Despesa { get; set; }

        [Column(name: "IDFORMAPGM")]
        public int? IdFormaPag { get; set; }

        [NotMapped]
        public FormaPag FormaPag { get; set; }

        [Column(name: "IDFORNECEDOR")]
        public int? IdFornecedor { get; set; }

        [NotMapped]
        public Fornecedor Fornecedor { get; set; }

        [Column(name: "IDLINK")]
        public int? IdLink { get; set; }

        [Column(name: "IDSETOR")]
        public int? IdSetor { get; set; }

        [NotMapped]
        public Setor Setor { get; set; }

        [Column(name: "IDSUBDESP")]
        public int? IdSubDesp { get; set; }

        [NotMapped]
        public SubDespesa SubDespesa { get; set; }

        [Column(name: "IDTIPODOC")]
        public int? IdTipoDoc { get; set; }

        [NotMapped]
        public TipoDoc TipoDoc { get; set; }

        [Column(name: "IDUSUARIO")]
        public int? IdUsuario { get; set; }

        [NotMapped]
        public Usuario Usuario { get; set; }

        [Column(name: "JUROS")]
        [Range(0, 9999999999999999.99)]
        public double? Juros { get; set; } = 0;

        [Column(name: "NUM_CHQ")]
        [MaxLength(15)]
        public string NumChq { get; set; }

        [Column(name: "NUMDOC")]
        [MaxLength(10)]
        public string NumDoc { get; set; }

        [Column(name: "OBS")]
        [MaxLength(64)]
        public string Obs { get; set; }

        [Column(name: "TIPOPER")]
        [MaxLength(1)]
        public string TipoOper { get; set; }

        [Column(name: "VALOR")]
        [Range(0, 9999999999999999.99)]
        public double? Valor { get; set; } = 0;
    }
}