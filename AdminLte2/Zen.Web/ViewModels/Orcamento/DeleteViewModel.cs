﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zen.Web.ViewModels.Orcamento
{
    public class DeleteViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe seu nome")]
        [MaxLength(100, ErrorMessage = "O nome deve ter até 100 caracteres")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Deseja Apagar o registro?")]
        public bool YesNo { get; set; }
    }
}