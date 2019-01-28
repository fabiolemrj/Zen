using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zen.Web.ViewModels.ServViewModel
{
    public class CreateEditViewModel
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "O campo Descrição deve ter até 50 caracteres")]
        [Required(ErrorMessage = "O campo Descrição é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Descrição")]
        public string Nome { get; set; }

        public int? IdDesp { get; set; }

        [Display(Name = "Despesa")]
        public int? IdSubDesp { get; set; }

        [MaxLength(50, ErrorMessage = "O campo Serviço de terceiros deve ter até 50 caracteres")]
        [Display(Name = "Serviços Terceiros")]
        public string Campo_St { get; set; }

        [MaxLength(50, ErrorMessage = "O campo Tipo deve ter até 50 caracteres")]
        [Display(Name = "Tipo")]
        public string Tipo { get; set; }
    }
}