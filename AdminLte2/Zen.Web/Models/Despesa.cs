using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{
    [Table("despesas")]
    public class Despesa
    {
        [Key]
        [Column(name: "IDDESP")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(30)]
        [Column(name: "DESCRICAO")]
        public string Nome { get; set; }
        [MaxLength(10)]
        [Column(name: "COD")]
        public string Codigo { get; set; }

        [NotMapped]
        public ICollection<SubDespesa> SubDespesas { get; set; }

   
    }
}