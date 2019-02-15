using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zen.Web.ViewModels.FacasViewModel
{
    public class CreateModelView
    {
        [Key]
        [Display(Name = "Id")]        
        public int IdBanco { get; set; }

        [MaxLength(15, ErrorMessage = "O codigo da faca deve ter até 15 caracteres")]
        [Required(ErrorMessage = "O campo Nome é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Codigo da Faca")]
        public string Nome { get; set; }
    }
}