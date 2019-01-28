using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{
    [Table("tiporeceita")]
    public class TipoReceita
    {
        [Key]
        [Column(name: "IDTIPOREC")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(30)]
        [Required]
        [Column(name: "DCTIPOREC")]
        public string Nome { get; set; }

        [MaxLength(10)]
        [Column(name: "COD")]
        public string Codigo { get; set; }

        [Column(name: "SETOR")]
        public int Setor { get; set; } = 1;

    }
}