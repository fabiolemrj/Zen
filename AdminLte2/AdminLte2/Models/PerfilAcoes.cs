using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminLte2.Models
{

    [Table("PerfilAcoes")]
    public class PerfilAcoes
    {
        [Key, Column(Order = 1)]
        [ForeignKey("Perfil")]
        public int IdPerfil { get; set; }

        public virtual Perfil Perfil{ get; set; }

        [Key, Column(Order = 2)]
        public int Acao { get; set; }

    }
}