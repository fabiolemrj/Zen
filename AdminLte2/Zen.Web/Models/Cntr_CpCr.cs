using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{
    [Table("cntr_cpcr")]
    public class Cntr_CpCr
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Column(name: "IDCR")]
        public int IdCr { get; set; }

        [NotMapped]
        public ContasReceber ContaReceber { get; set; }

        [Column(name: "IDCP")]
        public int IdCp { get; set; }

        [NotMapped]
        public ContaPagar ContaPagar { get; set; }
    }
}