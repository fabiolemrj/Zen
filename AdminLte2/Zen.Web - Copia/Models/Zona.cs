using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{
    [Table("zonas")]
    public class Zona
    {
        [Key]
        [Column(name: "IDZONA")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(10)]
        [Required]
        [Column(name: "DCZONA")]
        public string Nome { get; set; }

        [Column(name: "PRIORIDADE")]
        public int Prioridade { get; set; }
    }
}