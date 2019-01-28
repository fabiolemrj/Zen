using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zen.Web.ViewModels.BancoViewModel
{
    public class CreateEditViewModel
    {
        [Key]
        [Display(Name = "Codigo")]
        [MaxLength(4, ErrorMessage = "O campo codigo deve ter até 4 caracteres")]
        [Required(ErrorMessage = "O campo Código é obrigatório", AllowEmptyStrings = false)]
        public string IdBanco { get; set; }

        [MaxLength(30, ErrorMessage = "O campo Nome deve ter até 30 caracteres")]
        [Required(ErrorMessage = "O campo Nome é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }
    }
}