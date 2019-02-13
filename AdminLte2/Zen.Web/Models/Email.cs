using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zen.Web.Models
{
    public class Email
    {
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DtEnvio { get; set; }
        public Usuario Usuario { get; set; }
        [DisplayFormat(DataFormatString = "{0:hh\\:mm\\:ss}", ApplyFormatInEditMode = true)]
        public TimeSpan HrEnvio { get; set; }
        public string Remetente { get; set; }        
        public string Destinatario { get; set; }
        public string Assunto { get; set; }
        public string Mensagem { get; set; }
        public SituacaoEnvioEmail StEnvio { get; set; }
    }

    public enum SituacaoEnvioEmail
    {
        Todos = -1,
        NaoEnviado = 0,
        Sucesso = 1,
        Erro = 2
    }
}