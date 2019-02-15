using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zen.Web.ViewModels.SetorViewModel
{
    public class DeleteViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome do setor")]
        [MaxLength(15, ErrorMessage = "O nome do setor deve ter até 30 caracteres")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Deseja Apagar o registro?")]
        public bool YesNo { get; set; }
    }
}