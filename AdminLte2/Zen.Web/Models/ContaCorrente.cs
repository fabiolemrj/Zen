using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{
    [Table("contascorrentes")]
    public class ContaCorrente
    {
        [Key]
        [Column(name: "IDCC")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(20)]      
        [Column(name: "NMAGENCIA")]
        public string NomeAgencia { get; set; }
        
        [MaxLength(1)]
        [Column(name: "CT_INVEST")]
        public string Investimento { get; set; }

        [Column(name: "DT_LANC")]
        public DateTime? DtLanc { get; set; } = DateTime.Now;

        [MaxLength(30)]
        [Column(name: "IDBANCO")]
        public string IdBanco { get; set; }

        [MaxLength(20)]
        [Column(name: "NUM_CC")]
        public string NumeroConta { get; set; }
                
        [Column(name: "NUMAGENCIA")]
        public int NumeroAgencia { get; set; }

        [Column(name: "SALDO_ATU")]
        [Range(0, 9999999999999999.99)]
        public double? SaldoAtual { get; set; } = 0;

        [Column(name: "SALDO_INI")]
        [Range(0, 9999999999999999.99)]
        public double? SaldoIni { get; set; } = 0;

        [NotMapped]
        public Banco Banco { get; set; }

    }
}