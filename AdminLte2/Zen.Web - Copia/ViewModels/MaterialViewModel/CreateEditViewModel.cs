using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zen.Web.ViewModels.MaterialViewModel
{
    public class CreateEditViewModel
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "O campo Nome deve ter até 50 caracteres")]
        [Required(ErrorMessage = "O campo Nome é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [MaxLength(50, ErrorMessage = "O campo Nome deve ter até 50 caracteres")]
        [Display(Name = "Fabricante")]
        public string Fabricante { get; set; }

        [MaxLength(1, ErrorMessage = "O campo Fora de Linha deve ter até 50 caracteres")]
        [Display(Name = "Fora de Linha")]
        public string Fora { get; set; }

        [MaxLength(1, ErrorMessage = "O campo Nome deve ter até 50 caracteres")]
        [Display(Name = "Tipo da lamnina")]
        public string TipoLam { get; set; }

        [Display(Name = "Altura")]
        public double Alt { get; set; } 

        [Display(Name = "Largura")]
        public double Larg { get; set; } 

        [Display(Name = "Valor Total")]
        [Range(0, 9999999999999999.99)]
        public double ValorTotal { get; set; } 

        [Display(Name = "Valor Unitário")]
        [Range(0, 9999999999999999.99)]
        public double ValorUnit { get; set; } 

        [Display(Name = "Fornecedor")]
        public int IdFornecedor { get; set; }

        [Display(Name = "Nomero de folhas")]
        public int NumFolha { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data Atualização")]
        public DateTime DtAtu { get; set; }



    }
}