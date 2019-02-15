using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{
    [Table("cod_facas")]
    public class Facas
    {
        [Key]
        [Column(name: "IDFACA")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdFaca { get; set; }
        [MaxLength(15)]
        [Column(name: "COD_FACA")]
        public string Cod_Faca { get; set; }
    }
}