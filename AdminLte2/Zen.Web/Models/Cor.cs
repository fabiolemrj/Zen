using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{
    [Table("cores")]
    public class Cor
    {
        [Key]
        [Column(name: "IDCOR")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdCor { get; set; }

        [MaxLength(30)]
        [Column(name: "NOME")]
        public string Nome { get; set; }

        [MaxLength(10)]
        [Column(name: "COR")]
        public string CorReal { get; set; }

        [NotMapped]
        public bool Selecionado { get; set; }
    }
}