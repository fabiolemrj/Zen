using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zen.Web.ViewModels.ConfigViewModel
{
    public class CreateEditViewModel
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(255, ErrorMessage = "O caminho do documento deve ter até 255 caracteres")]
        //[Required(ErrorMessage = "O campo apelido é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Caminho do Documento")]
        public string CaminhoDoc { get; set; }

        [MaxLength(255, ErrorMessage = "O caminho do layout da OSI deve ter até 255 caracteres")]
        //[Required(ErrorMessage = "O campo apelido é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Caminho do Layouy da OSI")]
        public string CaminhoLayoutOsi { get; set; }

        [MaxLength(255, ErrorMessage = "O caminho do layout da OSI deve ter até 255 caracteres")]
        //[Required(ErrorMessage = "O campo apelido é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Caminho do Backup")]
        public string PathBackup { get; set; }

        [Display(Name = "ISS (%)")]        
        [Range(0, 9999999999999999.99)]
        public double? ImpIss { get; set; } = 0;

        [Display(Name = "Simples (%)")]
        [Range(0, 9999999999999999.99)]
        public double? ImpSimples { get; set; } = 0;

        [Display(Name = "Manuseio (%)")]
        [Range(0, 9999999999999999.99)]
        public double? RiscoManuseio { get; set; } = 0;

        [Display(Name = "Serviços 3 (%)")]
        [Range(0, 9999999999999999.99)]
        public double? RiscoS3 { get; set; } = 0;

        [Display(Name = "Valor até 100 unidades")]
        [Range(0, 9999999999999999.99)]
        public double? Vr100Hs { get; set; } = 0;

        [Display(Name = "Valor até 100 unidades")]
        [Range(0, 9999999999999999.99)]
        public double? Vr100Rs { get; set; } = 0;

        [Display(Name = "simples")]
        [Range(0, 9999999999999999.99)]
        public double? VrArte1 { get; set; } = 0;

        [Display(Name = "médio")]
        [Range(0, 9999999999999999.99)]
        public double? VrArte2 { get; set; } = 0;

        [Display(Name = "difícil")]
        [Range(0, 9999999999999999.99)]
        public double? VrArte3 { get; set; } = 0;

        [Display(Name = "Valor por cento a mais")]
        [Range(0, 9999999999999999.99)]
        public double? VrAdd100Hs { get; set; } = 0;

        [Display(Name = "Valor por cento a mais")]
        [Range(0, 9999999999999999.99)]
        public double? VrAdd100Rs { get; set; } = 0;

        [Display(Name = "Clichê 5 x 10")]
        [Range(0, 9999999999999999.99)]
        public double? VrCliche1Hs { get; set; } = 0;

        [Display(Name = "Clichê 10 x 10")]
        [Range(0, 9999999999999999.99)]
        public double? VrCliche2Hs { get; set; } = 0;

        [Display(Name = "Clichê 15 x 10")]
        [Range(0, 9999999999999999.99)]
        public double? VrCliche3Hs { get; set; } = 0;

        [Display(Name = "Clichê 5 x 10")]
        [Range(0, 9999999999999999.99)]
        public double? VrCliche1Rs { get; set; } = 0;

        [Display(Name = "Clichê 10 x 10")]
        [Range(0, 9999999999999999.99)]
        public double? VrCliche2Rs { get; set; } = 0;

        [Display(Name = "Clichê 15 x 10")]
        [Range(0, 9999999999999999.99)]
        public double? VrCliche3Rs { get; set; } = 0;

        [Display(Name = "grau 1")]
        [Range(0, 9999999999999999.99)]
        public double? VrCola1 { get; set; } = 0;

        [Display(Name = "grau 2")]
        [Range(0, 9999999999999999.99)]
        public double? VrCola2 { get; set; } = 0;

        [Display(Name = "grau 3")]
        [Range(0, 9999999999999999.99)]
        public double? VrCola3 { get; set; } = 0;

        [Display(Name = "21 cm  X  30 cm")]
        [Range(0, 9999999999999999.99)]
        public double? VrContra1 { get; set; } = 0;

        [Display(Name = "30 cm  X  45 cm")]
        [Range(0, 9999999999999999.99)]
        public double? VrContra2 { get; set; } = 0;

        [Display(Name = "30 cm  X  45 cm")]
        [Range(0, 9999999999999999.99)]
        public double? VrContra3 { get; set; } = 0;

        [Display(Name = "grau 1")]
        [Range(0, 9999999999999999.99)]
        public double? VrDobra1 { get; set; } = 0;

        [Display(Name = "grau 2")]
        [Range(0, 9999999999999999.99)]
        public double? VrDobra2 { get; set; } = 0;

        [Display(Name = "grau 3")]
        [Range(0, 9999999999999999.99)]
        public double? VrDobra3 { get; set; } = 0;

        [Display(Name = "Grande S ou M")]
        [Range(0, 9999999999999999.99)]
        public double? VrEntrMaqG { get; set; } = 0;

        [Display(Name = "Médio S ou M")]
        [Range(0, 9999999999999999.99)]
        public double? VrEntrMaqM { get; set; } = 0;

        [Display(Name = "Pequeno S ou M")]
        [Range(0, 9999999999999999.99)]
        public double? VrEntrMaqP { get; set; } = 0;

        [Display(Name = "até 15,0 x 21,0")]
        [Range(0, 9999999999999999.99)]
        public double? VrFoto1 { get; set; } = 0;

        [Display(Name = "até 21,0 x 29,7")]
        [Range(0, 9999999999999999.99)]
        public double? VrFoto2 { get; set; } = 0;

        [Display(Name = "até 29,7 x 42,0")]
        [Range(0, 9999999999999999.99)]
        public double? VrFoto3 { get; set; } = 0;

        [Display(Name = "Manual (G)")]
        [Range(0, 9999999999999999.99)]
        public double? VrImpMg { get; set; } = 0;

        [Display(Name = "Manual (M)")]
        [Range(0, 9999999999999999.99)]
        public double? VrImpMm { get; set; } = 0;

        [Display(Name = "Manual (P)")]
        [Range(0, 9999999999999999.99)]
        public double? VrImpMp { get; set; } = 0;

        [Display(Name = "Semi-automática (G)")]
        [Range(0, 9999999999999999.99)]
        public double? VrImpSag { get; set; } = 0;

        [Display(Name = "Semi-automática (M)")]
        [Range(0, 9999999999999999.99)]
        public double? VrImpSam { get; set; } = 0;

        [Display(Name = "Semi-automática (P)")]
        [Range(0, 9999999999999999.99)]
        public double? VrImpSap { get; set; } = 0;

        [Display(Name = "Semi-manual")]
        [Range(0, 9999999999999999.99)]
        public double? VrImpSm { get; set; } = 0;
              
        [Range(0, 9999999999999999.99)]
        public double? VrImpDia1 { get; set; } = 0;
             
        [Range(0, 9999999999999999.99)]
        public double? VrImpDia2 { get; set; } = 0;
               
        [Range(0, 9999999999999999.99)]
        public double? VrImpDia3 { get; set; } = 0;
               
        [Range(0, 9999999999999999.99)]
        public double? VrImpDiaMin { get; set; } = 0;

        [Display(Name = "Valor do cm da lâmina comum")]
        [Range(0, 9999999999999999.99)]
        public double? VrLamComum { get; set; } = 0;

        [Display(Name = "Valor do cm da lâmina especial")]
        [Range(0, 9999999999999999.99)]
        public double? VrLamEspecial { get; set; } = 0;

        [Display(Name = "até 3 milheiros")]
        [Range(0, 9999999999999999.99)]
        public double? VrLaminacao1 { get; set; } = 0;

        [Display(Name = "acima de 3 milheiros")]
        [Range(0, 9999999999999999.99)]
        public double? VrLaminacao2 { get; set; } = 0;
      
        [Range(0, 9999999999999999.99)]
        public double? VrLaminacao3 { get; set; } = 0;

        [Display(Name = "Custo mínimo para a faca")]
        [Range(0, 9999999999999999.99)]
        public double? VrMinFaca { get; set; } = 0;

        [Display(Name = "Custo mínimo para a faca")]
        [Range(0, 9999999999999999.99)]
        public double? VrMinFoto { get; set; } = 0;

        [Display(Name = "Corte/Vinco")]
        [Range(0, 9999999999999999.99)]
        public double? VrUsoFaca { get; set; } = 0;

        [Display(Name = "Corte/Vinco acima de 40 x 60")]
        [Range(0, 9999999999999999.99)]
        public double? VrUsoFaca4060 { get; set; } = 0;

        [Display(Name = "Corte/Vinco acima de 40 x 60 (meio corte)")]
        [Range(0, 9999999999999999.99)]
        public double? VrUsoFaca4060Meio { get; set; } = 0;

        [Display(Name = "Valor do cm da lâmina especial")]
        [Range(0, 9999999999999999.99)]
        public double? VrUsoFacaEspec { get; set; } = 0;

        [Display(Name = "Corte/Vinco até 40 x 60 (meio corte)")]
        [Range(0, 9999999999999999.99)]
        public double? VrUsoFacaMeio { get; set; } = 0;

        [Display(Name = "Valor unitário do vazador")]
        [Range(0, 9999999999999999.99)]
        public double? VrVazador { get; set; } = 0;
    }
}