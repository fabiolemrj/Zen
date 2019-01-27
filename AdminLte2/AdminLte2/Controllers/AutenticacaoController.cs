using AdminLte2.Models;
using AdminLte2.Servico;
using AdminLte2.Utils;
using AdminLte2.ViewModels.AutenticacaoViewModel;
using AdminLte2.ViewModels.UsuariosViewModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace AdminLte2.Controllers
{
    public class AutenticacaoController : CustomController
    {
        ZenContext db = new ZenContext();
        ServicoUsuarios servico = new ServicoUsuarios();
        // GET: Autenticacao
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Login(string ReturnUrl)
        {
            var viewmodel = new LoginViewModel
            {
                UrlRetorno = ReturnUrl
            };

            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel viewmodel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewmodel);
            }

            var usuario = db.Usuarios.FirstOrDefault(u => u.Login == viewmodel.Login);

            if (usuario == null)
            {
                ModelState.AddModelError("Login", "Login incorreto");
                return View(viewmodel);
            }

            if (usuario.Senha != Hash.GerarHash(viewmodel.Senha))
            {
                ModelState.AddModelError("Senha", "Senha incorreta");
                return View(viewmodel);
            }

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim("Login", usuario.Login),
                new Claim("Id", usuario.Id.ToString()),
                new Claim("Perfil", usuario.Tipo.ToString()),
                new Claim(ClaimTypes.Role, usuario.Tipo.ToString())
            }, "ApplicationCookie");

            var servPerfilAcoes = new ServicoPerfilAcoes();
            var lstAcoes = servPerfilAcoes.ObterPerfilAcoesPorPerfil(db, usuario.IdPerfil);

            foreach(var item in lstAcoes)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, item.Acao.ToString()));
            }

            Request.GetOwinContext().Authentication.SignIn(identity);
          
           if (!String.IsNullOrWhiteSpace(viewmodel.UrlRetorno) ||
           Url.IsLocalUrl(viewmodel.UrlRetorno))
                return Redirect(viewmodel.UrlRetorno);
            else
                return RedirectToAction("Index", "Painel");
        }

        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut("ApplicationCookie");
            return RedirectToAction("Principal", "AdminLte");
        }

        [Authorize]
        public ActionResult AlterarFoto()
        {
            var identity = User.Identity as ClaimsIdentity;
            var model = new AlterarFotoViewModel();
            var login = identity.Claims.FirstOrDefault(c => c.Type == "Login").Value;
            var usuario = servico.ObterUsuarioPorLogin(db, login);
            model.NmUsuario = usuario.Nome;
            model.Id = usuario.Id;
            model.Foto = usuario.Foto;
            return View(model);
        }

        [HttpPost]
        public ActionResult AlterarFoto(AlterarFotoViewModel model, HttpPostedFileBase file)
        {
            byte[] fileInBytes = new byte[file.ContentLength];
            using(BinaryReader reader = new BinaryReader(file.InputStream))
            {
                fileInBytes = reader.ReadBytes(file.ContentLength);
            }
            string fileasString = Convert.ToBase64String(fileInBytes);
            var usuario = servico.ObterUsuarioPorId(db,model.Id);
            usuario.Foto = fileInBytes;
            usuario.ExtensaoFoto = file.ContentType;
            servico.Salvar(db, usuario);
            return RedirectToAction("Principal","AdminLte");
        }

        public FileContentResult ObterImagem(int id)
        {
            var usuario = servico.ObterUsuarioPorId(db, id);
            if (usuario != null)
            {
                return File(usuario.Foto, usuario.ExtensaoFoto);
            }
            
            return null;
        }
           
        public String ObterNomeUsuario()
        {
            var identity = User.Identity as ClaimsIdentity;
            var valorDaClaim = int.Parse(identity.Claims.FirstOrDefault(c => c.Type == "Id").Value);
            var usuario = servico.ObterUsuarioPorId(db, valorDaClaim);
            if (usuario != null)
            {
                return "<b>" +usuario.Nome+"</b>";
            }
            return string.Empty;
        }

        public ActionResult PagNaoAutoriz()
        {
            return View();
        }

    }
}