using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zen.Web.ViewModels.FornecedorViewModel
{
    public class CreateEditViewModel
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "O Nome deve ter até 50 caracteres")]
        [Required(ErrorMessage = "O campo Nome é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [MaxLength(50, ErrorMessage = "O Contato deve ter até 50 caracteres")]
        [Display(Name = "Contato")]
        public string Contato { get; set; }

        [MaxLength(50, ErrorMessage = "O Cargo deve ter até 50 caracteres")]
        [Display(Name = "Cargo")]
        public string Cargo { get; set; }

        [MaxLength(25, ErrorMessage = "O bairro deve ter até 25 caracteres")]
        [Display(Name = "Bairro")]
        public string Bairro { get; set; }

        [MaxLength(50, ErrorMessage = "O endereço deve ter até 50 caracteres")]
        [Display(Name = "Endereço")]
        public string End { get; set; }

        [MaxLength(10, ErrorMessage = "O CEP da Razão Social deve ter até 9 caracteres")]
        [Display(Name = "CEP")]
        public string Cep { get; set; }

        [MaxLength(30, ErrorMessage = "A cidade deve ter até 30 caracteres")]
        [Display(Name = "Cidade")]
        public string Cidade { get; set; }
        
        [MaxLength(2, ErrorMessage = "O Estado deve ter até 2 caracteres")]
        [Display(Name = "Estado")]
        public string Uf { get; set; }

        [MaxLength(15, ErrorMessage = "O telefone (1) deve ter até 15 caracteres")]
        [Display(Name = "Telefone(1)")]
        public string Tel1 { get; set; }

        [MaxLength(15, ErrorMessage = "O telefone (2) deve ter até 15 caracteres")]
        [Display(Name = "Telefone(2)")]
        public string Tel2 { get; set; }

        [MaxLength(15, ErrorMessage = "O Fax deve ter até 15 caracteres")]
        [Display(Name = "Fax")]
        public string Fax1 { get; set; }

        [MaxLength(15, ErrorMessage = "O Fax (2) deve ter até 15 caracteres")]
        [Display(Name = "Fax(2)")]
        public string Fax2 { get; set; }

        [MaxLength(50, ErrorMessage = "O endereço do Site deve ter até 50 caracteres")]
        [Display(Name = "Site")]
        public string Site { get; set; }

        [MaxLength(50, ErrorMessage = "O e-mail deve ter até 50 caracteres")]
        [Display(Name = "e-mail")]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(18, ErrorMessage = "O telefone gratuito deve ter até 18 caracteres")]
        [Display(Name = "Tel. gratuito")]
        public string Tel0800 { get; set; }

        [Display(Name = "Observação")]
        public string Obs { get; set; }

        [MaxLength(18, ErrorMessage = "A Inscrição Estadual deve ter até 18 caracteres")]
        [Display(Name = "Inscr. Estadual")]
        public string InscEstadual { get; set; }

        [MaxLength(18, ErrorMessage = "A Inscrição Municipal deve ter até 18 caracteres")]
        [Display(Name = "Inscr. Municipal")]
        public string InscMunicipal { get; set; }

        [Display(Name = "Dados OK?")]
        [MaxLength(1, ErrorMessage = "Dados OK deve ter apenas 1 caracter")]
        public string DadosOk { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Atualizado")]
        public DateTime? DtAtu { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Cadastrado")]
        public DateTime? DtIns { get; set; } = DateTime.Now;

        [Display(Name = "Ativo")]
        public string EhAtivo { get; set; }
        
        [MaxLength(15, ErrorMessage = "O Celular (1) deve ter até 15 caracteres")]
        [Display(Name = "Celular(1)")]
        public string Celular1 { get; set; }

        [MaxLength(15, ErrorMessage = "O Celular (2) deve ter até 15 caracteres")]
        [Display(Name = "Celular(2)")]
        public string Celular2 { get; set; }

        [MaxLength(50, ErrorMessage = "A Razão Social deve ter até 50 caracteres")]
        [Display(Name = "Razão Social")]
        public string RazaoSocial { get; set; }

        [MaxLength(25, ErrorMessage = "O Bairro da Razão Social deve ter até 25 caracteres")]
        [Display(Name = "Bairro")]
        public string BairroRs { get; set; }

        [MaxLength(10, ErrorMessage = "O CEP da Razão Social deve ter até 9 caracteres")]
        [Display(Name = "CEP")]
        public string CepRs { get; set; }

        [MaxLength(30, ErrorMessage = "A cidade da Razão Social deve ter até 30 caracteres")]
        [Display(Name = "Cidade")]
        public string CidadeRs { get; set; }

        [MaxLength(18, ErrorMessage = "O CNPJ deve ter até 18 caracteres")]
        [Display(Name = "CNPJ")]
        public string Cnpj { get; set; }

        [MaxLength(18, ErrorMessage = "O CPF deve ter até 18 caracteres")]
        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        [MaxLength(50, ErrorMessage = "O endereço da da Razão Social deve ter até 50 caracteres")]
        [Display(Name = "Endereço")]
        public string EndRs { get; set; }

        [MaxLength(2, ErrorMessage = "O Estado da Razão Social deve ter até 2 caracteres")]
        [Display(Name = "Estado")]
        public string UfRs { get; set; }
    }
}