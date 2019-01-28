using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Zen.Web.Models;
using Zen.Web.Servico;
using Zen.Web.Utils;
using Zen.Web.ViewModels.PerfilViewModel;

namespace Zen.Web.Controllers
{
    public class PerfilController : CustomController
    {
        ZenContext db = new ZenContext();
        ServicoPerfil servico = new ServicoPerfil();

        private string CreateBreadCrumbIndex()
        {
            return $@"<ol class='breadcrumb'>
                         <li>                            
                             <a href ='/'> Principal </a>
                         </li>
                         <li class='active'>
                           <i class='fa fa-users'> </i>
                           <a href='/Perfil'> Perfis </a>
                         </li>
                         
                    </ol>";
        }

        private string CreateBreadCrumbCreatEdit()
        {
            return $@"<ol class='breadcrumb'>
                         <li>                            
                             <a href ='/'> Principal </a>
                         </li>
                         <li>
                           <i class='fa fa-users'> </i>
                           <a href='/Perfil'> Perfis </a>
                         </li>
                         <li class='active'> Cadastro
                         </li>
                    </ol>";
        }
        // GET: Perfil
        [Authorize]
        public ActionResult AlterarSenha()
        {
            var identity = User.Identity as ClaimsIdentity;
            var model = new AlterarSenhaViewModel();
            model.NmUsuario = identity.Name;
            return View(model);
        }

        [HttpPost]
        public ActionResult AlterarSenha(AlterarSenhaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var identity = User.Identity as ClaimsIdentity;
            var login = identity.Claims.FirstOrDefault(c => c.Type == "Login").Value;

            var usuario = db.Usuarios.FirstOrDefault(u => u.Login == login);

            if (Hash.GerarHash(model.SenhaAtual) != usuario.Senha)
            {
                ModelState.AddModelError("SenhaAtual", "Senha incorreta");
                return View();
            }

            usuario.Senha = Hash.GerarHash(model.NovaSenha);
            db.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Principal", "AdminLte");
        }

        [DireitoAcesso(Constantes.AC_PERFIL_CONS)]
        public ActionResult Index()
        {
            TempData["breadcrumb"] = CreateBreadCrumbIndex();
            TempData["nometela"] = "Perfis";

            var model = new IndexViewModel();
            model.LstPerfil = servico.ObterListaPerfils(db, "");
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(IndexViewModel model)
        {
            model.LstPerfil = servico.ObterListaPerfils(db, model.filtroDescr);

            return View(model);
        }

        [DireitoAcesso(Constantes.AC_PERFIL_CAD)]
        public ActionResult Create()
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Novo Perfil";
            return View();
        }

        [HttpPost]
        public ActionResult Create(AddEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var perfil = new Perfil()
            {
                Id = 0,
                Descricao = model.Descricao,
                Situacao = model.Situacao
            };

            servico.Salvar(db, perfil);
            return RedirectToAction("Index");
        }

        [DireitoAcesso(Constantes.AC_PERFIL_CAD)]
        public ActionResult Edit(int id)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Editar Perfil";

            var perfil = servico.ObterObjetoPorId(db, id);
            var model = new AddEditViewModel();
            model.Descricao = perfil.Descricao;
            model.Situacao = perfil.Situacao;
            model.Id = perfil.Id;

            if (perfil == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(AddEditViewModel model)
        {
            var perfil = servico.ObterObjetoPorId(db, model.Id);
            if (perfil != null)
            {
                perfil.Situacao = model.Situacao;
                perfil.Descricao = model.Descricao;
            }

            servico.Salvar(db, perfil);
            return RedirectToAction("Index");
        }

        [DireitoAcesso(Constantes.AC_PERFIL_CAD)]
        public ActionResult Delete(int id)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Apagar Perfil";

            var perfil = servico.ObterObjetoPorId(db, id);
            var model = new DeleteViewModel();
            model.Descricao = perfil.Descricao;
            model.Id = perfil.Id;
            model.Mensagem = @"Deseja realmente apagar o perfil " + perfil.Descricao + "?";

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(DeleteViewModel model)
        {
            var perfil = servico.ObterObjetoPorId(db, model.Id);

            if (perfil != null && (perfil.Usuarios == null || perfil.Usuarios.Count() == 0))
            {
                try
                {
                    servico.Delete(db, perfil);
                    TempData["sucesso"] = $@"O perfil {perfil.Descricao} foi apagado com sucesso!";
                }
                catch
                {
                    TempData["erro"] = $@"Erro ao tentar apagar o perfil {perfil.Descricao}";
                }
                
            }
            else if (perfil.Usuarios.Count() > 0)
            {
                model.Mensagem = @"O Perfil " + perfil.Descricao + " não pode ser apagado, pois está associado a usuarios!";
                TempData["erro"] = model.Mensagem;
                return View(model);
            }

            return RedirectToAction("Index");
        }
    }
}