using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zen.Web.ViewModels.SetorViewModel
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

        [MaxLength(30, ErrorMessage = "O campo Responsavel deve ter até 30 caracteres")]
        [Required(ErrorMessage = "O campo Responsavel é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Nome")]
        public string Responsavel { get; set; }
    }
}