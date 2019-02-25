using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{
    [Table("orccartelas")]
    public class OrcCartelas
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

        [Column(name: "TIPO")]
        public int? Tipo { get; set; }

        [MaxLength(1)]
        [Column(name: "LOCAL")]
        public string Local { get; set; }

        [Column(name: "QTD_IMP")]
        public int? QuantImp { get; set; }

        [Column(name: "VAR1")]
        public int? Var1 { get; set; }

        [Column(name: "VAR2")]
        public int? Var2 { get; set; }

        [Column(name: "VAR3")]
        public int? Var3 { get; set; }

        [Column(name: "VAR4")]
        public int? Var4 { get; set; }
    }
}