using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{
    [Table("orcamentodet")]
    public class OrcamentoDet
    {
        [Key]
        [Column(name: "IDPEDIDO",Order =1)]
        public int IdPedido { get; set; }

        [Key]
        [Column(name: "ITEM", Order = 2)]
        public int Item { get; set; }

        [MaxLength(30)]
        [Column(name: "PRODUTO")]
        public string DescProduto { get; set; }

        [Column(name: "IDPRODUTO")]
        public int? IdProduto { get; set; }

        [NotMapped]
        public Produto Produto { get; set; }

        [MaxLength(50)]
        [Column(name: "MATERIAL1")]
        public string DescMaterial1 { get; set; }

        [Column(name: "IDMAT1")]
        public int? IdMaterial1 { get; set; }

        [NotMapped]
        public Material Material1 { get; set; }

        [MaxLength(1)]
        [Column(name: "MAT1_FORNEC")]
        public string Mat1Fornec { get; set; }

        [MaxLength(50)]
        [Column(name: "MATERIAL2")]
        public string DescMaterial2 { get; set; }

        [Column(name: "IDMAT2")]
        public int? IdMaterial2 { get; set; }

        [NotMapped]
        public Material Material2 { get; set; }

        [MaxLength(1)]
        [Column(name: "MAT2_FORNEC")]
        public string Mat2Fornec { get; set; }

        [MaxLength(50)]
        [Column(name: "MATERIAL3")]
        public string DescMaterial3 { get; set; }

        [Column(name: "IDMAT3")]
        public int? IdMaterial3 { get; set; }

        [NotMapped]
        public Material Material3 { get; set; }

        [MaxLength(1)]
        [Column(name: "MAT3_FORNEC")]
        public string Mat3Fornec { get; set; }

        [MaxLength(50)]
        [Column(name: "MATERIAL4")]
        public string DescMaterial4 { get; set; }

        [Column(name: "IDMAT4")]
        public int? IdMaterial4 { get; set; }

        [NotMapped]
        public Material Material4 { get; set; }

        [MaxLength(1)]
        [Column(name: "MAT4_FORNEC")]
        public string Mat4Fornec { get; set; }

        [Column(name: "PRAZO")]
        public DateTime? Prazo { get; set; }

        [MaxLength(1)]
        [Column(name: "RELEVO_SECO")]
        public string RelevoSeco { get; set; }

        [MaxLength(1)]
        [Column(name: "RS_CHAPA")]
        public string RsChapa { get; set; }

        [Column(name: "LARG_Rs")]
        public double? LargRs { get; set; }

        [Column(name: "ALT_Rs")]
        public double? AltRs { get; set; }

        [MaxLength(1)]
        [Column(name: "HOT_STAMP")]
        public string HotStamp { get; set; }

        [MaxLength(1)]
        [Column(name: "HS_CHAPA")]
        public string HsChapa { get; set; }

        [Column(name: "LARG_HS")]
        public double? LargHs { get; set; }

        [Column(name: "ALT_HS")]
        public double? AltHs { get; set; }

        [MaxLength(1)]
        [Column(name: "OFFSET")]
        public string OffSet { get; set; }

        [Column(name: "OFF_F")]
        public int? OffF { get; set; }

        [Column(name: "OFF_V")]
        public int? OffV { get; set; }

        [MaxLength(1)]
        [Column(name: "OUTROS_IMP")]
        public string OutrosImp { get; set; }

        [MaxLength(32)]
        [Column(name: "OBSIMP")]
        public string ObsImp { get; set; }

        [MaxLength(1)]
        [Column(name: "SEM_ACAB")]
        public string SemAcab { get; set; }

        [MaxLength(1)]
        [Column(name: "CORTE_SIMPLES")]
        public string CorteSimples { get; set; }

        [MaxLength(1)]
        [Column(name: "DOBRA")]
        public string Dobra { get; set; }

        [MaxLength(1)]
        [Column(name: "COLA")]
        public string Cola { get; set; }

        [MaxLength(1)]
        [Column(name: "CONTRAPLACA")]
        public string ContraPlaca { get; set; }

        [MaxLength(1)]
        [Column(name: "ENCADERNACAO")]
        public string Encadernacao { get; set; }

        [MaxLength(1)]
        [Column(name: "ELASTICO")]
        public string Elastico { get; set; }

        [MaxLength(1)]
        [Column(name: "CORDAO")]
        public string Cordao { get; set; }

        [MaxLength(1)]
        [Column(name: "MONTAGEM")]
        public string Montagem { get; set; }

        [MaxLength(1)]
        [Column(name: "PINTURA")]
        public string Pintura { get; set; }

        [MaxLength(1)]
        [Column(name: "OUTROS_ACAB1")]
        public string OutrosAcab1 { get; set; }
        
        [Column(name: "OBS_ACAB1")]
        public string ObsAcab1 { get; set; }

        [MaxLength(10)]
        [Column(name: "COD_FACA")]
        public string CodFaca { get; set; }

        [MaxLength(1)]
        [Column(name: "CORTE_ESP")]
        public string CorteEsp { get; set; }

        [MaxLength(1)]
        [Column(name: "CORTE_VINCO")]
        public string CorteVinco { get; set; }

        [MaxLength(1)]
        [Column(name: "MEIO_CORTE")]
        public string MeioCorte { get; set; }

        [MaxLength(1)]
        [Column(name: "VINCO")]
        public string Vinco { get; set; }

        [MaxLength(1)]
        [Column(name: "LAM_FOSCA_F")]
        public string LamFoscaF { get; set; }

        [MaxLength(1)]
        [Column(name: "LAM_FOSCA_V")]
        public string LamFoscaV { get; set; }

        [Column(name: "VAZADOR")]
        public int? Vazador { get; set; }

        [Column(name: "ILHOS")]
        public int? Ilhos { get; set; }

        [MaxLength(1)]
        [Column(name: "WIRE_O")]
        public string WireO { get; set; }
        
        [MaxLength(1)]
        [Column(name: "ESPIRAL")]
        public string Espiral { get; set; }

        [MaxLength(1)]
        [Column(name: "OUTROS_ACAB2")]
        public string OutrosAcab2 { get; set; }
        
        [Column(name: "IDOSI")]
        public int? IdOsi { get; set; }

        [MaxLength(1)]
        [Column(name: "EXECUTADO")]
        public string Executado { get; set; }
    }
}