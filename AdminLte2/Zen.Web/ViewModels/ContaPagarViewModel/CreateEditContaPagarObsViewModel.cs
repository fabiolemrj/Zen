using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zen.Web.ViewModels.ContaPagarViewModel
{
    public class CreateEditContaPagarObsViewModel
    {
        [Display(Name = "Id")]
        public int IdTitulo { get; set; }
        
        [MaxLength(150, ErrorMessage = "O campo Produto deve ter até 150 caracter")]
        [Display(Name = "Produto")]
        public string Produto { get; set; }

        [MaxLength(150, ErrorMessage = "O campo Produto deve ter até 150 caracter")]
        [Display(Name = "Quantidade")]
        public string Quantidade { get; set; }

        [MaxLength(150, ErrorMessage = "O campo Marca deve ter até 150 caracter")]
        [Display(Name = "Marca")]
        public string Marca { get; set; }

        [MaxLength(150, ErrorMessage = "O campo Valor deve ter até 150 caracter")]
        [Display(Name = "Valor")]
        public string Valor { get; set; }
    }
}