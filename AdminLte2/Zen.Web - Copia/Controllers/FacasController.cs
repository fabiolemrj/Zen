using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;
using Zen.Web.Models;
using Zen.Web.Servico;
using Zen.Web.ViewModels.FacasViewModel;

namespace Zen.Web.Controllers
{
    public class FacasController : CustomController
    {
        ZenContext db = new ZenContext();
        ServicoFacas servico = new ServicoFacas();

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
                           <a href='/Facas'> Facas </a>
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
                           <a href = '/Facas' > Facas </a>
                     
                         <li class='active'> Cadastro
                         </li>
                    </ol>";
        }

        [DireitoAcesso(Constantes.AC_CONS_CAD_FACAS)]
        public ActionResult Index(string filtro = "", int pagina = 1, int tamPag = Constantes.TamanhoPagina)
        {
            TempData["breadcrumb"] = CreateBreadCrumbIndex();
            TempData["nometela"] = "Facas";

            var lista = PrepararViewModel(filtro, pagina, tamPag);

            return View(lista);
        }

        private IPagedList<Facas> PrepararViewModel(string filtro, int pagina, int tamPag)
        {
            //PopularViewBag();
            var lst = new List<Facas>();

            lst = servico.ObterListaObjetos(db, filtro).ToList();

            ViewBag.Filtro = filtro;
            ViewBag.TamanhoPagina = tamPag;
            return lst.ToPagedList(pagina, tamPag);
        }

        [DireitoAcesso(Constantes.AC_INC_CAD_FACAS)]
        public ActionResult Create()
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Nova Faca";
            TempData["lboper"] = "Nova";

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
                TempData["nometela"] = "Nova Faca";
                TempData["lboper"] = "Novo";

                return View(model);
            }

            var faca = new Facas();
            ModelParaObjeto(model, faca);

            try
            {
                servico.Salvar(db, faca);
                TempData["sucesso"] = $@"Faca salvo com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar editar a faca {faca.Cod_Faca}";
            }
            return RedirectToAction("Index");
        }

        private Facas ModelParaObjeto(CreateEditViewModel model, Facas faca)
        {
            faca.IdFaca = model.IdFaca;
            faca.Cod_Faca = model.Cod_Faca;

            return faca;
        }

        [DireitoAcesso(Constantes.AC_EDIT_CAD_FACAS)]
        public ActionResult Edit(int id)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Editar Facas";
            TempData["lboper"] = "Editar";

            var model = CriarViewModelAddEdit();
            var faca = servico.ObterObjetoPorId(db, id);
            if (faca == null)
            {
                TempData["erro"] = $@"Erro ao tentar editar a faca {faca.Cod_Faca}";
                return RedirectToAction("Index");
            }
            ObjetoParaModel(faca, model);

            return View("Create", model);
        }

        [HttpPost]
        public ActionResult Edit(CreateEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
                TempData["nometela"] = "Editar Faca";
                TempData["lboper"] = "Editar";
                return View("Create", model);
            }

            var faca = servico.ObterObjetoPorId(db, model.IdFaca);
            if (faca == null)
            {
                TempData["erro"] = $@"Erro ao tentar editar a faca {faca.Cod_Faca}";
                return RedirectToAction("Index");
            }

            ModelParaObjeto(model, faca);

            try
            {
                servico.Salvar(db, faca);
                TempData["sucesso"] = $@"Faca salva com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar editar a faca {faca.Cod_Faca}";
            }

            return RedirectToAction("Index");
        }

        [DireitoAcesso(Constantes.AC_APG_CAD_FACAS)]
        public ActionResult Delete(int id)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Apagar Faca";

            var faca = servico.ObterObjetoPorId(db, id);
            DeleteViewModel model = new DeleteViewModel();
            model.IdFaca = faca.IdFaca;
            model.Nome = faca.Cod_Faca;
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(DeleteViewModel model)
        {
            var faca = servico.ObterObjetoPorId(db, model.IdFaca);
            try
            {
                if (faca != null)
                {
                    servico.Delete(db, faca);
                }
                TempData["sucesso"] = $@"A faca {faca.Cod_Faca} foi apagada com sucesso!";
            }
            catch
            {
                TempData["erro"] = $@"Erro ao tentar apagar a faca {faca.Cod_Faca}";
            }
            return RedirectToAction("Index");
        }

        private void ObjetoParaModel(Facas faca, CreateEditViewModel model)
        {
            model.IdFaca = faca.IdFaca;
            model.Cod_Faca = faca.Cod_Faca;
        }


    }
}