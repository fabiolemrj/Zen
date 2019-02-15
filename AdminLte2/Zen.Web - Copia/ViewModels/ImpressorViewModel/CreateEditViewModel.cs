using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zen.Web.ViewModels.ImpressorViewModel
{
    public class CreateEditViewModel
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(20, ErrorMessage = "O nome deve ter até 20 caracteres")]
        [Required(ErrorMessage = "O campo nome é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Ativo")]
        [Required(ErrorMessage = "O campo ativo é obrigatório", AllowEmptyStrings = false)]
        public string Ativo { get; set; }

    }
}