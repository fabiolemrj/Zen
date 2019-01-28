using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{
    [Table("servico_fornec")]
    public class ServFornec
    {
        [Key]
        [Column(name: "IDSERVICO",Order =1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdServico { get; set; }

        [Key]
        [Column(name: "IDFORNECEDOR",Order =2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdFornecedor { get; set; }

        [NotMapped]
        public Fornecedor Fornecedor { get; set; }

        [NotMapped]
        public Serv Serv { get; set; }
    }
}