using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zen.Web.ViewModels.AutenticacaoViewModel
{
    public class ForgotPasswordViewModel
    {
        [Display(Name = "Informe o seu e-mail para resetar senha")]
        [Required(ErrorMessage = "Email Obrigatório")]
        [EmailAddress]
        public string Email { get; set; }
    }
}