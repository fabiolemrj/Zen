using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;
using Zen.Web.Models;
using Zen.Web.Servico;
using Zen.Web.ViewModels.CorViewModel;

namespace Zen.Web.Controllers
{
    public class CorController : CustomController
    {
        ZenContext db = new ZenContext();
        ServicoCor servico = new ServicoCor();

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
                           <i class='fa fa-tint'> </i>
                           <a href='/cliente'> Bancos </a>
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
                           <i class='fa fa-bank'> </i>
                           <a href = '/cor' > Cores </a>
                     
                         <li class='active'> Cadastro
                         </li>
                    </ol>";
        }

        private IPagedList<ListaViewModel> PrepareViewLista(string filtro, int pagina, int tamPag)
        {
            var lst = servico.ObterListaObjetos(db, filtro).ToList();
            var lstModel = new List<ListaViewModel>();
            
            ViewBag.Filtro = filtro;
            ViewBag.TamanhoPagina = tamPag;

            return lstModel.ToPagedList(pagina, tamPag);
        }

        private void Unificar(List<Cor> lstCor)
        {
            //TODO: atuaizar campo cor nas tabelas orccalculo, osiacab
            foreach (var item in lstCor)
            {
                var cor = servico.ObterObjetoPorId(db, item.IdCor);

                if (cor != null)
                { 
                    servico.Delete(db, item);
                }
                  
            }
        }
              

        private IPagedList<Cor> PrepararViewModel(string filtro, int pagina, int tamPag)
        {
            //PopularViewBag();
            var lst = new List<Cor>();

            lst = servico.ObterListaObjetos(db, filtro).ToList();

            ViewBag.Filtro = filtro;
            ViewBag.TamanhoPagina = tamPag;
            return lst.ToPagedList(pagina, tamPag);
        }

        [DireitoAcesso(Constantes.AC_CONS_CAD_CORES)]
        public ActionResult Index(string filtro = "", int pagina = 1, int tamPag = Constantes.TamanhoPagina)
        {
            TempData["breadcrumb"] = CreateBreadCrumbIndex();
            TempData["nometela"] = "Cores";

            var lista = PrepararViewModel(filtro, pagina, tamPag);

            return View(lista);
        }

        private void ListaCores()
        {
            var lst = servico.ObterListaObjetos(db, "");
            ViewBag.CorDest = new SelectList(lst, "Idcor", "Nome");
        }

        [HttpPost]
        public ActionResult Index(ListaViewModel model)
        {
            
            var cont = 0;

            var lstCoresSelecionadas = new List<Cor>();

            foreach (var item in model.ListaCores)
            {
                if (item.Selecionado)
                {
                    lstCoresSelecionadas.Add(item);
                    cont++;
                }
            }

            if (lstCoresSelecionadas.Count > 0)
            {
                Unificar(lstCoresSelecionadas);
            }
            var lista = PrepararViewModel("", 1, Constantes.TamanhoPagina);

            return View(lista);
        }

        [DireitoAcesso(Constantes.AC_INC_CAD_CORES)]
        public ActionResult Create()
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Nova Cor";
            TempData["lboper"] = "Novo";

            var model = CriarViewModelAddEdit();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CreateEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
                TempData["nometela"] = "Nova Cor";
                TempData["lboper"] = "Novo";

                return View(model);
            }

            var cor = new Cor();
            ModelParaObjeto(model, cor);

            try
            {
                servico.Salvar(db, cor);
                TempData["sucesso"] = $@"Cor salva com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar editar a cor {cor.Nome}";
            }
            return RedirectToAction("Index");
        }

        private void ModelParaObjeto(CreateEditViewModel model, Cor cor)
        {
            cor.IdCor = model.IdCor;
            cor.Nome = model.Nome;            
        }

        private CreateEditViewModel CriarViewModelAddEdit()
        {
            var model = new CreateEditViewModel();
            return model;
        }

        [DireitoAcesso(Constantes.AC_EDIT_CAD_CORES)]
        public ActionResult Edit(int id)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Editar Cor";
            TempData["lboper"] = "Editar";

            var model = CriarViewModelAddEdit();
            var banco = servico.ObterObjetoPorId(db, id);
            if (banco == null)
            {
                TempData["erro"] = $@"Erro ao tentar editar a cor {banco.Nome}";
                return RedirectToAction("Index");
            }
            ObjetoParaModel(banco, model);

            return View("Create", model);

        }

        [HttpPost]
        public ActionResult Edit(CreateEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
                TempData["nometela"] = "Editar Cor";
                TempData["lboper"] = "Editar";
                return View("Create", model);
            }

            var cor = servico.ObterObjetoPorId(db, model.IdCor);
            if (cor == null)
            {
                TempData["erro"] = $@"Erro ao tentar editar a cor {cor.Nome}";
                return RedirectToAction("Index");
            }

            ModelParaObjeto(model, cor);

            try
            {
                servico.Salvar(db, cor);
                TempData["sucesso"] = $@"Cor salvo com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar editar a cor {cor.Nome}";
            }

            return RedirectToAction("Index");
        }

        private void ObjetoParaModel(Cor cor, CreateEditViewModel model)
        {
            model.IdCor = cor.IdCor;
            model.Nome = cor.Nome;
        }

        [DireitoAcesso(Constantes.AC_EDIT_CAD_BANCO)]
        public ActionResult Delete(int id)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Apagar Cor";

            var cor = servico.ObterObjetoPorId(db, id);
            DeleteViewModel model = new DeleteViewModel();
            model.IdCor = cor.IdCor;
            model.Nome = cor.Nome;
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(DeleteViewModel model)
        {
            var cor = servico.ObterObjetoPorId(db, model.IdCor);
            try
            {
                if (cor != null)
                {
                    servico.Delete(db, cor);
                }
                TempData["sucesso"] = $@"A cor {cor.Nome} foi apagado com sucesso!";
            }
            catch
            {
                TempData["erro"] = $@"Erro ao tentar apagar a cor {cor.Nome}";
            }
            return RedirectToAction("Index");
        }

        public ActionResult UnificarCores(string filtro = "",  int pagina = 1, int tamPag = Constantes.TamanhoPagina)
        {
            TempData["breadcrumb"] = CreateBreadCrumbIndex();
            TempData["nometela"] = "Unificar Cores";          
            

            return View();
        }

        public PartialViewResult _Lista(string filtro = "")
        {
            ListaCores();
            ListaViewModel model = new ListaViewModel();
            
            model.ListaCores = servico.ObterListaObjetos(db,filtro).ToList();           
            
            return PartialView(model);
        }

       
    }
}