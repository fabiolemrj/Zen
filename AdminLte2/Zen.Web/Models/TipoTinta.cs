using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{
    [Table("tipotinta")]
    public class TipoTinta
    {
        [Key]
        [Column(name: "IDTPTINTA")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [MaxLength(20)]
        [Required]
        [Column(name: "DCTPTINTA")]
        public string Nome { get; set; }
    }
}