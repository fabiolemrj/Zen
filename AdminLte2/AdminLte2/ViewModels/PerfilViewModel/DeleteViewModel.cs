using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminLte2.ViewModels.PerfilViewModel
{
    public class DeleteViewModel
    {
        [Key]
        public int Id { get; set; }
              
        [MaxLength(100, ErrorMessage = "O nome deve ter até 100 caracteres")]
        [Display(Name = "Nome")]
        public string Descricao { get; set; }

        [Display(Name = "Deseja Apagar o registro")]
        public string Mensagem { get; set; }

        [Display(Name = "Deseja Apagar o registro?")]
        public bool YesNo { get; set; }
    }
}