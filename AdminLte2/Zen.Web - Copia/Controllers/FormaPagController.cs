using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;
using Zen.Web.Models;
using Zen.Web.Servico;
using Zen.Web.ViewModels.FormaPagViewModel;

namespace Zen.Web.Controllers
{
    public class FormaPagController : CustomController
    {
        ZenContext db = new ZenContext();
        ServicoFormaPag servico = new ServicoFormaPag();

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
                           <i class='fa fa-money'> </i>
                           <a href='/FormaPag'> Formas de Pagamento </a>
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
                           <i class='fa fa-money'> </i>
                           <a href = '/FormaPag' > Formas de Pagamento </a>
                     
                         <li class='active'> Cadastro
                         </li>
                    </ol>";
        }
        // GET: FormaPag
        [DireitoAcesso(Constantes.AC_CONS_FORMAPGM)]
        public ActionResult Index(string filtro = "", int pagina = 1, int tamPag = Constantes.TamanhoPagina)
        {
            TempData["breadcrumb"] = CreateBreadCrumbIndex();
            TempData["nometela"] = "Formas de Pagamento";

            var lista = PrepararViewModel(filtro, pagina, tamPag);

            return View(lista);
        }

        private IPagedList<FormaPag> PrepararViewModel(string filtro, int pagina, int tamPag)
        {
            //PopularViewBag();
            var lst = new List<FormaPag>();

            lst = servico.ObterListaObjetos(db, filtro).ToList();

            ViewBag.Filtro = filtro;
            ViewBag.TamanhoPagina = tamPag;
            return lst.ToPagedList(pagina, tamPag);
        }

        [DireitoAcesso(Constantes.AC_INC_FORMAPGM)]
        public ActionResult Create()
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Nova Forma de Pagamento";
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
                TempData["nometela"] = "Nova Forma de Pagamento";
                TempData["lboper"] = "Nova";

                return View(model);
            }

            var formapag = new FormaPag();
            ModelParaObjeto(model, formapag);

            try
            {
                servico.Salvar(db, formapag);
                TempData["sucesso"] = $@"Forma de pagamento salvo com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar editar a forma de pagamento {formapag.Nome}";
            }
            return RedirectToAction("Index");
        }

        private FormaPag ModelParaObjeto(CreateEditViewModel model, FormaPag formapag)
        {
            formapag.Id = model.Id;
            formapag.Nome = model.Nome;

            return formapag;
        }

        [DireitoAcesso(Constantes.AC_EDIT_FORMAPGM)]
        public ActionResult Edit(int id)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Editar Formas de Pagamento";
            TempData["lboper"] = "Editar";

            var model = CriarViewModelAddEdit();
            var formapag = servico.ObterObjetoPorId(db, id);
            if (formapag == null)
            {
                TempData["erro"] = $@"Erro ao tentar editar a forma de pagamento {formapag.Nome}";
                return RedirectToAction("Index");
            }
            ObjetoParaModel(formapag, model);

            return View("Create", model);
        }

        [HttpPost]
        public ActionResult Edit(CreateEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
                TempData["nometela"] = "Editar Forma de Pagamento";
                TempData["lboper"] = "Editar";
                return View("Create", model);
            }

            var formapag = servico.ObterObjetoPorId(db, model.Id);
            if (formapag == null)
            {
                TempData["erro"] = $@"Erro ao tentar editar a forma de pagamento {formapag.Nome}";
                return RedirectToAction("Index");
            }

            ModelParaObjeto(model, formapag);

            try
            {
                servico.Salvar(db, formapag);
                TempData["sucesso"] = $@"Forma de pagamento salva com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar editar a forma de pagamento {formapag.Nome}";
            }

            return RedirectToAction("Index");
        }

        private void ObjetoParaModel(FormaPag formapag, CreateEditViewModel model)
        {
            model.Id = formapag.Id;
            model.Nome = formapag.Nome;
        }


        [DireitoAcesso(Constantes.AC_APG_FORMAPGM)]
        public ActionResult Delete(int id)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Apagar Forma de Pagamento";

            var faca = servico.ObterObjetoPorId(db, id);
            DeleteViewModel model = new DeleteViewModel();
            model.Id = faca.Id;
            model.Nome = faca.Nome;
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(DeleteViewModel model)
        {
            var formapag = servico.ObterObjetoPorId(db, model.Id);
            try
            {
                if (formapag != null)
                {
                    servico.Delete(db, formapag);
                }
                TempData["sucesso"] = $@"A forma de pagamento {formapag.Nome} foi apagada com sucesso!";
            }
            catch
            {
                TempData["erro"] = $@"Erro ao tentar apagar a forma de pagamento {formapag.Nome}";
            }
            return RedirectToAction("Index");
        }

    }
}