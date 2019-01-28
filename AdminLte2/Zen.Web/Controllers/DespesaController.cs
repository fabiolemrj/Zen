using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;
using Zen.Web.Models;
using Zen.Web.Servico;
using Zen.Web.ViewModels.DespesaViewModel;

namespace Zen.Web.Controllers
{
    public class DespesaController : CustomController
    {
        ZenContext db = new ZenContext();
        ServicoDespesa servico = new ServicoDespesa();

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
                           <i class='fa fa-dollar'> </i>
                           <a href='/Despesa'> Itens de Despesa </a>
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
                           <i class='fa fa-dollar'> </i>
                           <a href = '/Despesa' > Itens de Despesa </a>
                     
                         <li class='active'> Cadastro
                         </li>
                    </ol>";
        }

        // GET: Despesa
        [DireitoAcesso(Constantes.AC_CONS_ITENSDESP)]
        public ActionResult Index(string filtro = "", int pagina = 1, int tamPag = Constantes.TamanhoPagina)
        {
            TempData["breadcrumb"] = CreateBreadCrumbIndex();
            TempData["nometela"] = "Itens de Despesa";

            var lista = PrepararViewModel(filtro, pagina, tamPag);

            return View(lista);
        }

        private IPagedList<Despesa> PrepararViewModel(string filtro, int pagina, int tamPag)
        {
            //PopularViewBag();
            var lst = new List<Despesa>();

            lst = servico.ObterListaObjetos(db, filtro).ToList();

            ViewBag.Filtro = filtro;
            ViewBag.TamanhoPagina = tamPag;
            return lst.ToPagedList(pagina, tamPag);
        }


        [DireitoAcesso(Constantes.AC_INC_ITENSDESP)]
        public ActionResult Create()
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Novo itens de despesa";
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
                TempData["nometela"] = "Novo Item de Despesa";
                TempData["lboper"] = "Novo";

                return View(model);
            }

            var despesa = new Despesa();
            ModelParaObjeto(model, despesa);

            try
            {
                servico.Salvar(db, despesa);
                TempData["sucesso"] = $@"Item de despesa salvo com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar editar o item de despesa {despesa.Nome}";
            }
            return RedirectToAction("Index");
        }

        private Despesa ModelParaObjeto(CreateEditViewModel model, Despesa despesa)
        {
            despesa.Id = model.Id;
            despesa.Nome = model.Nome;
            despesa.Codigo = model.Codigo;

            return despesa;
        }

        [DireitoAcesso(Constantes.AC_EDIT_ITENSDESP)]
        public ActionResult Edit(int id)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Editar Itens de despesa";
            TempData["lboper"] = "Editar";

            var model = CriarViewModelAddEdit();
            var despesa = servico.ObterObjetoPorId(db, id);
            if (despesa == null)
            {
                TempData["erro"] = $@"Erro ao tentar editar o item de despesa {despesa.Nome}";
                return RedirectToAction("Index");
            }
            ObjetoParaModel(despesa, model);

            return View("Create", model);
        }

        private void ObjetoParaModel(Despesa despesa, CreateEditViewModel model)
        {
            model.Id = despesa.Id;
            model.Codigo = despesa.Codigo;
            model.Nome = despesa.Nome;            
        }

        [HttpPost]
        public ActionResult Edit(CreateEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
                TempData["nometela"] = "Editar Itens de despesa";
                TempData["lboper"] = "Editar";
                return View("Create", model);
            }

            var despesa = servico.ObterObjetoPorId(db, model.Id);
            if (despesa == null)
            {
                TempData["erro"] = $@"Erro ao tentar editar o item de despesa {despesa.Nome}";
                return RedirectToAction("Index");
            }

            ModelParaObjeto(model, despesa);

            try
            {
                servico.Salvar(db, despesa);
                TempData["sucesso"] = $@"Item de despesa salvo com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar editar o item de despesa {despesa.Nome}";
            }

            return RedirectToAction("Index");
        }

        [DireitoAcesso(Constantes.AC_APG_ITENSDESP)]
        public ActionResult Delete(int id)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Apagar Item de despesa";

            var despesa = servico.ObterObjetoPorId(db, id);
            DeleteViewModel model = new DeleteViewModel();
            model.Id = despesa.Id;
            model.Nome = despesa.Nome;
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(DeleteViewModel model)
        {
            var despesa = servico.ObterObjetoPorId(db, model.Id);
            try
            {
                if (despesa != null)
                {
                    servico.Delete(db, despesa);
                }
                TempData["sucesso"] = $@"O item de despesa {despesa.Nome} foi apagado com sucesso!";
            }
            catch(Exception ex)
            {
                var msg = ex.Message;
                TempData["erro"] = $@"Erro ao tentar apagar o item de despesa {despesa.Nome}";
            }
            return RedirectToAction("Index");
        }

    }
}