using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdminLte2.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        [MaxLength(50)]
        public string Login { get; set; }

        [Required]
        [MaxLength(100)]
        public string Senha { get; set; }

        [Required]
        public TipoUsuario Tipo { get; set; } = TipoUsuario.Padrao;

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [Column(name:"IDSITUACAO")]
        public Situacao Situacao { get; set; } = Situacao.Ativo;
        
        [Column(name: "Foto")]
        public  byte[] Foto{ get; set; }

        [Column(name: "ExtFoto")]
        [MaxLength(50)]
        public string ExtensaoFoto { get; set; }

        [Column(name: "Funcao")]
        [MaxLength(50)]
        public string Funcao { get; set; }

        [Column(name:"IdPerfil")]
        [ForeignKey("Perfil")]
        public int IdPerfil { get; set; }
              
        public virtual Perfil Perfil { get; set; }

      
    }
}