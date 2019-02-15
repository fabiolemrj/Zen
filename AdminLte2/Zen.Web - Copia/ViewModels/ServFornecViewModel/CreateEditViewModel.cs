using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zen.Web.ViewModels.ServFornecViewModel
{
    public class CreateEditViewModel
    {
        [Key]
        [Display(Name = "Serviço")]
        [Required(ErrorMessage = "O campo serviço é obrigatório", AllowEmptyStrings = false)]
        public int IdServico { get; set; }

        [Key]
        public int IdFornecedor { get; set; }
               
        [Display(Name = "Fornecedor")]
        public string Fornecedor { get; set; }

    }
}