using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{
    [Table("orccalculo")]
    public class OrcCalculo
    {
        [Key]
        [Column(name: "IDPEDIDO", Order = 1)]
        public int IdPedido { get; set; }

        [Key]
        [Column(name: "ITEM", Order = 2)]
        public int Item { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Column(name: "DTCALCULADO")]
        public DateTime? DtCalculado { get; set; }

        [MaxLength(1)]
        [Column(name: "CALCULADO")]
        public string Calculado { get; set; }

        [MaxLength(1)]
        [Column(name: "ENVIADO")]
        public string Enviado { get; set; }

        [MaxLength(1)]
        [Column(name: "APROVADO")]
        public string Aprovado { get; set; }

        [MaxLength(1)]
        [Column(name: "TEM_CART")]
        public string TemCart { get; set; }

        [MaxLength(1)]
        [Column(name: "TEM_VAR")]
        public string TemVar { get; set; }

        [MaxLength(1)]
        [Column(name: "SANGRADA")]
        public string Sangrada { get; set; }

        [Column(name: "QTD_CART")]
        public int? QuantCart { get; set; }

        [Column(name: "MARGEM")]
        public double? Margem { get; set; }

        [Column(name: "DISTANCIA")]
        public double? Distancia { get; set; }

        [Column(name: "PERDAS")]
        public double? Perdas { get; set; }

        [Column(name: "LARG_CORT_INI")]
        public double? LargCortIni { get; set; }

        [Column(name: "ALT_CORT_INI")]
        public double? AltCortIni { get; set; }

        [Column(name: "QTD_MILHEIROS")]
        public int? QuantMilheiros { get; set; }

        [Column(name: "DSTRB_INS1")]
        public int? DistribIns1 { get; set; }

        [Column(name: "QTD_INS1")]
        public int? QuantIns1 { get; set; }

        [Column(name: "VR_INS1")]
        public double? VrIns1 { get; set; }

        [Column(name: "RISCO_INS1")]
        public double? RiscoIns1 { get; set; }

        [Column(name: "VRF_INS1")]
        public double? VrfIns1 { get; set; }

        [Column(name: "DSTRB_INS2")]
        public int? DistribIns2 { get; set; }

        [Column(name: "QTD_INS2")]
        public int? QuantIns2 { get; set; }

        [Column(name: "VR_INS2")]
        public double? VrIns2 { get; set; }

        [Column(name: "RISCO_INS2")]
        public double? RiscoIns2 { get; set; }

        [Column(name: "VRF_INS2")]
        public double? VrfIns2 { get; set; }

        [Column(name: "DSTRB_INS3")]
        public int? DistribIns3 { get; set; }

        [Column(name: "QTD_INS3")]
        public int? QuantIns3 { get; set; }

        [Column(name: "VR_INS3")]
        public double? VrIns3 { get; set; }

        [Column(name: "RISCO_INS3")]
        public double? RiscoIns3 { get; set; }

        [Column(name: "VRF_INS3")]
        public double? VrfIns3 { get; set; }

        [Column(name: "DSTRB_INS4")]
        public int? DistribIns4 { get; set; }

        [Column(name: "QTD_INS4")]
        public int? QuantIns4 { get; set; }

        [Column(name: "VR_INS4")]
        public double? VrIns4 { get; set; }

        [Column(name: "RISCO_INS4")]
        public double? RiscoIns4 { get; set; }

        [Column(name: "VRF_INS4")]
        public double? VrfIns4 { get; set; }

        [Column(name: "QTD_PERDAS")]
        public int? QuantPerdas { get; set; }

        [MaxLength(1)]
        [Column(name: "FOTO_AUTO")]
        public string FotoAuto { get; set; }

        [Column(name: "VR_FOTO")]
        public double? VrFoto { get; set; }

        [Column(name: "VR_M_ARTEFIM")]
        public double? VrMArteFim { get; set; }

        [Column(name: "RISCO_FOTO")]
        public double? RiscoFoto { get; set; }

        [Column(name: "VRF_FOTO")]
        public double? VrfFoto { get; set; }

        [MaxLength(1)]
        [Column(name: "FACA_FORNEC")]
        public string FacaFornec { get; set; }

        [Column(name: "FACA_EXT")]
        public int? FacaExt { get; set; }

        [MaxLength(1)]
        [Column(name: "FACA_DET")]
        public string FacaDet { get; set; }

        [Column(name: "VR_DET")]
        public double? VrDet { get; set; }

        [MaxLength(1)]
        [Column(name: "TIPO_LAM")]
        public string TipoLam { get; set; }

        [Column(name: "VR_FACA")]
        public double? VrFaca { get; set; }

        [Column(name: "VR_USOFACA")]
        public double? VrUsoFaca { get; set; }

        [Column(name: "VR_VAZADOR")]
        public double? VrVazador { get; set; }

        [Column(name: "RISCO_FACA")]
        public double? RiscoFaca { get; set; }

        [Column(name: "VRF_FACA")]
        public double? VrfFaca { get; set; }

        [Column(name: "VRF_USOFACA")]
        public double? VrfUsoFaca { get; set; }

        [MaxLength(64)]
        [Column(name: "TIPO_ILHOS")]
        public string TipoIlhos { get; set; }

        [Column(name: "COR_ILHOS")]
        public int? CorIlhos { get; set; }

        [Column(name: "VR_UNIT_ILHOS")]
        public double? VrUnitIlhos { get; set; }

        [Column(name: "VR_COLOC_ILHOS")]
        public double? VrColocIlhos { get; set; }

        [Column(name: "VR_ILHOS")]
        public double? VrIlhos { get; set; }

        [Column(name: "RISCO_ILHOS")]
        public double? RiscoIlhos { get; set; }

        [Column(name: "VRF_ILHOS")]
        public double? VrfIlhos { get; set; }

        [Column(name: "QTD_WO")]
        public int? QuantWo { get; set; }

        [Column(name: "DIAMETRO_WO")]
        public double? DiameatroWo { get; set; }

        [Column(name: "MEDIDA_WO")]
        public double? MedidaWo { get; set; }

        [Column(name: "VR_UNIT_WO")]
        public double? VrUnitWo { get; set; }

        [Column(name: "VR_WIRE_O")]
        public double? VrWireWo { get; set; }

        [Column(name: "RISCO_WO")]
        public double? RiscoWo { get; set; }

        [Column(name: "VRF_WO")]
        public double? VrfWo { get; set; }

        [Column(name: "COR_WO")]
        public int? CorWo { get; set; }

        [Column(name: "QTD_ESP")]
        public int? QuantEsp { get; set; }

        [Column(name: "DIAMETRO_ESP")]
        public double? DiametroEsp { get; set; }

        [Column(name: "MEDIDA_ESP")]
        public double? MedidaEsp { get; set; }

        [Column(name: "VR_UNIT_ESP")]
        public double? VrUnitEsp { get; set; }

        [Column(name: "VR_ESPIRAL")]
        public double? VrEspiral { get; set; }

        [Column(name: "RISCO_ESP")]
        public double? RiscoEsp { get; set; }

        [Column(name: "VRF_ESP")]
        public double? VrfEsp { get; set; }

        [Column(name: "COR_ESP")]
        public int? CorEsp { get; set; }

        [Column(name: "TIPO_MONTAGEM")]
        public string TipoMontagem { get; set; }

        [Column(name: "VR_MONTAGEM")]
        public double? VrMontagem { get; set; }

        [MaxLength(64)]
        [Column(name: "TIPO_OUTROS1")]
        public string TipoOutros1 { get; set; }

        [Column(name: "VR_OUTROS1")]
        public double? VrOutros1 { get; set; }

        [Column(name: "VRF_OUTROS1")]
        public double? VrfOutros1 { get; set; }

        [Column(name: "RISCO_OUTROS1")]
        public double? RiscoOutros1 { get; set; }

        [MaxLength(64)]
        [Column(name: "TIPO_OUTROS2")]
        public string TipoOutros2 { get; set; }

        [Column(name: "VR_OUTROS2")]
        public double? VrOutros2 { get; set; }

        [Column(name: "VRF_OUTROS2")]
        public double? VrfOutros2 { get; set; }

        [Column(name: "RISCO_OUTROS2")]
        public double? RiscoOutros2 { get; set; }

        [Column(name: "VR_ELASTICO")]
        public double? VrElastico { get; set; }

        [Column(name: "RISCO_ELAST")]
        public double? RiscoElast { get; set; }

        [Column(name: "VRF_ELAST")]
        public double? VrfElast { get; set; }

        [Column(name: "TIPO_ENCAD")]
        public string TipoEncad { get; set; }

        [Column(name: "QTD_ENCAD")]
        public int QuantEncad { get; set; }

        [Column(name: "VR_UNIT_ENCAD")]
        public double? VrUnitEncad { get; set; }

        [Column(name: "VR_ENCADERNACAO")]
        public double? VrEncadernacao { get; set; }

        [Column(name: "RISCO_ENCAD")]
        public double? RiscoEncad { get; set; }

        [Column(name: "VRF_ENCAD")]
        public double? VrfEncad { get; set; }

        [Column(name: "AREA_LAMINACAO")]
        public double? AreaLaminacao { get; set; }

        [Column(name: "VR_LAMINACAO")]
        public double? VrLaminacao { get; set; }

        [Column(name: "RISCO_LAM")]
        public double? RiscoLam { get; set; }

        [Column(name: "VRF_LAM")]
        public double? VrfLam { get; set; }

        [Column(name: "DOBRA_DIF")]
        public int? DobraDif { get; set; }

        [Column(name: "VR_CENTO_DOBRA")]
        public double? VrCentoDobra { get; set; }

        [Column(name: "VR_DOBRA")]
        public double? VrDobra { get; set; }

        [Column(name: "RISCO_DOBRA")]
        public double? RiscoDobra { get; set; }

        [Column(name: "VRF_DOBRA")]
        public double? VrfDobra { get; set; }

        [Column(name: "COLA_DIF")]
        public double? ColaDif { get; set; }

        [Column(name: "RISCO_COLA")]
        public double? RiscoCola { get; set; }

        [Column(name: "VR_CENTO_COLA")]
        public double? VrCentoCola { get; set; }

        [Column(name: "VR_COLA")]
        public double? VrCola { get; set; }

        [Column(name: "VRF_COLA")]
        public double? VrfCola { get; set; }

        [Column(name: "CONTRA_FMT")]
        public int? ContraFmt { get; set; }

        [Column(name: "VR_CENTO_CONTRA")]
        public double? VrCentoContra { get; set; }

        [Column(name: "VR_CONTRA")]
        public double? VrContra { get; set; }

        [Column(name: "RISCO_CONTRA")]
        public double? RiscoContra { get; set; }

        [Column(name: "VRF_CONTRA")]
        public double? VrfContra { get; set; }

        [MaxLength(64)]
        [Column(name: "TIPO_ELASTICO")]
        public string TipoElastico { get; set; }

        [Column(name: "COR_ELASTICO")]
        public int? CorElastico { get; set; }

        [Column(name: "QTD_ELASTICO")]
        public int? QuantElastico { get; set; }

        [Column(name: "MEDIDA_ELAST")]
        public int? MedidaElast { get; set; }

        [Column(name: "VR_UNIT_ELAST")]
        public double? VrUnitElast { get; set; }

        [Column(name: "VR_FRETE_ELAST")]
        public double? VrFreteElast { get; set; }

        [MaxLength(64)]
        [Column(name: "TIPO_CORDAO")]
        public string TipoCordao { get; set; }

        [Column(name: "QTD_CORDAO")]
        public int? QuantCordao { get; set; }

        [Column(name: "COR_CORDAO")]
        public int? CorCordao { get; set; }

        [Column(name: "MEDIDA_CORDAO")]
        public double? MedidaCordao { get; set; }

        [Column(name: "VR_UNIT_CORDAO")]
        public double? VrUnitCordao { get; set; }

        [Column(name: "VR_FRETE_CORDAO")]
        public double? VrFreteCordao { get; set; }

        [Column(name: "VR_CORDAO")]
        public double? VrCordao { get; set; }

        [Column(name: "RISCO_CORDAO")]
        public double? RiscoCordao { get; set; }

        [Column(name: "VRF_CORDAO")]
        public double? VrfCordao { get; set; }

        [MaxLength(32)]
        [Column(name: "TIPO_PINTURA")]
        public string TipoPintura { get; set; }

        [Column(name: "COR_PINTURA")]
        public int? CorPintura { get; set; }

        [Column(name: "VR_CENTO_PINTURA")]
        public double? VrCentoPintura { get; set; }

        [Column(name: "VR_PINTURA")]
        public double? VrPintura { get; set; }

        [Column(name: "RISCO_PINTURA")]
        public double? RiscoPintura { get; set; }

        [Column(name: "VRF_PINTURA")]
        public double? VrfPintura { get; set; }

        [Column(name: "VR_CHAPA_RS")]
        public double? VrChapaRs { get; set; }

        [Column(name: "VR_IMP_RS")]
        public double? VrImpRs { get; set; }

        [Column(name: "RISCO_RS")]
        public double? RiscoRs { get; set; }

        [Column(name: "VRF_RS")]
        public double? VrfRs { get; set; }

        [Column(name: "VR_CHAPA_HS")]
        public double? VrChapaHs { get; set; }

        [Column(name: "VR_IMP_HS")]
        public double? VrImpHs { get; set; }

        [Column(name: "RISCO_HS")]
        public double? RiscoHs { get; set; }

        [Column(name: "VRF_HS")]
        public double? VrfHs { get; set; }

        [Column(name: "VR_OFFSET")]
        public double? VrOffSet { get; set; }

        [Column(name: "RISCO_OFF")]
        public double? RiscoOff { get; set; }

        [Column(name: "VRF_OFF")]
        public double? Vrfoff { get; set; }

        [Column(name: "VR_OUTRAS_IMP")]
        public double? VrOutrasImp { get; set; }

        [Column(name: "RISCO_OI")]
        public double? RiscoOi { get; set; }

        [Column(name: "VRF_OI")]
        public double? VrfOi { get; set; }

        [Column(name: "ARTE_DIF")]
        public int? ArteDif { get; set; }

        [Column(name: "VR_ARTE")]
        public double? VrArte { get; set; }

        [Column(name: "VRF_ARTE")]
        public double? VrfArte { get; set; }

        [Column(name: "RISCO_ARTE")]
        public double? RiscoArte { get; set; }

        [Column(name: "VR_TRANSPORTE")]
        public double? VrTransporte { get; set; }

        [Column(name: "NUM_ENTMAQ")]
        public double? NumEntMaq { get; set; }

        [Column(name: "VR_ENTMAQ")]
        public double? VrEntMaq { get; set; }

        [Column(name: "MAIOR_LADO")]
        public double? MaiorLado { get; set; }

        [Column(name: "NUM_IMP")]
        public int? NumImp { get; set; }

        [Column(name: "VRU_IMP")]
        public double? VrUImp { get; set; }

        [Column(name: "VRUF_IMP")]
        public double? VrUfImp { get; set; }

        [Column(name: "VR_IMPRESSAO")]
        public double? VrImpressao { get; set; }

        [Column(name: "VR_DESPESAS_S_RISCO")]
        public double? VrDespesasSRisco { get; set; }

        [Column(name: "VR_DESPESAS_RISCO")]
        public double? VrDespesasRisco { get; set; }

        [Column(name: "VR_DESPESAS")]
        public double? VrDespesas { get; set; }

        [Column(name: "VR_RECEITAS_S_RISCO")]
        public double? VrReceitasSRisco { get; set; }

        [Column(name: "VR_RECEITAS_RISCO")]
        public double? VrREceitasRisco { get; set; }

        [Column(name: "VR_RECEITAS")]
        public double? VrReceitas { get; set; }

        [Column(name: "VR_SUBTOTAL")]
        public double? VrSubTotal { get; set; }

        [Column(name: "VR_COMISSAO")]
        public double? VrComissao { get; set; }

        [Column(name: "VR_ACRESCIMO")]
        public double? VrAcrescimo { get; set; }

        [Column(name: "VR_DESCONTO")]
        public double? VrDesconto { get; set; }

        [Column(name: "VR_IMPOSTO")]
        public double? VrImposto { get; set; }

        [Column(name: "VR_TOTAL")]
        public double? VrTotal { get; set; }
        
    }
}