using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zen.Web.ViewModels.TipoTintaViewModel
{
    public class DeleteViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome")]
        [MaxLength(15, ErrorMessage = "O nome do tipo de documento deve ter até 20 caracteres")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Deseja Apagar o registro?")]
        public bool YesNo { get; set; }
    }
}