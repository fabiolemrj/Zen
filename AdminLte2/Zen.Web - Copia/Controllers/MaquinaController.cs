using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;
using Zen.Web.Models;
using Zen.Web.Servico;
using Zen.Web.Utils;
using Zen.Web.ViewModels.MaquinaViewModel;

namespace Zen.Web.Controllers
{
    public class MaquinaController : CustomController
    {
        ZenContext db = new ZenContext();
        ServicoMaquina servico = new ServicoMaquina();

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
                           <i class='fa fa-print'> </i>
                           <a href='/Maquina'> Maquinas </a>
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
                           <i class='fa fa-print'> </i>
                           <a href = '/Maquina' > Maquinas </a>
                     
                         <li class='active'> Cadastro
                         </li>
                    </ol>";
        }

        // GET: Maquina
        [DireitoAcesso(Constantes.AC_CONS_CAD_MAQ)]
        public ActionResult Index(string filtro = "", int pagina = 1, int tamPag = Constantes.TamanhoPagina)
        {
            TempData["breadcrumb"] = CreateBreadCrumbIndex();
            TempData["nometela"] = "Maquinas";

            var lista = PrepararViewModel(filtro, pagina, tamPag);

            return View(lista);
        }

        private IPagedList<Maquina> PrepararViewModel(string filtro, int pagina, int tamPag)
        {
            //PopularViewBag();
            var lst = new List<Maquina>();

            lst = servico.ObterListaObjetos(db, filtro).ToList();

            ViewBag.Filtro = filtro;
            ViewBag.TamanhoPagina = tamPag;
            return lst.ToPagedList(pagina, tamPag);
        }

        [DireitoAcesso(Constantes.AC_INC_CAD_MAQ)]
        public ActionResult Create()
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Nova Maquina";
            TempData["lboper"] = "Nova";

            var model = CriarViewModelAddEdit();

            return View(model);
        }

        private CreateEditViewModel CriarViewModelAddEdit()
        {
            PreencherCombos();
            
            var model = new CreateEditViewModel();
            return model;
        }

        [HttpPost]
        public ActionResult Create(CreateEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                PreencherCombos();
                TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
                TempData["nometela"] = "Nova Maquina";
                TempData["lboper"] = "Nova";

                return View(model);
            }
            
            var maquina = new Maquina();
            ModelParaObjeto(model, maquina);

            try
            {
                servico.Salvar(db, maquina);
                TempData["sucesso"] = $@"Maquina salvo com sucesso!";
            }
            catch (Exception e)
            {
                var msg = e.Message;
                TempData["erro"] = $@"Erro ao tentar editar a maquina {maquina.Nome}";
            }
            return RedirectToAction("Index");
        }

        private Maquina ModelParaObjeto(CreateEditViewModel model, Maquina maquina)
        {
            
            maquina.Id = model.Id;
            maquina.Nome = model.Nome;
            maquina.IdImpressor = model.IdImpressor;
            maquina.IdAuxiliar = model.IdAuxiliar;
            maquina.Situacao = model.Situacao;
            maquina.Classificacao = model.Classificacao;

            return maquina;
        }

        [DireitoAcesso(Constantes.AC_EDIT_CAD_MAQ)]
        public ActionResult Edit(int id)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Editar Maquina";
            TempData["lboper"] = "Editar";
            
            var model = CriarViewModelAddEdit();
            var maquina = servico.ObterObjetoPorId(db, id);
            if (maquina == null)
            {
                TempData["erro"] = $@"Erro ao tentar editar a maquina {maquina.Nome}";
                return RedirectToAction("Index");
            }
            ObjetoParaModel(maquina, model);

            return View("Create", model);
        }

        private void ObjetoParaModel(Maquina maquina, CreateEditViewModel model)
        {
            model.Id = maquina.Id;
            model.IdAuxiliar = maquina.IdAuxiliar;
            model.Nome = maquina.Nome;
            model.IdImpressor = maquina.IdImpressor;
            model.Situacao = maquina.Situacao;
            model.Classificacao = maquina.Classificacao;
        }

        private void PreencherCombos()
        {
            var servImpressor = new ServicoImpressor();
            ViewBag.Impressor = new SelectList(servImpressor.ObterListaObjetos(db, "", "S"),"Id","Nome");
            ViewBag.Auxiliar = new SelectList(servImpressor.ObterListaObjetos(db, "", "S"), "Id", "Nome");
            ViewBag.ObterAtivo = new SelectList(ListasGenericas.ObterAtivo, "Sigla", "Nome");
            ViewBag.Classificacao = new SelectList(ListasGenericas.ObterClassif, "Sigla", "Nome");
            
            //new SelectList(ListasGenericas.ObterEstados, "Sigla", "Nome");
        }

        [HttpPost]
        public ActionResult Edit(CreateEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                PreencherCombos();
                TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
                TempData["nometela"] = "Editar Maquina";
                TempData["lboper"] = "Editar";
                return View("Create", model);
            }

            var maquina = servico.ObterObjetoPorId(db, model.Id);
            if (maquina == null)
            {
                TempData["erro"] = $@"Erro ao tentar editar a maquina {maquina.Nome}";
                return RedirectToAction("Index");
            }

            ModelParaObjeto(model, maquina);

            try
            {
                servico.Salvar(db, maquina);
                TempData["sucesso"] = $@"Maquina salva com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar editar a maquina {maquina.Nome}";
            }

            return RedirectToAction("Index");
        }

        [DireitoAcesso(Constantes.AC_APG_CAD_MAQ)]
        public ActionResult Delete(int id)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Apagar Maquina";

            var maquina = servico.ObterObjetoPorId(db, id);
            DeleteViewModel model = new DeleteViewModel();
            model.Id = maquina.Id;
            model.Nome = maquina.Nome;
            return View(model);

        }

        [HttpPost]
        public ActionResult Delete(DeleteViewModel model)
        {
            var maquina = servico.ObterObjetoPorId(db, model.Id);
            try
            {
                if (maquina != null)
                {
                    servico.Delete(db, maquina);
                }
                TempData["sucesso"] = $@"A maquina {maquina.Nome} foi apagada com sucesso!";
            }
            catch
            {
                TempData["erro"] = $@"Erro ao tentar apagar a maquina {maquina.Nome}";
            }
            return RedirectToAction("Index");
        }

    }
}