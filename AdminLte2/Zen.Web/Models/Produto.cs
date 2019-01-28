using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{
    [Table("produtos")]
    public class Produto
    {
        [Key]
        [Column(name: "IDPRODUTO")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [MaxLength(30)]
        [Required]
        [Column(name: "DCPRODUTO")]
        public string Nome { get; set; }
    }
}