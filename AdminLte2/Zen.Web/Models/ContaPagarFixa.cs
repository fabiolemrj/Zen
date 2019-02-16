using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{
    [Table("contaspagarfixas")]
    public class ContaPagarFixa
    {
        [Key]
        [Column(name: "IDDESP", Order =1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdDesp { get; set; }

        [NotMapped]
        public Despesa Despesa { get; set; }

        [Key]
        [Column(name: "IDSUBDESP", Order =2)]
        public int IdSubDesp { get; set; }

        [NotMapped]
        public SubDespesa SubDespesa { get; set; }

        [Column(name: "DIA")]
        public int? Dia { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Column(name: "DTPRIMLANC")]
        public DateTime? DtPrimLanc { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Column(name: "DTULTLANC")]
        public DateTime? DtUltLanc { get; set; }

        [MaxLength(1)]
        [Column(name: "ESTADO")]
        public string Estado { get; set; }

        [MaxLength(64)]
        [Column(name: "HIST")]
        public string Historico { get; set; }


        [Column(name: "IDCC")]
        public int? IdCc { get; set; }

        [NotMapped]
        public ContaCorrente ContaCorrente { get; set; }
        
        [Column(name: "IDFORMAPGM")]
        public int? IdFormaPag { get; set; }

        [NotMapped]
        public FormaPag FormaPag { get; set; }

        [Column(name: "IDFORNECEDOR")]
        public int? IdFornecedor { get; set; }

        [NotMapped]
        public Fornecedor Fornecedor { get; set; }

        [Column(name: "IDSETOR")]
        public int? IdSetor { get; set; }

        [NotMapped]
        public Setor Setor { get; set; }

        [Column(name: "IDTIPODOC")]
        public int? IdTipoDoc { get; set; }

        [NotMapped]
        public TipoDoc TipoDoc { get; set; }

        [Column(name: "IDUSUARIO")]
        public int? IdUsuario { get; set; }

        [Column(name: "NPARC")]
        public int? NumParc { get; set; }

        [Column(name: "NPARCATU")]
        public int? NumParcAtu { get; set; }

        [MaxLength(1)]
        [Column(name: "PERIODICIDADE")]
        public string Periodicidade { get; set; }
        
        [Column(name: "VALOR")]
        [Range(0, 9999999999999999.99)]
        public double? Valor { get; set; } = 0;
    }
}