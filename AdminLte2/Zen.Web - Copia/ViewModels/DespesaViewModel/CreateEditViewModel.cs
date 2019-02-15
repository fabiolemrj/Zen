using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zen.Web.ViewModels.DespesaViewModel
{
    public class CreateEditViewModel
    {
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [MaxLength(30, ErrorMessage = "O Nome deve ter até 30 caracteres")]
        [Required(ErrorMessage = "O campo Nome é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [MaxLength(10, ErrorMessage = "O código deve ter até 10 caracteres")]
        [Required(ErrorMessage = "O campo Código é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Código")]
        public string Codigo { get; set; }

    }
}