using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{
    [Table("cntr_orc")]
    public class CntrOrc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Column(name: "IDORCAMENTO")]
        public int IdOrcamento { get; set; }

        [Column(name: "IDPEDIDO")]
        public int IdPedido { get; set; }
    }
}