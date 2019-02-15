using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zen.Web.ViewModels.ServViewModel
{
    public class DeleteViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe a Descrição")]
        [MaxLength(100, ErrorMessage = "O Descrição deve ter até 50 caracteres")]
        [Display(Name = "Descrição")]
        public string Nome { get; set; }

        [Display(Name = "Deseja Apagar o registro?")]
        public bool YesNo { get; set; }
    }
}