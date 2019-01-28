using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;
using Zen.Web.Models;
using Zen.Web.Servico;
using Zen.Web.Utils;
using Zen.Web.ViewModels.ImpressorViewModel;

namespace Zen.Web.Controllers
{
    public class ImpressorController : CustomController
    {
        ZenContext db = new ZenContext();
        ServicoImpressor servico = new ServicoImpressor();

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
                           <i class='fa fa-user'> </i>
                           <a href='/Impressor'> Impressores </a>
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
                           <i class='fa fa-user'> </i>
                           <a href = '/Impressor' > Impressores </a>
                     
                         <li class='active'> Cadastro
                         </li>
                    </ol>";
        }

        // GET: Impressor
        [DireitoAcesso(Constantes.AC_CONS_CAD_IMPRESS)]
        public ActionResult Index(string filtro = "", string ativo = "S", int pagina = 1, int tamPag = Constantes.TamanhoPagina)
        {
            TempData["breadcrumb"] = CreateBreadCrumbIndex();
            TempData["nometela"] = "Impressores";

            var lista = PrepararViewModel(filtro, ativo,pagina, tamPag);

            return View(lista);
        }

        private IPagedList<Impressor> PrepararViewModel(string filtro, string ativo, int pagina, int tamPag)
        {
            //PopularViewBag();
            var lst = new List<Impressor>();

            lst = servico.ObterListaObjetos(db, filtro, ativo).ToList();
            ViewBag.Ativo = ativo;
            ViewBag.Filtro = filtro;
            ViewBag.TamanhoPagina = tamPag;
            return lst.ToPagedList(pagina, tamPag);
        }

        [DireitoAcesso(Constantes.AC_INC_CAD_IMPRESS)]
        public ActionResult Create()
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Novo Impressor";
            TempData["lboper"] = "Novo";

            var model = CriarViewModelAddEdit();
            model.Ativo = "S";
            return View(model);
        }


        private CreateEditViewModel CriarViewModelAddEdit()
        {
            ViewBag.SimNao = new SelectList(ListasGenericas.ObterSimNao, "Sigla", "Nome");
            var model = new CreateEditViewModel();
            return model;
        }

        [HttpPost]
        public ActionResult Create(CreateEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
                TempData["nometela"] = "Novo Impressor";
                TempData["lboper"] = "Novo";
                ViewBag.SimNao = new SelectList(ListasGenericas.ObterSimNao, "Sigla", "Nome");

                return View(model);
            }

       
            var impressor = new Impressor();
            ModelParaObjeto(model, impressor);

            try
            {
                servico.Salvar(db, impressor);
                TempData["sucesso"] = $@"Impressor salvo com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar criar Impressor {impressor.Nome}";
            }
            return RedirectToAction("Index");
        }

        private Impressor ModelParaObjeto(CreateEditViewModel model, Impressor impressor)
        {
            impressor.Id = model.Id;
            impressor.Nome = model.Nome;
            impressor.Ativo = model.Ativo;

            return impressor;
        }

        [DireitoAcesso(Constantes.AC_EDIT_CAD_IMPRESS)]
        public ActionResult Edit(int id)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Editar Impressor";
            TempData["lboper"] = "Editar";

            var model = CriarViewModelAddEdit();
            var impressor = servico.ObterObjetoPorId(db, id);
            if (impressor == null)
            {
                TempData["erro"] = $@"Erro ao tentar editar o impressor {impressor.Nome}";
                return RedirectToAction("Index");
            }
            ObjetoParaModel(impressor, model);

            return View("Create", model);
        }

        private void ObjetoParaModel(Impressor impressor, CreateEditViewModel model)
        {
            model.Id = impressor.Id;
            model.Nome = impressor.Nome;
            model.Ativo = impressor.Ativo;
        }

        [HttpPost]
        public ActionResult Edit(CreateEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
                TempData["nometela"] = "Editar Impressor";
                TempData["lboper"] = "Editar";
                return View("Create", model);
            }

            var impressor = servico.ObterObjetoPorId(db, model.Id);
            if (impressor == null)
            {
                TempData["erro"] = $@"Erro ao tentar editar o impressor {impressor.Nome}";
                return RedirectToAction("Index");
            }

            ModelParaObjeto(model, impressor);

            try
            {
                servico.Salvar(db, impressor);
                TempData["sucesso"] = $@"Impressor salva com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar editar o impressor {impressor.Nome}";
            }

            return RedirectToAction("Index");
        }

        [DireitoAcesso(Constantes.AC_APG_CAD_IMPRESS)]
        public ActionResult Delete(int id)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Apagar Impressor";

            var impressor = servico.ObterObjetoPorId(db, id);
            DeleteViewModel model = new DeleteViewModel();
            model.Id = impressor.Id;
            model.Nome = impressor.Nome;
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(DeleteViewModel model)
        {
            var impressor = servico.ObterObjetoPorId(db, model.Id);
            try
            {
                if (impressor != null)
                {
                    servico.Delete(db, impressor);
                }
                TempData["sucesso"] = $@"O impressor {impressor.Nome} foi apagado com sucesso!";
            }
            catch
            {
                TempData["erro"] = $@"Erro ao tentar apagar o impressor {impressor.Nome}";
            }
            return RedirectToAction("Index");
        }



    }
}