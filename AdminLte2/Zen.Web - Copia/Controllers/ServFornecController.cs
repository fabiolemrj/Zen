using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;
using Zen.Web.Models;
using Zen.Web.Servico;
using Zen.Web.ViewModels.ServFornecViewModel;

namespace Zen.Web.Controllers
{
    public class ServFornecController : CustomController
    {
        ZenContext db = new ZenContext();
        ServicoServFornec servico = new ServicoServFornec();
        ServicoFornecedor servFornecedor = new ServicoFornecedor();
        ServicoServ servServicos = new ServicoServ();

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
                           <i class='fa fa-industry'> </i>
                           <a href='/Fornecedor'> Fornecedor </a>
                         </li>
                         <li class='active'>
                           <i class='fa fa-industry'> </i>
                           <a href='/ServFornec'> Fornecedor </a>
                         </li>
                    </ol>";
        }

        private string CreateBreadCrumbCreatEdit()
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
                           <i class='fa fa-industry'> </i>
                           <a href='/Fornecedor'> Fornecedor </a>
                         </li>
                         <li class='active'>
                           <i class='fa fa-industry'> </i>
                           <a href='/ServFornec'> Fornecedor </a>
                         </li>
                    </ol>";
        }

        // GET: ServFornec
        [DireitoAcesso(Constantes.AC_CONS_CAD_FORNEC)]
        public ActionResult Index(int idFornec, int pagina = 1, int tamPag = Constantes.TamanhoPagina)
        {
            TempData["breadcrumb"] = CreateBreadCrumbIndex();
            TempData["nometela"] = "Serviços por Fornecedor";
            
            var lista = PrepararViewModel(idFornec, pagina, tamPag);

            return View(lista);

        }

        private IPagedList<ServFornec> PrepararViewModel(int idFornec, int pagina, int tamPag)
        {
            //PopularViewBag();
            var lstCli = new List<ServFornec>();

            lstCli = servico.ObterListaObjetos(db, idFornec).ToList();

            ViewBag.TamanhoPagina = tamPag;
            ViewBag.Fornecedor = servFornecedor.ObterObjetoPorId(db, idFornec).Nome;
            ViewBag.idFornecedor = idFornec;
            return lstCli.ToPagedList(pagina, tamPag);
        }


        [DireitoAcesso(Constantes.AC_EDIT_CAD_FORNEC)]
        public ActionResult Create(int idFornec)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Associar Serviço ao fornecedor";
            TempData["lboper"] = "Novo";

            var model = CriarViewModelAddEdit(idFornec);

            return View(model);
        }

        private CreateEditViewModel CriarViewModelAddEdit(int idFornec)
        {
            PreencherCombos(idFornec);
            var model = new CreateEditViewModel();
            model.IdFornecedor = idFornec;
            return model;
        }

        private void PreencherCombos(int idFornec)
        {

            ViewBag.Fornecedor = servFornecedor.ObterObjetoPorId(db, idFornec).Nome;

            var lstserv = servServicos.ObterListaObjetos(db, "").OrderBy(c=>c.Nome).ToList();
            var lstFornecServ = servico.ObterListaObjetos(db, idFornec).ToList();
            var lstResult = new List<Serv>();

            foreach (var itmServ in lstserv)
            {
                var obj = lstFornecServ.FirstOrDefault(c => c.IdFornecedor == idFornec && c.IdServico == itmServ.Id);
                if (obj == null)
                {
                    lstResult.Add(itmServ);
                }
            }
            ViewBag.idFornecedor = idFornec;
            ViewBag.Servico = new SelectList(lstResult, "Id", "Nome");
        }

        [HttpPost]
        public ActionResult Create(CreateEditViewModel model)
        {

            if (!ModelState.IsValid)
            {
                TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
                TempData["nometela"] = "Associar Serviço ao fornecedor";
                TempData["lboper"] = "Novo";

                PreencherCombos(model.IdFornecedor);
                return View(model);
            }

            var objeto = new ServFornec();
            ModelParaObjeto(model, objeto);

            try
            {
                servico.Salvar(db, objeto);
                TempData["sucesso"] = $@"Fornecedor salvo com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar salvar o fornecedor {objeto.Serv.Nome}";
            }
            return RedirectToAction("Index",new { idFornec = model.IdFornecedor });
            //return RedirectToAction("Index", new { id = maloteId });
        }

        private ServFornec ModelParaObjeto(CreateEditViewModel model, ServFornec objeto)
        {

            objeto.IdFornecedor = model.IdFornecedor;
            objeto.IdServico = model.IdServico;

            return objeto;
        }

        [DireitoAcesso(Constantes.AC_APG_CAD_FORNEC)]
        public ActionResult Delete(int idFornec,int idServ)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Apagar Serviço do fornecedor";

            var setor = servico.ObterObjetoPorId(db, idFornec,idServ);
            DeleteViewModel model = new DeleteViewModel();
            model.IdFornec = setor.IdFornecedor;
            model.IdServ = setor.IdServico;
            model.Nome = servServicos.ObterObjetoPorId(db, idServ).Nome;
            return View(model);
        }



        [HttpPost]
        public ActionResult Delete(DeleteViewModel model)
        {
            var objeto = servico.ObterObjetoPorId(db, model.IdFornec,model.IdServ);
            try
            {
                if (objeto != null)
                {
                    servico.Delete(db, objeto);

                }
                TempData["sucesso"] = $@"O serviço {objeto.Serv.Nome} foi apagado com sucesso!";
            }
            catch
            {
                TempData["erro"] = $@"Erro ao tentar apagar o tipo de documento {objeto.Serv.Nome}";
            }
            return RedirectToAction("Index", new { idFornec = model.IdFornec });
            //return RedirectToAction("Index");
        }

      
    }
}