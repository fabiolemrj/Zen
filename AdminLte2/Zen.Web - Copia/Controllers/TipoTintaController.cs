using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;
using Zen.Web.Models;
using Zen.Web.Servico;
using Zen.Web.ViewModels.TipoTintaViewModel;

namespace Zen.Web.Controllers
{
    public class TipoTintaController : Controller
    {
        ZenContext db = new ZenContext();
        ServicoTipoTinta servico = new ServicoTipoTinta();

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
                           <a href='/TipoTinta'> Tipo de Tinta </a>
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
                         <li>
                           <i class='fa fa-tint'> </i>
                           <a href = '/TipoTinta' > Tipo de Tinta </a>
                         </li>
                         <li class='active'> Cadastro
                         </li>
                    </ol>";
        }


        [DireitoAcesso(Constantes.AC_CONS_CAD_TPTINT)]
        public ActionResult Index(string filtro = "", int pagina = 1, int tamPag = Constantes.TamanhoPagina)
        {
            TempData["breadcrumb"] = CreateBreadCrumbIndex();
            TempData["nometela"] = "Tipo de Tinta";

            var lista = PrepararViewModel(filtro, pagina, tamPag);

            return View(lista);
        }

        private IPagedList<TipoTinta> PrepararViewModel(string filtro, int pagina, int tamPag)
        {
            //PopularViewBag();
            var lst = new List<TipoTinta>();

            lst = servico.ObterListaObjetos(db, filtro).ToList();

            ViewBag.Filtro = filtro;
            ViewBag.TamanhoPagina = tamPag;
            return lst.ToPagedList(pagina, tamPag);
        }

        [DireitoAcesso(Constantes.AC_INC_CAD_TPTINT)]
        public ActionResult Create()
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Novo Tipo de Tinta";
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
                TempData["nometela"] = "Novo Tipo de Tinta";
                TempData["lboper"] = "Novo";

                return View(model);
            }

            var objeto = new TipoTinta();

            ModelParaObjeto(model, objeto);
            try
            {
                servico.Salvar(db, objeto);
                TempData["sucesso"] = $@"Tipo de Tinta salvo com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar editar o tipo de tinta {objeto.Nome}";
            }
            return RedirectToAction("Index");
        }

        private TipoTinta ModelParaObjeto(CreateEditViewModel model, TipoTinta objeto)
        {
            objeto.Id = model.Id;
            objeto.Nome = model.Nome;

            return objeto;
        }

        [DireitoAcesso(Constantes.AC_EDIT_CAD_TPTINT)]
        public ActionResult Edit(int id)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Editar Tipo de Tinta";
            TempData["lboper"] = "Editar";

            var model = CriarViewModelAddEdit();
            var objeto = servico.ObterObjetoPorId(db, id);
            if (objeto == null)
            {
                TempData["erro"] = $@"Erro ao tentar editar o tipo de tinta {objeto.Id}";
                return RedirectToAction("Index");
            }
            ObjetoParaModel(objeto, model);

            return View("Create", model);
        }

        private void ObjetoParaModel(TipoTinta objeto, CreateEditViewModel model)
        {
            model.Id = objeto.Id;
            model.Nome = objeto.Nome;
        }

        [HttpPost]
        public ActionResult Edit(CreateEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
                TempData["nometela"] = "Editar Tipo de Tinta";
                TempData["lboper"] = "Editar";
                return View("Create", model);
            }

            var objeto = servico.ObterObjetoPorId(db, model.Id);
            if (objeto == null)
            {
                TempData["erro"] = $@"Erro ao tentar editar o tipo de tinta {objeto.Nome}";
                return RedirectToAction("Index");
            }

            ModelParaObjeto(model, objeto);

            try
            {
                servico.Salvar(db, objeto);
                TempData["sucesso"] = $@"Tipo de tinta salvo com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar editar o tipo de tinta {objeto.Nome}";
            }

            return RedirectToAction("Index");
        }

        [DireitoAcesso(Constantes.AC_APG_CAD_TPTINT)]
        public ActionResult Delete(int id)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Apagar Tipo de Tinta";

            var setor = servico.ObterObjetoPorId(db, id);
            DeleteViewModel model = new DeleteViewModel();
            model.Id = setor.Id;
            model.Nome = setor.Nome;
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(DeleteViewModel model)
        {
            var objeto = servico.ObterObjetoPorId(db, model.Id);
            try
            {
                if (objeto != null)
                {
                    servico.Delete(db, objeto);
                }
                TempData["sucesso"] = $@"O tipo de tinta {objeto.Nome} foi apagado com sucesso!";
            }
            catch
            {
                TempData["erro"] = $@"Erro ao tentar apagar o tipo de tinta {objeto.Nome}";
            }
            return RedirectToAction("Index");
        }
    }
}