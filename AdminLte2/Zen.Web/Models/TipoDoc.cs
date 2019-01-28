using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{
    [Table("tiposdoc")]
    public class TipoDoc
    {
        [Key]
        [Column(name: "IDTIPODOC")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(20)]
        [Required]
        [Column(name: "DCTIPODOC")]
        public string Nome { get; set; }
    }
}