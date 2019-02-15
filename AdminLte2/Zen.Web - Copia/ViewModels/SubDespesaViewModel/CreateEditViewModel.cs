using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.ViewModels.SubDespesaViewModel
{
    public class CreateEditViewModel
    {
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }
                
        public int idDesp { get; set; }
                
        public int IdSubDesp { get; set; }

        [MaxLength(30, ErrorMessage = "O Nome deve ter até 30 caracteres")]
        [Required(ErrorMessage = "O campo Nome é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [MaxLength(10, ErrorMessage = "O Codigo deve ter até 10 caracteres")]
        [Required(ErrorMessage = "O Codigo Nome é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Código")]
        public string Codigo { get; set; }

        [Display(Name = "Despesa")]
        public string NmDespesa { get; set; }
    }
}