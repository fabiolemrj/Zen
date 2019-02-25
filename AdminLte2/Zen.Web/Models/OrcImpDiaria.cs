﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{
    [Table("orcimpdiaria")]
    public class OrcImpDiaria
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

        [Column(name: "TP_IMP_DIA")]
        public int? TpImpDia { get; set; }

        [Column(name: "QTD_IMP_DIA")]
        public int? QuantImpDia { get; set; }
        
        [Column(name: "VALOR")]
        public double? Valor { get; set; }

        [MaxLength(1)]
        [Column(name: "FIXO")]
        public string Fixo { get; set; }
    }
}