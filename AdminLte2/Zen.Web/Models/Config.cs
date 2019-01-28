using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{

    [Table("config")]
    public class Config
    {
        [Key]
        [Column(name: "IDCONFIG")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(255)]
        [Column(name: "CAMINHO_DOC")]
        public string CaminhoDoc { get; set; }

        [MaxLength(255)]
        [Column(name: "CAMINHO_LAYOUT_OSI")]
        public string CaminhoLayoutOsi { get; set; }

        [MaxLength(255)]
        [Column(name: "PATH_BACKUP")]
        public string PathBackup { get; set; }

        [Column(name: "IMP_ISS")]
        [Range(0, 9999999999999999.99)]
        public double? ImpIss { get; set; } = 0;

        [Column(name: "IMP_SIMPLES")]
        [Range(0, 9999999999999999.99)]
        public double? ImpSimples { get; set; } = 0;

        [Column(name: "RISCO_M")]
        [Range(0, 9999999999999999.99)]
        public double? RiscoManuseio { get; set; } = 0;

        [Column(name: "RISCO_S3")]
        [Range(0, 9999999999999999.99)]
        public double? RiscoS3 { get; set; } = 0;

        [Column(name: "VR100_HS")]
        [Range(0, 9999999999999999.99)]
        public double? Vr100Hs { get; set; } = 0;

        [Column(name: "VR100_RS")]
        [Range(0, 9999999999999999.99)]
        public double? Vr100Rs { get; set; } = 0;

        [Column(name: "VRARTE1")]
        [Range(0, 9999999999999999.99)]
        public double? VrArte1 { get; set; } = 0;

        [Column(name: "VRARTE2")]
        [Range(0, 9999999999999999.99)]
        public double? VrArte2 { get; set; } = 0;

        [Column(name: "VRARTE3")]
        [Range(0, 9999999999999999.99)]
        public double? VrArte3 { get; set; } = 0;

        [Column(name: "VRADD100_HS")]
        [Range(0, 9999999999999999.99)]
        public double? VrAdd100Hs { get; set; } = 0;

        [Column(name: "VRADD100_RS")]
        [Range(0, 9999999999999999.99)]
        public double? VrAdd100Rs { get; set; } = 0;

        [Column(name: "VRCLICHE1_HS")]
        [Range(0, 9999999999999999.99)]
        public double? VrCliche1Hs { get; set; } = 0;

        [Column(name: "VRCLICHE2_HS")]
        [Range(0, 9999999999999999.99)]
        public double? VrCliche2Hs { get; set; } = 0;

        [Column(name: "VRCLICHE3_HS")]
        [Range(0, 9999999999999999.99)]
        public double? VrCliche3Hs { get; set; } = 0;

        [Column(name: "VRCLICHE1_RS")]
        [Range(0, 9999999999999999.99)]
        public double? VrCliche1Rs { get; set; } = 0;

        [Column(name: "VRCLICHE2_RS")]
        [Range(0, 9999999999999999.99)]
        public double? VrCliche2Rs { get; set; } = 0;

        [Column(name: "VRCLICHE3_RS")]
        [Range(0, 9999999999999999.99)]
        public double? VrCliche3Rs { get; set; } = 0;

        [Column(name: "VRCOLA1")]
        [Range(0, 9999999999999999.99)]
        public double? VrCola1 { get; set; } = 0;

        [Column(name: "VRCOLA2")]
        [Range(0, 9999999999999999.99)]
        public double? VrCola2 { get; set; } = 0;

        [Column(name: "VRCOLA3")]
        [Range(0, 9999999999999999.99)]
        public double? VrCola3 { get; set; } = 0;

        [Column(name: "VRCONTRA1")]
        [Range(0, 9999999999999999.99)]
        public double? VrContra1 { get; set; } = 0;

        [Column(name: "VRCONTRA2")]
        [Range(0, 9999999999999999.99)]
        public double? VrContra2 { get; set; } = 0;

        [Column(name: "VRCONTRA3")]
        [Range(0, 9999999999999999.99)]
        public double? VrContra3 { get; set; } = 0;

        [Column(name: "VRDOBRA1")]
        [Range(0, 9999999999999999.99)]
        public double? VrDobra1 { get; set; } = 0;

        [Column(name: "VRDOBRA2")]
        [Range(0, 9999999999999999.99)]
        public double? VrDobra2 { get; set; } = 0;

        [Column(name: "VRDOBRA3")]
        [Range(0, 9999999999999999.99)]
        public double? VrDobra3 { get; set; } = 0;

        [Column(name: "VRENTRMAQ_G")]
        [Range(0, 9999999999999999.99)]
        public double? VrEntrMaqG { get; set; } = 0;

        [Column(name: "VRENTRMAQ_M")]
        [Range(0, 9999999999999999.99)]
        public double? VrEntrMaqM { get; set; } = 0;

        [Column(name: "VRENTRMAQ_P")]
        [Range(0, 9999999999999999.99)]
        public double? VrEntrMaqP { get; set; } = 0;

        [Column(name: "VRFOTO1")]
        [Range(0, 9999999999999999.99)]
        public double? VrFoto1 { get; set; } = 0;

        [Column(name: "VRFOTO2")]
        [Range(0, 9999999999999999.99)]
        public double? VrFoto2 { get; set; } = 0;

        [Column(name: "VRFOTO3")]
        [Range(0, 9999999999999999.99)]
        public double? VrFoto3 { get; set; } = 0;

        [Column(name: "VRIMP_MG")]
        [Range(0, 9999999999999999.99)]
        public double? VrImpMg { get; set; } = 0;

        [Column(name: "VRIMP_MM")]
        [Range(0, 9999999999999999.99)]
        public double? VrImpMm { get; set; } = 0;

        [Column(name: "VRIMP_MP")]
        [Range(0, 9999999999999999.99)]
        public double? VrImpMp { get; set; } = 0;

        [Column(name: "VRIMP_SAG")]
        [Range(0, 9999999999999999.99)]
        public double? VrImpSag { get; set; } = 0;

        [Column(name: "VRIMP_SAM")]
        [Range(0, 9999999999999999.99)]
        public double? VrImpSam { get; set; } = 0;

        [Column(name: "VRIMP_SAP")]
        [Range(0, 9999999999999999.99)]
        public double? VrImpSap { get; set; } = 0;

        [Column(name: "VRIMP_SM")]
        [Range(0, 9999999999999999.99)]
        public double? VrImpSm { get; set; } = 0;

        [Column(name: "VRIMPDIA1")]
        [Range(0, 9999999999999999.99)]
        public double? VrImpDia1 { get; set; } = 0;

        [Column(name: "VRIMPDIA2")]
        [Range(0, 9999999999999999.99)]
        public double? VrImpDia2 { get; set; } = 0;

        [Column(name: "VRIMPDIA3")]
        [Range(0, 9999999999999999.99)]
        public double? VrImpDia3 { get; set; } = 0;

        [Column(name: "VRIMPDIAMIN")]
        [Range(0, 9999999999999999.99)]
        public double? VrImpDiaMin { get; set; } = 0;

        [Column(name: "VRLAMCOMUM")]
        [Range(0, 9999999999999999.99)]
        public double? VrLamComum { get; set; } = 0;

        [Column(name: "VRLAMESPECIAL")]
        [Range(0, 9999999999999999.99)]
        public double? VrLamEspecial { get; set; } = 0;

        [Column(name: "VRLAMINACAO1")]
        [Range(0, 9999999999999999.99)]
        public double? VrLaminacao1 { get; set; } = 0;

        [Column(name: "VRLAMINACAO2")]
        [Range(0, 9999999999999999.99)]
        public double? VrLaminacao2 { get; set; } = 0;

        [Column(name: "VRLAMINACAO3")]
        [Range(0, 9999999999999999.99)]
        public double? VrLaminacao3 { get; set; } = 0;

        [Column(name: "VRMINFACA")]
        [Range(0, 9999999999999999.99)]
        public double? VrMinFaca { get; set; } = 0;

        [Column(name: "VRMINFOTO")]
        [Range(0, 9999999999999999.99)]
        public double? VrMinFoto { get; set; } = 0;

        [Column(name: "VRUSOFACA")]
        [Range(0, 9999999999999999.99)]
        public double? VrUsoFaca { get; set; } = 0;

        [Column(name: "VRUSOFACA4060")]
        [Range(0, 9999999999999999.99)]
        public double? VrUsoFaca4060 { get; set; } = 0;

        [Column(name: "VRUSOFACA4060MEIO")]
        [Range(0, 9999999999999999.99)]
        public double? VrUsoFaca4060Meio { get; set; } = 0;

        [Column(name: "VRUSOFACAESPEC")]
        [Range(0, 9999999999999999.99)]
        public double? VrUsoFacaEspec { get; set; } = 0;

        [Column(name: "VRUSOFACAMEIO")]
        [Range(0, 9999999999999999.99)]
        public double? VrUsoFacaMeio { get; set; } = 0;

        [Column(name: "VRVAZADOR")]
        [Range(0, 9999999999999999.99)]
        public double? VrVazador { get; set; } = 0;
    }

}