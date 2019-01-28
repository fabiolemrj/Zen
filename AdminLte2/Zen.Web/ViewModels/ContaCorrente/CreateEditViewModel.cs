using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zen.Web.ViewModels.ContaCorrente
{
    public class CreateEditViewModel
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(20, ErrorMessage = "O Nome da agencia deve ter até 50 caracteres")]
        [Required(ErrorMessage = "O campo Nome da agencia é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Nome da Agencia")]
        public string NomeAgencia { get; set; }

        [MaxLength(1, ErrorMessage = "O Investivmento deve ter apenas 1 caracter")]
        [Display(Name = "Investimento?")]
        public string Investimento { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Último Lançamento")]
        public DateTime? DtLanc { get; set; } = DateTime.Now;

        [MaxLength(4, ErrorMessage = "O Numero do banco deve ter até 4 caracteres")]
        [Required(ErrorMessage = "O campo Numero do banco é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Banco")]
        public string IdBanco { get; set; }

        [MaxLength(15, ErrorMessage = "O Conta Corrente deve ter até 15 caracteres")]
        [Required(ErrorMessage = "O campo Conta Corrente é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Conta Corrente")]
        public string NumeroConta { get; set; }

        [Required(ErrorMessage = "O campo Numero da agencia é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Numero da Agencia")]
        public int NumeroAgencia { get; set; }

        [Display(Name = "Saldo Autal")]
        [Range(0, 9999999999999999.99)]
        public double? SaldoAtual { get; set; }

        [Display(Name = "Saldo Inicial")]
        [Range(0, 9999999999999999.99)]
        public double? SaldoIni { get; set; }

    }
}