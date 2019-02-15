
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.ViewModels.UsuarioViewModel
{
    public class AddEditViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe seu nome")]
        [MaxLength(100, ErrorMessage = "O nome deve ter até 100 caracteres")]
        [Display(Name = "Nome")]
        public string NmUsuario { get; set; }

        [Required(ErrorMessage = "Informe seu Login")]
        [MaxLength(50, ErrorMessage = "O login deve ter até 50 caracteres")]
        [MinLength(3, ErrorMessage = "O Login deve ter no mínimo 03 caracteres")]
        public string Login { get; set; }

        //[Required(ErrorMessage = "Informe sua senha")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6" + "caracteres")]
        public string Senha { get; set; }

        //[Required(ErrorMessage = "Confirme sua senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6" + "caracteres")]
        [Compare(nameof(Senha), ErrorMessage = "A senha e a" + "confirmação não estão iguais")]
        public string ConfirmacaoSenha { get; set; }

        [Display(Name = "Tipo de Acesso")]
        [Required(ErrorMessage = "Tipo de Acesso Obrigatório")]
        public TipoUsuario TipoAcesso { get; set; }

        [Display(Name = "Situação")]
        [Required(ErrorMessage = "Situação Obrigatória")]
        public Situacao Situacao { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email Obrigatório")]    
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Função")]
        public string Funcao { get; set; }

        public virtual Perfil Perfil { get; set; }

        [Required(ErrorMessage = "Perfil Obrigatório")]
        [Display(Name = "Perfil")]
        public int IdPerfil { get; set; }
    }
}