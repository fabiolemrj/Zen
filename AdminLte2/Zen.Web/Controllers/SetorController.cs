
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;
using Zen.Web.Models;
using Zen.Web.Servico;
using Zen.Web.ViewModels.SetorViewModel;

namespace Zen.Web.Controllers
{
    public class SetorController : CustomController
    {

        ZenContext db = new ZenContext();
        ServicoSetor servico = new ServicoSetor();

        private string CreateBreadCrumbIndex()
        {
            return $@"<ol class='breadcrumb'>
                         <li>                            
                             <a href ='/'> Principal </a>
                         </li>
                         <li>      
                             <i class='fa fa-folder'> </i>
                             <a href ='/'> Arquivo </a>
                         </li>
                        <li>      
                             <i class='fa fa-folder-o'> </i>
                             <a href ='/'> Cadastros Auxiliares </a>
                         </li>
                         <li class='active'>
                           <i class='fa fa-file'> </i>
                           <a href='/Setor'> Setores </a>
                         </li>
                         
                    </ol>";
        }

        private string CreateBreadCrumbCreatEdit()
        {
            return $@"<ol class='breadcrumb'>
                         <li>                            
                             <a href = '/' > Principal </ a >
                         </li>
                         <li>
                             <i class='fa fa-folder'> </i>
                             <a href = '/' > Arquivo </a>
                         </li>
                        <li>
                             <i class='fa fa-folder-o'> </i>
                             <a href = '/' > Cadastros Auxiliares</a>
                         </li>
                           <i class='fa fa-file'> </i>
                           <a href = '/Setor' > Setores </a>
                     
                         <li class='active'> Cadastro
                         </li>
                    </ol>";
        }

        // GET: Setor
        [DireitoAcesso(Constantes.AC_CONS_SETORES)]
        public ActionResult Index(string filtro = "", int pagina = 1, int tamPag = Constantes.TamanhoPagina)
        {
            TempData["breadcrumb"] = CreateBreadCrumbIndex();
            TempData["nometela"] = "Setores";

            var lista = PrepararViewModel(filtro, pagina, tamPag);

            return View(lista);
        }

        private IPagedList<Setor> PrepararViewModel(string filtro, int pagina, int tamPag)
        {
            //PopularViewBag();
            var lst = new List<Setor>();

            lst = servico.ObterListaObjetos(db, filtro).ToList();

            ViewBag.Filtro = filtro;
            ViewBag.TamanhoPagina = tamPag;
            return lst.ToPagedList(pagina, tamPag);
        }

        [DireitoAcesso(Constantes.AC_INC_SETORES)]
        public ActionResult Create()
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Novo Setor";
            TempData["lboper"] = "Novo";

            var model = CriarViewModelAddEdit();

            return View(model);
        }

        private CreateEditViewModel CriarViewModelAddEdit()
        {
            var model = new CreateEditViewModel();
            return model;
        }

        [HttpPost]
        public ActionResult Create(CreateEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
                TempData["nometela"] = "Novo Setor";
                TempData["lboper"] = "Novo";

                return View(model);
            }

            var setor = new Setor();
            ModelParaObjeto(model, setor);

            try
            {
                servico.Salvar(db, setor);
                TempData["sucesso"] = $@"Setor salvo com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar editar o setor {setor.Nome}";
            }
            return RedirectToAction("Index");
        }

        private Setor ModelParaObjeto(CreateEditViewModel model, Setor setor)
        {
            setor.Id = model.Id;
            setor.Nome = model.Nome;
            setor.Responsavel = model.Responsavel;

            return setor;
        }

        [DireitoAcesso(Constantes.AC_EDIT_SETORES)]
        public ActionResult Edit(int id)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Editar setor";
            TempData["lboper"] = "Editar";

            var model = CriarViewModelAddEdit();
            var setor = servico.ObterObjetoPorId(db, id);
            if (setor == null)
            {
                TempData["erro"] = $@"Erro ao tentar editar o setor {setor.Id}";
                return RedirectToAction("Index");
            }
            ObjetoParaModel(setor, model);

            return View("Create", model);
        }


        private void ObjetoParaModel(Setor setor, CreateEditViewModel model)
        {
            model.Id = setor.Id;
            model.Nome = setor.Nome;
            model.Responsavel = setor.Responsavel;
        }

        [HttpPost]
        public ActionResult Edit(CreateEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
                TempData["nometela"] = "Editar setor";
                TempData["lboper"] = "Editar";
                return View("Create", model);
            }

            var setor = servico.ObterObjetoPorId(db, model.Id);
            if (setor == null)
            {
                TempData["erro"] = $@"Erro ao tentar editar o setor {setor.Nome}";
                return RedirectToAction("Index");
            }

            ModelParaObjeto(model, setor);

            try
            {
                servico.Salvar(db, setor);
                TempData["sucesso"] = $@"setor salvo com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar editar o setor {setor.Nome}";
            }

            return RedirectToAction("Index");
        }

        [DireitoAcesso(Constantes.AC_APG_SETORES)]
        public ActionResult Delete(int id)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Apagar setor";

            var setor = servico.ObterObjetoPorId(db, id);
            DeleteViewModel model = new DeleteViewModel();
            model.Id = setor.Id;
            model.Nome = setor.Nome;
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(DeleteViewModel model)
        {
            var setor = servico.ObterObjetoPorId(db, model.Id);
            try
            {
                if (setor != null)
                {
                    servico.Delete(db, setor);
                }
                TempData["sucesso"] = $@"O setor {setor.Nome} foi apagado com sucesso!";
            }
            catch
            {
                TempData["erro"] = $@"Erro ao tentar apagar o setor {setor.Nome}";
            }
            return RedirectToAction("Index");
        }

    }
}