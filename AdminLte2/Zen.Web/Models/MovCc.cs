using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{
    [Table("movcc")]
    public class MovCc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(name: "NRSEQ")]
        public int NrSeq { get; set; }

        [Required]
        [Column(name: "IDCC")]
        public int IdCc { get; set; }

        [NotMapped]
        public ContaCorrente ContaCorrente { get; set; }

        [NotMapped]
        public ContaPagar ContaPagar { get; set; }

        [NotMapped]
        public ContasReceber ContaReceber { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required]
        [Column(name: "DTMOV")]
        public DateTime DtMov { get; set; } = DateTime.Now;

        [Column(name: "VALOR")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Range(0, 9999999999999999.99)]
        public double? Valor { get; set; } = 0;

        [Column(name: "NUM_CHQ")]
        [MaxLength(15)]
        public string NumChq { get; set; }

        [Column(name: "SENTIDO")]
        [MaxLength(1)]
        public string Sentido { get; set; }

        [Column(name: "SALDO_ANT")]
        [Range(0, 9999999999999999.99)]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public double SaldoAnt { get; set; }

        [MaxLength(64)]
        [Column(name: "HIST")]
        public string Historico { get; set; }
        
        [Column(name: "IDTITULO")]
        public int? IdTitulo { get; set; }

        [Column(name: "TIPOPER")]
        [MaxLength(1)]
        public string TipoOper { get; set; }

        [Column(name: "IDLINK")]
        public int? IdLink { get; set; }

        [NotMapped]
        public TipoDoc TipoDocumento { get; set; }

        [NotMapped]
        public FormaPag Formapag { get; set; }

        [NotMapped]
        public string FornecCliente { get; set; }

        [NotMapped]
        public double SaldoAtual { get; set; } = 0;
    }
}