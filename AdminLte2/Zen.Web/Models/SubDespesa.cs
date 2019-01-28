using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{
    [Table("subdespesas")]
    public class SubDespesa
    {
        [Key]
        [Column(name: "IDITEM")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

       
        [Column(name: "IDDESP")]        
        public int IdDesp { get; set; }
        [NotMapped]
        public Despesa Despesa { get; set; }

        [Column(name: "IDSUBDESP")]
        public int IdSubDesp { get; set; }
        [MaxLength(30)]
        [Column(name: "DESCRICAO")]
        public string Nome { get; set; }
        [MaxLength(10)]
        [Column(name: "COD")]
        public string Codigo { get; set; }

      
    }
}