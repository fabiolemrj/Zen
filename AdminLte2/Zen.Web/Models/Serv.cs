using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{
    [Table("servicos")]
    public class Serv
    {
        [Key]
        [Column(name: "IDSERVICO")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        [Column(name: "DESCR")]
        public string Nome { get; set; }

        [Column(name: "IDDESP")]
        public int? IdDesp { get; set; }

        [Column(name: "IDSUBDESP")]
        public int? IdSubDesp { get; set; }

        [NotMapped]
        public SubDespesa SubDespesa { get; set; }

        [MaxLength(50)]
        [Column(name: "CAMPO_ST")]
        public string Campo_St { get; set; }

        [MaxLength(50)]
        [Column(name: "TIPO")]
        public string Tipo { get; set; }

        [NotMapped]
        public string Despesa { get; set; }
    }
}