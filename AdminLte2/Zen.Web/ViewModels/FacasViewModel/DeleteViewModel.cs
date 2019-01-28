using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zen.Web.ViewModels.FacasViewModel
{
    public class DeleteViewModel
    {
        [Key]
        public int IdFaca { get; set; }

        [Required(ErrorMessage = "Informe o codigo da faca")]
        [MaxLength(15, ErrorMessage = "O codigo da faca deve ter até 100 caracteres")]
        [Display(Name = "Codigo da faca")]
        public string Nome { get; set; }

        [Display(Name = "Deseja Apagar o registro?")]
        public bool YesNo { get; set; }
    }
}