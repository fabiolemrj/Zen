using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.ViewModels.MaquinaViewModel
{
    public class CreateEditViewModel
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(30, ErrorMessage = "O campo Nome deve ter até 30 caracteres")]
        [Required(ErrorMessage = "O campo Nome é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Maquina")]
        public string Nome { get; set; }
                
        public Impressor Impressor { get; set; }
        [Display(Name = "Impressor")]
        [Required(ErrorMessage = "O campo impressor é obrigatório", AllowEmptyStrings = false)]
        public int IdImpressor { get; set; }
               
        public Impressor Auxiliar { get; set; }
        [Display(Name = "Auxiliar")]
        [Required(ErrorMessage = "O campo auxiliar é obrigatório", AllowEmptyStrings = false)]
        public int IdAuxiliar { get; set; }

        [Display(Name = "Situação")]
        [Required(ErrorMessage = "O campo situação é obrigatório", AllowEmptyStrings = false)]
        public string Situacao { get; set; }

        [Display(Name = "Classificacao")]
        [Required(ErrorMessage = "O campo classficacao é obrigatório", AllowEmptyStrings = false)]
        public string Classificacao { get; set; }
    }
}