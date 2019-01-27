using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdminLte2.ViewModels.ConfigEmailViewModel
{
    public class EditarViewModel
    {
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "O Remetente deve ter até 50 caracteres")]
        [Display(Name = "Remetente")]
        public string Remetente { get; set; }

        [MaxLength(50, ErrorMessage = "O Email deve ter até 50 caracteres")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(50, ErrorMessage = "O Usuário deve ter até 50 caracteres")]
        [Display(Name = "Usuário")]
        public string Usuario { get; set; }

        [MaxLength(50, ErrorMessage = "A descrição do SMTP deve ter até 50 caracteres")]
        [Display(Name = "Smtp")]
        public string EndSmtp { get; set; }

        [MaxLength(50, ErrorMessage = "A porta deve ter até 50 caracteres")]
        [Display(Name = "Porta")]
        public string PortaSmtp { get; set; }


        [MaxLength(50, ErrorMessage = "A Senha deve ter até 50 caracteres")]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
                
        [Display(Name = "Requer Autenticação")]
        public bool Autentica { get; set; }
    }
}