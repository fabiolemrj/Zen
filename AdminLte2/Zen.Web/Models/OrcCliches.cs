using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{
    [Table("orccliches")]
    public class OrcCliches
    {
        [Key]
        [Column(name: "IDPEDIDO", Order = 1)]
        public int IdPedido { get; set; }

        [Key]
        [Column(name: "ITEM", Order = 2)]
        public int Item { get; set; }

        [Key]
        [Column(name: "NRSEQ", Order = 3)]
        public int NrSeq { get; set; }
        
        [Column(name: "QTD")]
        public int? Quant { get; set; }
        
        [Column(name: "LARG")]
        public double? Larg { get; set; }

        [Column(name: "ALT")]
        public double? Alt { get; set; }

        [Column(name: "VALOR")]
        public double? Valor { get; set; }

        [MaxLength(1)]
        [Column(name: "FORNEC")]
        public string Fornec { get; set; }

        [MaxLength(1)]
        [Column(name: "FIXO")]
        public string Fixo { get; set; }

        [MaxLength(1)]
        [Column(name: "Tipo")]
        public string Tipo { get; set; }
    }
}