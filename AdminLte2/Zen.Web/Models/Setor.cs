using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{
    [Table("setores")]
    public class Setor
    {
        [Key]
        [Column(name: "IDSETOR")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [MaxLength(30)]
        [Required]
        [Column(name: "DCSETOR")]
        public string Nome { get; set; }

        [MaxLength(30)]      
        [Column(name: "RESPONSAVEL")]
        public string Responsavel { get; set; }
    }
}