using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;
using Zen.Web.Models;
using Zen.Web.Servico;
using Zen.Web.ViewModels.TransporteViewModel;

namespace Zen.Web.Controllers
{
    public class TransporteController : Controller
    {
        ZenContext db = new ZenContext();
        ServicoTransporte servico = new ServicoTransporte();

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
                           <i class='fa fa-truck'> </i>
                           <a href='/Transporte'> Transporte </a>
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
                           <i class='fa fa-truck'> </i>
                           <a href = '/Transporte' > Transporte </a>
                         </li>
                         <li class='active'> Cadastro
                         </li>
                    </ol>";
        }

        // GET: Transporte
        [DireitoAcesso(Constantes.AC_CONS_CAD_TRANS)]
        public ActionResult Index(string filtro = "", int pagina = 1, int tamPag = Constantes.TamanhoPagina)
        {
            TempData["breadcrumb"] = CreateBreadCrumbIndex();
            TempData["nometela"] = "Transporte";

            var lista = PrepararViewModel(filtro, pagina, tamPag);

            return View(lista);
        }

        private IPagedList<Transporte> PrepararViewModel(string filtro, int pagina, int tamPag)
        {
            //PopularViewBag();
            var lst = new List<Transporte>();

            lst = servico.ObterListaObjetos(db, filtro).ToList();

            ViewBag.Filtro = filtro;
            ViewBag.TamanhoPagina = tamPag;
            return lst.ToPagedList(pagina, tamPag);
        }

        [DireitoAcesso(Constantes.AC_INC_TIPODOC)]
        public ActionResult Create()
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Novo Tipo de Documento";
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
                TempData["nometela"] = "Novo Transporte";
                TempData["lboper"] = "Novo";

                return View(model);
            }

            var objeto = new Transporte();

            ModelParaObjeto(model, objeto);
            try
            {
                servico.Salvar(db, objeto);
                TempData["sucesso"] = $@"Transporte salvo com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar editar o transporte {objeto.Nome}";
            }
            return RedirectToAction("Index");
        }

        private Transporte ModelParaObjeto(CreateEditViewModel model, Transporte    objeto)
        {
            objeto.Id = model.Id;
            objeto.Nome = model.Nome;

            return objeto;
        }


        [DireitoAcesso(Constantes.AC_EDIT_CAD_TRANS)]
        public ActionResult Edit(int id)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Editar Transporte";
            TempData["lboper"] = "Editar";

            var model = CriarViewModelAddEdit();
            var objeto = servico.ObterObjetoPorId(db, id);
            if (objeto == null)
            {
                TempData["erro"] = $@"Erro ao tentar editar o transporte {objeto.Id}";
                return RedirectToAction("Index");
            }
            ObjetoParaModel(objeto, model);

            return View("Create", model);
        }

        private void ObjetoParaModel(Transporte objeto, CreateEditViewModel model)
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
                TempData["nometela"] = "Editar Transporte";
                TempData["lboper"] = "Editar";
                return View("Create", model);
            }

            var objeto = servico.ObterObjetoPorId(db, model.Id);
            if (objeto == null)
            {
                TempData["erro"] = $@"Erro ao tentar editar o transporte {objeto.Nome}";
                return RedirectToAction("Index");
            }

            ModelParaObjeto(model, objeto);

            try
            {
                servico.Salvar(db, objeto);
                TempData["sucesso"] = $@"Transporte salvo com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar editar o transporte {objeto.Nome}";
            }

            return RedirectToAction("Index");
        }

        [DireitoAcesso(Constantes.AC_APG_TIPODOC)]
        public ActionResult Delete(int id)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Apagar Transporte";

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
                TempData["sucesso"] = $@"O transporte {objeto.Nome} foi apagado com sucesso!";
            }
            catch
            {
                TempData["erro"] = $@"Erro ao tentar apagar o transporte {objeto.Nome}";
            }
            return RedirectToAction("Index");
        }
    }
}