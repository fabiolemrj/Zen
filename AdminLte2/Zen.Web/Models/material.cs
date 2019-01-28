using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{
    [Table("material")]
    public class Material
    {
        [Key]
        [Column(name: "IDMATERIAL")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [MaxLength(50)]
        [Column(name: "MATERIAL")]
        public string Nome { get; set; }

        [MaxLength(50)]
        [Column(name: "FABRICANTE")]
        public string Fabricante { get; set; }

        [MaxLength(1)]
        [Column(name: "FORA")]
        public string Fora { get; set; }

        [MaxLength(1)]
        [Column(name: "TIPO_LAM")]
        public string TipoLam { get; set; }

        [Column(name: "ALT")]
        public double? Alt { get; set; } = 0;

        [Column(name: "LARG")]
        public double? Larg { get; set; } = 0;

        [Column(name: "VRTOTAL")]
        [Range(0, 9999999999999999.99)]
        public double? ValorTotal { get; set; } = 0;

        [Column(name: "VRUNIT")]
        [Range(0, 9999999999999999.99)]
        public double? ValorUnit { get; set; } = 0;

        [Column(name: "IDFORNECEDOR")]
        public int? IdFornecedor { get; set; }

        [Column(name: "NRFLS")]
        public int? NumFolha { get; set; } = 0;

        [NotMapped]
        public Fornecedor Fornecedor { get; set; }

        [Column(name: "DTATU")]
        public DateTime? DtAtu { get; set; } = DateTime.Now;

    }
}