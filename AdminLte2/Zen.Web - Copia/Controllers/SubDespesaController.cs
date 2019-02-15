using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;
using Zen.Web.Models;
using Zen.Web.Servico;
using Zen.Web.ViewModels.SubDespesaViewModel;

namespace Zen.Web.Controllers
{
    public class SubDespesaController : CustomController
    {
        ZenContext db = new ZenContext();
        ServicoSubDespesa servico = new ServicoSubDespesa();
        ServicoDespesa servDesp = new ServicoDespesa();

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
                         <li>
                           <i class='fa fa-dollar'> </i>
                           <a href='/Despesa'> Despesa </a>
                         </li>
                        <li class='active'>
                           <a href='/SubDespesa'> SubItens de Despesa </a>
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
                           <i class='fa fa-dollar'> </i>
                           <a href = '/Despesa' > Despesa </a>
                        </li>
                        <li>
                           <a href = '/SubDespesa' > Itens de Despesa </a>
                        </li>
                         <li class='active'> Cadastro
                         </li>
                    </ol>";
        }
        // GET: SubDespesa
        [DireitoAcesso(Constantes.AC_CONS_ITENSDESP)]
        public ActionResult Index(int idDesp, string filtro = "", int pagina = 1, int tamPag = Constantes.TamanhoPagina)
        {
            TempData["breadcrumb"] = CreateBreadCrumbIndex();
            TempData["nometela"] = "Itens de Despesa";

            var lista = PrepararViewModel(idDesp, filtro, pagina, tamPag);

            return View(lista);
        }

        private IPagedList<SubDespesa> PrepararViewModel(int idDesp,string filtro, int pagina, int tamPag)
        {
            //PopularViewBag();
            var lst = new List<SubDespesa>();

            lst = servico.ObterListaObjetos(db,idDesp, filtro).ToList();
            ViewBag.idDesp = idDesp;
            ViewBag.Filtro = filtro;
            ViewBag.TamanhoPagina = tamPag;
            ViewBag.Nome = servDesp.ObterObjetoPorId(db, idDesp).Nome;
            return lst.ToPagedList(pagina, tamPag);
        }

        [DireitoAcesso(Constantes.AC_INC_ITENSDESP)]
        public ActionResult Create(int idDesp)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Novo Subitens de despesa";
            TempData["lboper"] = "Novo";

            var model = CriarViewModelAddEdit(idDesp);

            return View(model);
        }

        private CreateEditViewModel CriarViewModelAddEdit(int idDesp)
        {
            var model = new CreateEditViewModel();
            model.idDesp = idDesp;
            model.NmDespesa = servDesp.ObterObjetoPorId(db, idDesp).Nome;
            return model;
        }

        [HttpPost]
        public ActionResult Create(CreateEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
                TempData["nometela"] = "Novo SubItem de Despesa";
                TempData["lboper"] = "Novo";

                return View(model);
            }

            var subdespesa = new SubDespesa();
            ModelParaObjeto(model, subdespesa);

            try
            {
                servico.Salvar(db, subdespesa);
                TempData["sucesso"] = $@"SubItem de despesa salvo com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar editar o Subitem de despesa {subdespesa.Nome}";
            }
            return RedirectToAction("Index",new { model.idDesp });
        }

        private SubDespesa ModelParaObjeto(CreateEditViewModel model, SubDespesa subdespesa)
        {
            subdespesa.Id = model.Id;
            subdespesa.Nome = model.Nome;
            subdespesa.Codigo = model.Codigo;
            subdespesa.IdSubDesp = model.IdSubDesp;
            subdespesa.IdDesp = model.idDesp;
                  

            return subdespesa;
        }

        [DireitoAcesso(Constantes.AC_EDIT_ITENSDESP)]
        public ActionResult Edit(int id)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Editar SubItens de despesa";
            TempData["lboper"] = "Editar";

            
            var subdespesa = servico.ObterObjetoPorId(db, id);
            var model = CriarViewModelAddEdit(subdespesa.IdDesp);
            if (subdespesa == null)
            {
                TempData["erro"] = $@"Erro ao tentar editar o Subitem de despesa {subdespesa.Nome}";
                return RedirectToAction("Index");
            }
            ObjetoParaModel(subdespesa, model);

            return View("Create", model);
        }

        private void ObjetoParaModel(SubDespesa subdespesa, CreateEditViewModel model)
        {
            model.Id = subdespesa.Id;
            model.Codigo = subdespesa.Codigo;
            model.Nome = subdespesa.Nome;
            model.IdSubDesp = subdespesa.IdSubDesp;
            model.idDesp = subdespesa.IdDesp;
            model.NmDespesa = servDesp.ObterObjetoPorId(db, subdespesa.IdDesp).Nome;
        }

        [HttpPost]
        public ActionResult Edit(CreateEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
                TempData["nometela"] = "Editar SubItens de despesa";
                TempData["lboper"] = "Editar";
                return View("Create", model);
            }

            var despesa = servico.ObterObjetoPorId(db, model.Id);
            if (despesa == null)
            {
                TempData["erro"] = $@"Erro ao tentar editar o Subitem de despesa {despesa.Nome}";
                                
            }

            ModelParaObjeto(model, despesa);

            try
            {
                servico.Salvar(db, despesa);
                TempData["sucesso"] = $@"SubItem de despesa salvo com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar editar o Subitem de despesa {despesa.Nome}";
            }
            return RedirectToAction("Index", new { model.idDesp });
            
        }


        [DireitoAcesso(Constantes.AC_APG_ITENSDESP)]
        public ActionResult Delete(int id)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Apagar SubItem de despesa";

            var subdespesa = servico.ObterObjetoPorId(db, id);
            DeleteViewModel model = new DeleteViewModel();
            model.Id = subdespesa.Id;
            model.Nome = subdespesa.Nome;
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(DeleteViewModel model)
        {
            var subdespesa = servico.ObterObjetoPorId(db, model.Id);
            var idDesp = 0;
            try
            {
                if (subdespesa != null)
                {
                    idDesp = subdespesa.IdDesp;
                    servico.Delete(db, subdespesa);
                }
                TempData["sucesso"] = $@"O subitem de despesa {subdespesa.Nome} foi apagado com sucesso!";
            }
            catch
            {
                TempData["erro"] = $@"Erro ao tentar apagar o subitem de despesa {subdespesa.Nome}";
            }
            return RedirectToAction("Index", new { idDesp });
            
        }

    }
}