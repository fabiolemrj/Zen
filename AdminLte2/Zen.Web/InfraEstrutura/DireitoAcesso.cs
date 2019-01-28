using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Zen.Web.Models;
using Zen.Web.Servico;

namespace Zen.Web.InfraEstrutura
{
    public class DireitoAcesso: AuthorizeAttribute
    {
        private bool possuiDireitoAcesso;
        ZenContext db = new ZenContext();
        ServicoUsuarios servico = new ServicoUsuarios();
        ServicoPerfilAcoes servPerfilAcoes = new ServicoPerfilAcoes();
        private int acao = 0;

        private Usuario ObterUsuario(HttpContextBase httpContext)
        {

            if (httpContext.User.Identity != null)
            {
                try
                {
                    var identity = httpContext.User.Identity as ClaimsIdentity;
                    var valorDaClaim = int.Parse(identity.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                    var usuario = servico.ObterUsuarioPorId(db, valorDaClaim);
                    return usuario;
                }
                catch
                {
                    httpContext.Request.GetOwinContext().Authentication.SignOut("ApplicationCookie");
                }

            }
            return null;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            if (!httpContext.User.Identity.IsAuthenticated)
                return false;

            possuiDireitoAcesso = httpContext.User.IsInRole(acao.ToString());
            return possuiDireitoAcesso;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                base.OnAuthorization(filterContext);

                if (!possuiDireitoAcesso)
                {

                    filterContext.Result = new RedirectResult("~/Autenticacao/PagNaoAutoriz");
                }
            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult("~/Autenticacao/Logout");
            }
        }

        public DireitoAcesso(int acao)
            : base()
        {
            this.acao = acao;
        }
    }
}