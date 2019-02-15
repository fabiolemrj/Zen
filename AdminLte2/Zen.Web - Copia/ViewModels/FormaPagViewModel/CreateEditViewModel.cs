using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zen.Web.ViewModels.FormaPagViewModel
{
    public class CreateEditViewModel
    {
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [MaxLength(20, ErrorMessage = "O nome da forma de pagamento deve ter até 20 caracteres")]
        [Required(ErrorMessage = "O nome da forma de pagamento é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }
    }
}