using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{
    [Table("impressor")]
    public class Impressor
    {
        [Key]
        [Column(name: "IDIMPRESSOR")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [MaxLength(20)]
        [Column(name: "IMPRESSOR")]
        public string Nome { get; set; }

        [MaxLength(1)]
        [Column(name: "ATIVO")]
        public string Ativo { get; set; } = "S";
    }
}