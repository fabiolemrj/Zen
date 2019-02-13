using AdminLte2.Models;
using AdminLte2.Servico;
using AdminLte2.Utils;
using AdminLte2.ViewModels;
using AdminLte2.ViewModels.UsuariosViewModel;
using AdminLte2.ViewModels.UsuarioViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zen.Web.Servico;

namespace AdminLte2.Controllers
{
    public class UsuariosController : CustomController
    {
        ZenContext db = new ZenContext();
        ServicoUsuarios servico = new ServicoUsuarios();
        


        // GET: Usuarios
        [DireitoAcesso(Constantes.AC_USR_CONS)]
        public ActionResult Index()
        {
            
            var model = new IndexViewModel();

            PrepararViewModel(model);

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(IndexViewModel model)
        {
            PrepararViewModel(model);

            return View(model);
        }

        public void PrepararViewModel(IndexViewModel model)
        {
            PopularViewBag();
            model.LstUsuarios = servico.ObterListaUsuarios(db, model.filtroNome).ToList();
        }

        [DireitoAcesso(Constantes.AC_USR_CAD)]
        public ActionResult Create()
        {
            PopularViewBag();
            return View();
        }

        [HttpPost]
        public ActionResult Create(AddEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (servico.ObterUsuarioPorLogin(db, model.Login) != null)
            {
                ModelState.AddModelError("Usuario", "Esse Login já está em uso");
                return View(model);
            }

            model.Senha = "zen";
            var usuario = new Usuario()
            {
                Id = 0,
                Login = model.Login,
                Nome = model.NmUsuario,
                Senha = Hash.GerarHash(model.Senha),
                Tipo = model.TipoAcesso,
                Situacao = model.Situacao,
                Email = model.Email,
                Funcao = model.Funcao,
                IdPerfil = model.IdPerfil 
            };

            servico.Salvar(db, usuario);
            return RedirectToAction("Index");
        }

        

        private AddEditViewModel CriarAddEditViewModel(Usuario usurio)
        {
            PopularViewBag();

            var model = new AddEditViewModel();
            model.Id = usurio.Id;
            model.Login = usurio.Login;
            model.NmUsuario = usurio.Nome;
            model.TipoAcesso = usurio.Tipo;
            model.Situacao = usurio.Situacao;
            model.Email = usurio.Email;
            model.Funcao = usurio.Funcao;
            model.Perfil = usurio.Perfil;
            
            return model;
        }

        [DireitoAcesso(Constantes.AC_USR_CAD)]
        public ActionResult Edit(int id)
        {
            
            var usurio = servico.ObterUsuarioPorId(db, id);
            AddEditViewModel model = CriarAddEditViewModel(usurio);

            if (usurio == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(AddEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //if (model.Senha != model.ConfirmacaoSenha)
            //{
            //    ModelState.AddModelError("Senha", "Os campos de senha e confirmação de senha devem ser iguais");
            //    return View(model);
            //}

            var usuario = servico.ObterUsuarioPorId(db, model.Id);
            usuario.Nome = model.NmUsuario;
            usuario.Login = model.Login;
            // usuario.Senha = Hash.GerarHash(model.Senha);
            usuario.Tipo = model.TipoAcesso;
            usuario.Email = model.Email;
            usuario.Situacao = model.Situacao;
            usuario.Funcao = model.Funcao;
            usuario.Perfil = model.Perfil;

            servico.Salvar(db, usuario);
            return RedirectToAction("Index");
        }

        [DireitoAcesso(Constantes.AC_USR_CAD)]
        public ActionResult Delete(int id)
        {
            var usurio = servico.ObterUsuarioPorId(db, id);
            DeleteViewModel model = new DeleteViewModel();
            model.Id = usurio.Id;
            model.NmUsuario = usurio.Nome;
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(DeleteViewModel model)
        {
            var usurio = servico.ObterUsuarioPorId(db, model.Id);
            if (usurio != null)
            {              
                    servico.Delete(db, usurio);
         
            }

            return RedirectToAction("Index");
        }

        public void PopularViewBag()
        {
            ServicoPerfil servPerfil = new ServicoPerfil();
            ViewBag.IdPerfil = new SelectList(servPerfil.ObterListaPerfils(db, ""),"Id","Descricao");
        }

      

    }

}