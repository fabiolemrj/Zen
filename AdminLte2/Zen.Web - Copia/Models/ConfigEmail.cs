using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{
    [Table("config_email")]
    public class ConfigEmail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(1)]
        [Column(name: "AUTENTICA")]
        public string Autentica { get; set; }
        [MaxLength(50)]

        [Column(name: "EMAIL")]
        public string Email { get; set; }
        [MaxLength(50)]
        [Column(name: "ENDSMTP")]
        public string EndSmtp { get; set; }
        [MaxLength(50)]
        [Column(name: "PORTA_SMTP")]
        public string PortaSmtp { get; set; }
        [MaxLength(50)]
        [Column(name: "REMETENTE")]
        public string Remetente { get; set; }
        [MaxLength(50)]
        [Column(name: "SENHA")]
        public string Senha { get; set; }
        [MaxLength(50)]
        [Column(name: "USUARIO")]
        public string Usuario{ get; set; }
    }
}