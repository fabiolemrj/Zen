using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;
using Zen.Web.Models;
using Zen.Web.Servico;
using Zen.Web.ViewModels.ServViewModel;

namespace Zen.Web.Controllers
{
    public class ServController : CustomController
    {
        ZenContext db = new ZenContext();
        ServicoServ servico = new ServicoServ();
        // GET: Serv

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
                           <i class='fa fa-bookmark'> </i>
                           <a href='/Serv'> Serviços </a>
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
                           <i class='fa fa-bookmark'> </i>
                           <a href = '/Serv' > Zona </a>
                         </li>
                         <li class='active'> Cadastro
                         </li>
                    </ol>";
        }

        [DireitoAcesso(Constantes.AC_CONS_CAD_SERV)]
        public ActionResult Index(string filtro = "", int pagina = 1, int tamPag = Constantes.TamanhoPagina)
        {
            TempData["breadcrumb"] = CreateBreadCrumbIndex();
            TempData["nometela"] = "Serviços";

            var lista = PrepararViewModel(filtro, pagina, tamPag);

            return View(lista);
        }

        private IPagedList<Serv> PrepararViewModel(string filtro, int pagina, int tamPag)
        {
            //PopularViewBag();
            var lst = new List<Serv>();

            lst = servico.ObterListaObjetos(db, filtro).ToList();

            ViewBag.Filtro = filtro;
            ViewBag.TamanhoPagina = tamPag;
            return lst.ToPagedList(pagina, tamPag);
        }

        [DireitoAcesso(Constantes.AC_INC_CAD_SERV)]
        public ActionResult Create()
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Novo Serviço";
            TempData["lboper"] = "Novo";

            var model = CriarViewModelAddEdit();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CreateEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                PreencherCombos();
                TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
                TempData["nometela"] = "Novo Serviço";
                TempData["lboper"] = "Novo";

                return View(model);
            }

            var _servico = new Serv();
            ModelParaObjeto(model, _servico);

            try
            {
                servico.Salvar(db, _servico);
                TempData["sucesso"] = $@"Serviço salvo com sucesso!";
            }
            catch (Exception e)
            {
                var msg = e.Message;
                TempData["erro"] = $@"Erro ao tentar editar o serviço {_servico.Nome}";
            }
            return RedirectToAction("Index");
        }

        private Serv ModelParaObjeto(CreateEditViewModel model, Serv serv)
        {

            serv.Id = model.Id;
            serv.Nome = model.Nome;
            serv.IdDesp = model.IdDesp;
            serv.IdSubDesp = model.IdSubDesp;
            serv.Tipo = model.Tipo;
            serv.Campo_St = model.Campo_St;
            
            return serv;
        }

        private CreateEditViewModel CriarViewModelAddEdit()
        {
            PreencherCombos();

            var model = new CreateEditViewModel();
            return model;
        }

        private void PreencherCombos()
        {
            var servSubDesp = new ServicoSubDespesa();
            ViewBag.SubDesp = new SelectList(servSubDesp.ObterListaObjetos(db,""), "Id", "Nome");            
        }

        [DireitoAcesso(Constantes.AC_EDIT_CAD_SERV)]
        public ActionResult Edit(int id)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Editar Serviço";
            TempData["lboper"] = "Editar";

            var model = CriarViewModelAddEdit();
            var _servico = servico.ObterObjetoPorId(db, id);
            if (_servico == null)
            {
                TempData["erro"] = $@"Erro ao tentar editar o serviço {_servico.Nome}";
                return RedirectToAction("Index");
            }
            ObjetoParaModel(_servico, model);

            return View("Create", model);
        }

        [HttpPost]
        public ActionResult Edit(CreateEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                PreencherCombos();
                TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
                TempData["nometela"] = "Editar Serviço";
                TempData["lboper"] = "Editar";
                return View("Create", model);
            }

            var _servico = servico.ObterObjetoPorId(db, model.Id);
            if (_servico == null)
            {
                TempData["erro"] = $@"Erro ao tentar editar os serviço {_servico.Nome}";
                return RedirectToAction("Index");
            }

            ModelParaObjeto(model, _servico);

            try
            {
                servico.Salvar(db, _servico);
                TempData["sucesso"] = $@"Serviço salvo com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar editar o serviço {_servico.Nome}";
            }

            return RedirectToAction("Index");
        }


        private void ObjetoParaModel(Serv _serv, CreateEditViewModel model)
        {
            model.Id = _serv.Id;
            model.IdDesp = _serv.IdDesp;
            model.Nome = _serv.Nome;
            model.IdSubDesp = _serv.IdSubDesp;
            model.Tipo = _serv.Tipo;
            model.Campo_St = _serv.Campo_St;
        }

        [DireitoAcesso(Constantes.AC_APG_CAD_SERV)]
        public ActionResult Delete(int id)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Apagar Serviço";

            var maquina = servico.ObterObjetoPorId(db, id);
            DeleteViewModel model = new DeleteViewModel();
            model.Id = maquina.Id;
            model.Nome = maquina.Nome;
            return View(model);

        }

        [HttpPost]
        public ActionResult Delete(DeleteViewModel model)
        {
            var _servico = servico.ObterObjetoPorId(db, model.Id);
            try
            {
                if (_servico != null)
                {
                    servico.Delete(db, _servico);
                }
                TempData["sucesso"] = $@"O serviço {_servico.Nome} foi apagado com sucesso!";
            }
            catch
            {
                TempData["erro"] = $@"Erro ao tentar apagar o serviço {_servico.Nome}";
            }
            return RedirectToAction("Index");
        }
    }
}