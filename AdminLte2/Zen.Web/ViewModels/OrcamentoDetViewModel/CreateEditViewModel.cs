using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zen.Web.ViewModels.OrcamentoDetViewModel
{
    public class CreateEditViewModel
    {
        [Display(Name = "Pedido")]
        public int IdPedido { get; set; }

        [Display(Name = "Item")]
        public int Item { get; set; }
        
        [Display(Name = "Produto")]
        [Required(ErrorMessage = "O campo produto é obrigatório", AllowEmptyStrings = false)]
        public int? IdProduto { get; set; }

        [Display(Name = "Material 1")]
        [Required(ErrorMessage = "O campo Material 1 é obrigatório", AllowEmptyStrings = false)]
        public int? IdMaterial1 { get; set; }

        [Display(Name = "Material 2")]         
        public int? IdMaterial2 { get; set; }

        [Display(Name = "Material 3")]     
        public int? IdMaterial3 { get; set; }

        [Display(Name = "Material 4")]   
        public int? IdMaterial4 { get; set; }

        [Display(Name = "Relevo Seco")]
        public string RelevoSeco { get; set; }

        [Display(Name = "Fazer Chapa")]
        public string RsChapa { get; set; }

        [Display(Name = "Largura")]
        public double? LargRs { get; set; }

        [Display(Name = "Altura")]
        public double? AltRs { get; set; }

        [Display(Name = "Hot Stamp")]
        public string HotStamp { get; set; }

        [Display(Name = "Fazer Chapa")]
        public string HsChapa { get; set; }

        [Display(Name = "Largura")]
        public double? LargHs { get; set; }

        [Display(Name = "Altura")]
        public double? AltHs { get; set; }

        [Display(Name = "Largura")]
        public double? LargA { get; set; }

        [Display(Name = "Largura")]
        [Range(0.5, 250,ErrorMessage ="Valor inválido para largura")]
        public double? LargF { get; set; }

        [Display(Name = "Altura")]
        public double? AltA { get; set; }

        [Display(Name = "Altura")]
        [Range(0.5, 250, ErrorMessage = "Valor inválido para altura")]
        public double? AltF { get; set; }

        [Display(Name = "Comprimento")]
        [Range(0.5, 250, ErrorMessage = "Valor inválido para comprimento")]
        public double? CompF { get; set; }

        [Display(Name = "Quantidade")]
        [Range(1,999999,ErrorMessage ="A quantidade de itens deve ser maior que zero")]
        [Required(ErrorMessage = "O campo quantidade é obrigatório", AllowEmptyStrings = false)]
        public int? Quant { get; set; }

        [Display(Name = "Off Set")]
        public string OffSet { get; set; }

        [Display(Name = "Frente")]
        public int? OffF { get; set; }

        [Display(Name = "Verso")]
        public int? OffV { get; set; }

        [Display(Name = "Outros")]
        public string OutrosImp { get; set; }

        [Display(Name = "ObsIMp")]
        public string ObsImp { get; set; }

        [Display(Name = "Sem Acabamento")]
        public string SemAcab { get; set; }

        [Display(Name = "Frente")]
        [Required(ErrorMessage = "O campo impressão frente é obrigatório", AllowEmptyStrings = false)]
        public int ImpF { get; set; }

        [Display(Name = "Verso")]
        [Required(ErrorMessage = "O campo impressão verso é obrigatório", AllowEmptyStrings = false)]
        public int? ImpV { get; set; }

        [Display(Name = "Corte Simples")]
        [MaxLength(1, ErrorMessage = "O campo Corte simples deve ter até 1 caracter")]
        public string CorteSimples { get; set; }

        [Display(Name = "Dobra")]
        [MaxLength(1, ErrorMessage = "O campo dobra deve ter até 1 caracter")]
        public string Dobra { get; set; }

        [Display(Name = "Cola")]
        [MaxLength(1, ErrorMessage = "O campo cola deve ter até 1 caracter")]
        public string Cola { get; set; }

        [Display(Name = "Contraplacagem")]
        [MaxLength(1, ErrorMessage = "O campo contraplacagem deve ter até 1 caracter")]
        public string ContraPlaca { get; set; }

        [Display(Name = "Encadernação")]
        [MaxLength(1, ErrorMessage = "O campo encadernação deve ter até 1 caracter")]
        public string Encadernacao { get; set; }

        [Display(Name = "Elástico")]
        [MaxLength(1, ErrorMessage = "O campo elástico deve ter até 1 caracter")]
        public string Elastico { get; set; }

        [Display(Name = "Cordão")]
        [MaxLength(1, ErrorMessage = "O campo cordão deve ter até 1 caracter")]
        public string Cordao { get; set; }

        [Display(Name = "Montagem")]
        [MaxLength(1, ErrorMessage = "O campo montagem deve ter até 1 caracter")]
        public string Montagem { get; set; }

        [Display(Name = "Pintura")]
        [MaxLength(1, ErrorMessage = "O campo pintura deve ter até 1 caracter")]
        public string Pintura { get; set; }

        [Display(Name = "Outros")]
        [MaxLength(1, ErrorMessage = "O campo Outros deve ter até 1 caracter")]
        public string OutrosAcab1 { get; set; }

        [Display(Name = "Observação")]
        public string ObsAcab1 { get; set; }

        [Display(Name = "Faca")]
        [MaxLength(10, ErrorMessage = "O campo elástico deve ter até 10 caracter")]
        public string CodFaca { get; set; }

        [Display(Name = "Corte Especial")]
        [MaxLength(10, ErrorMessage = "O campo corte especial deve ter até 10 caracter")]
        public string CorteEsp { get; set; }

        [Display(Name = "Corte e Vinco")]
        [MaxLength(10, ErrorMessage = "O campo Corte e Vinco deve ter até 10 caracter")]
        public string CorteVinco { get; set; }

        [Display(Name = "Meio Corte")]
        [MaxLength(10, ErrorMessage = "O campo meio corte deve ter até 10 caracter")]
        public string MeioCorte { get; set; }

        [Display(Name = "Vinco")]
        [MaxLength(1, ErrorMessage = "O campo vinco deve ter até 1 caracter")]
        public string Vinco { get; set; }

        [Display(Name = "Frente")]
        [MaxLength(10, ErrorMessage = "O campo Laminação fosca frente deve ter até 1 caracter")]
        public string LamFoscaF { get; set; }

        [Display(Name = "Verso")]
        [MaxLength(10, ErrorMessage = "O campo laminação fosca verso deve ter até 10 caracter")]
        public string LamFoscaV { get; set; }

        [Display(Name = "Vazador")]
        [MaxLength(1, ErrorMessage = "O campo vazador deve ter até 1 caracter")]
        public int? Vazador { get; set; }

        [Display(Name = "Ilhos")]
        [MaxLength(10, ErrorMessage = "O campo Ilhos deve ter até 10 caracter")]
        public int? Ilhos { get; set; }

        [Display(Name = "Wire O")]
        [MaxLength(1, ErrorMessage = "O campo Wire O deve ter até 1 caracter")]
        public string WireO { get; set; }

        [Display(Name = "Espiral")]
        [MaxLength(1, ErrorMessage = "O campo espiral deve ter até 1 caracter")]
        public string Espiral { get; set; }

        [Display(Name = "Outros")]
        public string OutrosAcab2 { get; set; }

        public string FotoPoli { get; set; }

        public string FotoPoliFornec { get; set; }

        public string FotoRet { get; set; }

        public string FotoRetFornec { get; set; }

        public string FotoTraco { get; set; }

        public string FotoTracoFornec { get; set; }

        public string Faca { get; set; }

        public string ArteFinal { get; set; }

        public string RelevoFrances { get; set; }

        public string RfChapa { get; set; }
        public double? LargRf { get; set; }
        public double? AltRf { get; set; }
        public string Obs1 { get; set; }
    }
}