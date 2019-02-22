using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{
    [Table("orcvariacao")]
    public class OrcVariacao
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

        [MaxLength(1)]
        [Column(name: "LOCAL")]
        public string Local { get; set; }

        [Column(name: "QTD")]
        public int? Quant { get; set; }

        [MaxLength(50)]
        [Column(name: "VARIACAO")]
        public string Variacao { get; set; }
    }
}