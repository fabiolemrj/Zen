using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{
    [Table("maquinas")]
    public class Maquina
    {
        [Key]
        [Column(name: "IDMAQUINA")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [MaxLength(30)]
        [Column(name: "DCMAQUINA")]
        public string Nome { get; set; }

        [Column(name: "IDIMPRESSOR")]
        public int IdImpressor { get; set; }

        [NotMapped]
        public Impressor Impressor { get; set; }

        [NotMapped]
        public Impressor Auxiliar { get; set; }

        [Column(name: "IDAUXILIAR")]
        public int IdAuxiliar { get; set; }

        [MaxLength(1)]
        [Column(name: "SITUACAO")]
        public string Situacao { get; set; } = "A";

        [MaxLength(1)]
        [Column(name: "CLASSIF")]
        public string Classificacao { get; set; }

    }
}