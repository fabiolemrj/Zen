using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{
    [Table("formapgm")]
    public class FormaPag
    {
        [Key]
        [Column(name: "IDFORMAPGM")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(20)]
        [Column(name: "DCFORMAPGM")]
        public string Nome { get; set; }
    }
}