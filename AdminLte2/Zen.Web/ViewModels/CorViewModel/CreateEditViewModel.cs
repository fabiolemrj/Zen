using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zen.Web.ViewModels.CorViewModel
{
    public class CreateEditViewModel
    {
        [Key]
        public int IdCor { get; set; }

        [MaxLength(30, ErrorMessage = "O campo Nome deve ter até 30 caracteres")]
        [Required(ErrorMessage = "O campo Nome é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }
    }
}