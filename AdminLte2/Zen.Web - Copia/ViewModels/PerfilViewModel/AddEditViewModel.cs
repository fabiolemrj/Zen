
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.ViewModels.PerfilViewModel
{
    public class AddEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required]
        [Display(Name = "Situação")]
        public Situacao Situacao { get; set; }
    }
}