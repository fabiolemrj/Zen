using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;
using Zen.Web.Models;
using Zen.Web.Servico;
using Zen.Web.ViewModels.BancoViewModel;

namespace Zen.Web.Controllers
{
    public class BancoController : CustomController
    {
        ZenContext db = new ZenContext();
        ServicoBanco servico = new ServicoBanco();

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
                           <i class='fa fa-bank'> </i>
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
                           <a href = '/banco' > Bancos </a>
                     
                         <li class='active'> Cadastro
                         </li>
                    </ol>";
        }

        private IPagedList<Banco> PrepararViewModel(string filtro, int pagina, int tamPag)
        {
            //PopularViewBag();
            var lst = new List<Banco>();

            lst = servico.ObterListaObjetos(db,filtro).ToList();

            ViewBag.Filtro = filtro;
            ViewBag.TamanhoPagina = tamPag;
            return lst.ToPagedList(pagina, tamPag);
        }

        // GET: Banco
        [DireitoAcesso(Constantes.AC_CONS_BANCOS)]
        public ActionResult Index(string filtro = "", int pagina = 1, int tamPag = Constantes.TamanhoPagina)
        {
            TempData["breadcrumb"] = CreateBreadCrumbIndex();
            TempData["nometela"] = "Bancos";

            var lista = PrepararViewModel(filtro, pagina, tamPag);

            return View(lista);
        }

        [DireitoAcesso(Constantes.AC_INC_CAD_BANCO)]
        public ActionResult Create()
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Novo Banco";
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
                TempData["nometela"] = "Novo Banco";
                TempData["lboper"] = "Novo";

                return View(model);
            }

            var banco = new Banco();
            ModelParaObjeto(model, banco);

            try
            {
                servico.Salvar(db, banco);
                TempData["sucesso"] = $@"Banco salvo com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar editar o banco {banco.Nome}";
            }
            return RedirectToAction("Index");
        }

        private Banco ModelParaObjeto(CreateEditViewModel model, Banco banco)
        {
            banco.IdBanco = model.IdBanco;
            banco.Nome = model.Nome;

            return banco;
        }


        private CreateEditViewModel CriarViewModelAddEdit()
        {            
            var model = new CreateEditViewModel();
            return model;
        }

        [DireitoAcesso(Constantes.AC_EDIT_CAD_BANCO)]
        public ActionResult Edit(string id)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Editar Banco";
            TempData["lboper"] = "Editar";

            var model = CriarViewModelAddEdit();
            var banco = servico.ObterObjetoPorId(db, id);
            if (banco == null)
            {
                TempData["erro"] = $@"Erro ao tentar editar o banco {banco.Nome}";
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
                TempData["nometela"] = "Editar Banco";
                TempData["lboper"] = "Editar";
                return View("Create", model);
            }

            var banco = servico.ObterObjetoPorId(db, model.IdBanco);
            if (banco == null)
            {
                TempData["erro"] = $@"Erro ao tentar editar o banco {banco.Nome}";
                return RedirectToAction("Index");
            }

            ModelParaObjeto(model, banco);

            try
            {
                servico.Salvar(db, banco);
                TempData["sucesso"] = $@"Banco salvo com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar editar o banco {banco.Nome}";
            }

            return RedirectToAction("Index");
        }


        private void ObjetoParaModel(Banco banco, CreateEditViewModel model)
        {
            model.IdBanco = banco.IdBanco;
            model.Nome = banco.Nome;
        }

        [DireitoAcesso(Constantes.AC_EDIT_CAD_BANCO)]
        public ActionResult Delete(string id)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Apagar Banco";

            var banco = servico.ObterObjetoPorId(db, id);
            DeleteViewModel model = new DeleteViewModel();
            model.IdBanco = banco.IdBanco;
            model.Nome = banco.Nome;
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(DeleteViewModel model)
        {
            var banco = servico.ObterObjetoPorId(db, model.IdBanco);
            try
            {
                if (banco != null)
                {
                    servico.Delete(db, banco);
                }
                TempData["sucesso"] = $@"O banco {banco.Nome} foi apagado com sucesso!";
            }
            catch
            {
                TempData["erro"] = $@"Erro ao tentar apagar o banco {banco.Nome}";
            }
            return RedirectToAction("Index");
        }
    }
}