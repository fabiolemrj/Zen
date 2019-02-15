using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{
    [Table("bancos")]
    public class Banco
    {
        [Key]
        [Column(name: "IDBANCO")]
        [MaxLength(4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string IdBanco { get; set; }

        [MaxLength(30)]
        [Column(name: "NOME")]
        public string Nome { get; set; }
    }
}