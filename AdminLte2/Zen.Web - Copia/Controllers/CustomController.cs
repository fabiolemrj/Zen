using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Zen.Web.Models;
using Zen.Web.Servico;

namespace Zen.Web.Controllers
{
    public class CustomController : Controller
    {
        ServicoUsuarios servico = new ServicoUsuarios();
        ZenContext db = new ZenContext();
        // GET: Custom
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var usuario = ObterUsuario();
            if (usuario != null)
            {
                ViewBag.NmUsuarioLogado = usuario.Nome;
                ViewBag.IdUsuarioLogado = usuario.Id;
                ViewBag.TipoUsuarioLogado = usuario.Tipo.ToString();
                ViewBag.FuncaoUsuarioLogado = usuario.Funcao;

            }
            else
            {
                ViewBag.NmUsuarioLogado = string.Empty;
                ViewBag.IdUsuarioLogado = string.Empty;
                ViewBag.TipoUsuarioLogado = string.Empty;
                ViewBag.FuncaoUsuarioLogado = string.Empty;
            }
        }

        private Usuario ObterUsuario()
        {

            if (User.Identity != null)
            {
                try
                {
                    var identity = User.Identity as ClaimsIdentity;
                    var valorDaClaim = int.Parse(identity.Claims.FirstOrDefault(c => c.Type == "Id").Value);
                    var usuario = servico.ObterUsuarioPorId(db, valorDaClaim);
                    return usuario;
                }
                catch
                {
                    Request.GetOwinContext().Authentication.SignOut("ApplicationCookie");
                }

            }
            return null;
        }

        private FileContentResult ObterImagem(Usuario usuario)
        {

            if (usuario != null)
            {
                if (usuario.Foto != null)
                {
                    var tipoImg = string.Empty;
                    if (string.IsNullOrEmpty(usuario.ExtensaoFoto))
                    {
                        tipoImg = usuario.ExtensaoFoto;
                    }
                    return File(usuario.Foto, tipoImg);
                }

            }


            Image img = Image.FromFile("~/images/user-admin.png");
            ImageConverter imgCon = new ImageConverter();
            byte[] btArr = (byte[])imgCon.ConvertTo(img, typeof(byte[]));
            return File(btArr, "image/png");

        }

        public FileContentResult ObterImagem()
        {
            var identity = User.Identity as ClaimsIdentity;
            var valorDaClaim = int.Parse(identity.Claims.FirstOrDefault(c => c.Type == "Id").Value);
            var usuario = servico.ObterUsuarioPorId(db, valorDaClaim);

            if (usuario != null)
            {
                if (usuario.Foto != null)
                {
                    var tipoImg = usuario.ExtensaoFoto;
                    return File(usuario.Foto, tipoImg);
                }

            }
            Image img = Image.FromFile("C:\\fonte\\TesteLteAdmin\\AdminLte2\\AdminLte2\\images\\user-admin.png");
            ImageConverter imgCon = new ImageConverter();
            byte[] btArr = (byte[])imgCon.ConvertTo(img, typeof(byte[]));
            return File(btArr, "image/png");


        }
    }
}