using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{
    [Table("transportes")]
    public class Transporte
    {
        [Key]
        [Column(name: "IDTRANSPORTE")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(25)]
        [Required]
        [Column(name: "DCTRANSPORTE")]
        public string Nome { get; set; }
    }
}