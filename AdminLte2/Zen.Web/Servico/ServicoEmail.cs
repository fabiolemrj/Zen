using MySql.Data.MySqlClient.Memcached;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using Zen.Web.Models;

namespace Zen.Web.Servico
{
   
    public class ServicoEmail
    {
        ServicoConfigEmail servConf = new ServicoConfigEmail();

        public bool EnviarEmail(ZenContext db, Usuario usuario, string novaSenha)
        {
            
            var msg = $@"Olá {usuario.Nome}, sua senha no sistema Zen Web foi alterada para: <br/> <b> {novaSenha}</b> <br/>
                         Sugerimos altera-lá, após seu primeiro acesso.<br/>
                         Qualquer dúvida, entre em contato com o administrador do sistema!";

            var Email = servConf.ObterObjeto(db);
            
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            client.Host = Email.EndSmtp;
           
            client.EnableSsl =  !string.IsNullOrEmpty(Email.Autentica);
            string password = string.Empty;
            string remetente = Email.Remetente;

            client.UseDefaultCredentials = false;

            if (!string.IsNullOrEmpty(Email.Senha))
            {                
                client.Credentials = new System.Net.NetworkCredential(Email.Email, Email.Senha);
            }
            else
            {
                //client.Credentials = new System.Net.NetworkCredential(Email.Email, "fab28duda05");
                client.Credentials = new System.Net.NetworkCredential();
            }
            
            client.Port = Convert.ToInt32(Email.PortaSmtp);

            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(Email.Email, "Zen - Web");

            mail.To.Add(new MailAddress(usuario.Email, usuario.Nome));
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.Subject = "Nova Senha - Zen Web";
            mail.Body = msg;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            try
            {
                client.Send(mail);
                return true;
            }
            catch(Exception Ex)
            {
                return false;
            }
              
        }




    }
}