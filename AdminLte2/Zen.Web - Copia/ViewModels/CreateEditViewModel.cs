using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zen.Web.ViewModels.TipoReceitaViewModel
{
    public class CreateEditViewModel
    {
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [MaxLength(30, ErrorMessage = "O campo Nome deve ter até 30 caracteres")]
        [Required(ErrorMessage = "O campo Nome é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [MaxLength(10, ErrorMessage = "O campo Codigo deve ter até 30 caracteres")]
        [Required(ErrorMessage = "O campo Codigo é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Codigo")]
        public string Codigo { get; set; }
    }
}