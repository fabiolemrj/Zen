using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{
    public class FluxoCaixa
    {
        [Key]
        public int Id { get; set; }

        public ContaCorrente ContaCorrente { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DtMov { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        [Range(0, 9999999999999999.99)]
        public double Valor { get; set; }

        [MaxLength(1)]
        public string Sentido { get; set; }
        
        public string Historico { get; set; }

        public TipoDoc TipoDocumento { get; set; }

        public FormaPag Formapag { get; set; }

        public string FornecCliente { get; set; }

        [MaxLength(1)]
        public string Estado { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DtVenc { get; set; }

        public int Idtitulo { get; set; }
    }
}