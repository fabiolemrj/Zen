using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zen.Web.ViewModels.CorViewModel
{
    public class DeleteViewModel
    {
        [Key]
        public int IdCor { get; set; }

        [Required(ErrorMessage = "Informe o nome")]
        [MaxLength(30, ErrorMessage = "O nome deve ter até 30 caracteres")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Deseja Apagar o registro?")]
        public bool YesNo { get; set; }
    }
}