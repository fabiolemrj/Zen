using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{
    [Table("Perfil")]
    public class Perfil
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(name: "Descricao")]
        public string Descricao { get; set; }

        [Required]
        [Column(name: "IdSituacao")]
        public Situacao Situacao { get; set; } = Situacao.Ativo;

        public virtual ICollection<Usuario> Usuarios { get; set; }
        public virtual ICollection<PerfilAcoes> PerfilAcoes { get; set; }

        public Perfil()
        {
            Usuarios = new HashSet<Usuario>();
            PerfilAcoes = new HashSet<PerfilAcoes>();
        }
    }
}