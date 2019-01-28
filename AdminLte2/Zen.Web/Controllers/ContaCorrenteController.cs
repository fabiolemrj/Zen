using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;
using Zen.Web.Models;
using Zen.Web.Servico;
using Zen.Web.Utils;
using Zen.Web.ViewModels.ContaCorrente;

namespace Zen.Web.Controllers
{
    public class ContaCorrenteController : CustomController
    {
        ZenContext db = new ZenContext();
        ServicoContaCorrente servico = new ServicoContaCorrente();
        ServicoBanco servBanco = new ServicoBanco();

         private string CreateBreadCrumbIndex()
        {
            return $@"<ol class='breadcrumb'>
                         <li>                            
                             <a href ='/'> Principal </a>
                         </li>
                         <li>      
                             <i class='fa fa-folder'> </i>
                             <a href ='/'> Financeiro </a>
                         </li>
                         <li class='active'>
                           <i class='fa fa-money'> </i>
                           <a href='/Fornecedor'> Contas Correntes </a>
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
                             <a href ='/'> Financeiro </a>
                         </li>
                         <li>
                           <i class='fa fa-money'> </i>
                           <a href='/Fornecedor'> Contas Correntes </a>
                         </li>
                         <li class='active'> Cadastro
                         </li>
                    </ol>";
        }

        // GET: ContaCorrente
        [DireitoAcesso(Constantes.AC_CONS_CAD_CC)]
        public ActionResult Index(int tpfiltro = 1, string filtro = "", int pagina = 1, int tamPag = Constantes.TamanhoPagina)
        {
            TempData["breadcrumb"] = CreateBreadCrumbIndex();
            TempData["nometela"] = "Contas Correntes";


            var lista = PrepararViewModel(tpfiltro, filtro, pagina, tamPag);

            return View(lista);

        }

        private IPagedList<ContaCorrente> PrepararViewModel(int tpfiltro, string filtro, int pagina, int tamPag)
        {
            //PopularViewBag();
            var lista = new List<ContaCorrente>();

            lista = servico.ObterListaObjetos(db, filtro, tpfiltro).ToList();

            ViewBag.Filtro = filtro;
            ViewBag.TamanhoPagina = tamPag;
            ViewBag.TpFiltro = tpfiltro;
            return lista.ToPagedList(pagina, tamPag);
        }

        [DireitoAcesso(Constantes.AC_INC_CAD_CC)]
        public ActionResult Create()
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Nova Conta Corrente";
            TempData["lboper"] = "Nova";

            var model = CriarViewModelAddEdit();
            model.Id = -1;
            model.DtLanc = DateTime.Now;
            model.SaldoAtual = 0;
            model.SaldoIni = 0;

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CreateEditViewModel model)
        {

            if (!ModelState.IsValid)
            {
                TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
                TempData["nometela"] = "Nova Conta Corrente";
                TempData["lboper"] = "Nova";

                ViewBag.Banco = new SelectList(servBanco.ObterListaObjetos(db, ""), "IdBanco", "Nome");
                ViewBag.SimNao = new SelectList(ListasGenericas.ObterSimNao, "Sigla", "Nome");

                return View(model);
            }
            
            var objeto = new ContaCorrente();
            ModelParaObjeto(model, objeto);

            try
            {
                servico.Salvar(db, objeto);
                TempData["sucesso"] = $@"Conta Corrente salva com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar salvar a conta corrente {objeto.NumeroConta}";
            }
            return RedirectToAction("Index");
        }

        private CreateEditViewModel CriarViewModelAddEdit()
        {
            ViewBag.Banco = new SelectList(servBanco.ObterListaObjetos(db,""), "IdBanco", "Nome");
            ViewBag.SimNao = new SelectList(ListasGenericas.ObterSimNao, "Sigla", "Nome");

            var model = new CreateEditViewModel();
            return model;
        }

        private ContaCorrente ModelParaObjeto(CreateEditViewModel model, ContaCorrente objeto)
        {
            objeto.Id = model.Id;
            objeto.IdBanco = model.IdBanco;
            objeto.Investimento = model.Investimento;
            objeto.NomeAgencia = model.NomeAgencia;
            objeto.NumeroAgencia = model.NumeroAgencia;
            objeto.NumeroConta = model.NumeroConta;
            objeto.SaldoAtual = model.SaldoAtual;
            objeto.SaldoIni = model.SaldoIni;
            
            return objeto;
        }

        [DireitoAcesso(Constantes.AC_EDIT_CAD_CC)]
        public ActionResult Edit(int id)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Editar Conta Corrente";
            TempData["lboper"] = "Editar";

            var model = CriarViewModelAddEdit();
            var impressor = servico.ObterObjetoPorId(db, id);
            if (impressor == null)
            {
                TempData["erro"] = $@"Erro ao tentar editar o impressor {impressor.NomeAgencia}";
                return RedirectToAction("Index");
            }
            ObjetoParaModel(impressor, model);

            return View("Create", model);
        }


        private void ObjetoParaModel(ContaCorrente objeto, CreateEditViewModel model)
        {
            model.Id = objeto.Id;
            model.NomeAgencia = objeto.NomeAgencia;
            model.IdBanco = objeto.IdBanco;
            model.Investimento = objeto.Investimento;
            model.NumeroAgencia = objeto.NumeroAgencia;
            model.NumeroConta = objeto.NumeroConta;
            model.SaldoAtual = objeto.SaldoAtual;
            model.SaldoIni = objeto.SaldoIni;
        }

        [HttpPost]
        public ActionResult Edit(CreateEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
                TempData["nometela"] = "Editar Conta Corrente";
                TempData["lboper"] = "Editar";
                return View("Create", model);
            }

            var objeto = servico.ObterObjetoPorId(db, model.Id);
            if (objeto == null)
            {
                TempData["erro"] = $@"Erro ao tentar editar a Conta Corrente {objeto.NomeAgencia}";
                return RedirectToAction("Index");
            }

            ModelParaObjeto(model, objeto);

            try
            {
                servico.Salvar(db, objeto);
                TempData["sucesso"] = $@"Conta Corrente salva com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar editar a conta corrente {objeto.NomeAgencia}";
            }

            return RedirectToAction("Index");
        }

        [DireitoAcesso(Constantes.AC_APG_CAD_CC)]
        public ActionResult Delete(int id)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Apagar Conta Corrente";

            var impressor = servico.ObterObjetoPorId(db, id);
            DeleteViewModel model = new DeleteViewModel();
            model.Id = impressor.Id;
            model.Nome = impressor.NomeAgencia;
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(DeleteViewModel model)
        {
            var impressor = servico.ObterObjetoPorId(db, model.Id);
            try
            {
                if (impressor != null)
                {
                    servico.Delete(db, impressor);
                }
                TempData["sucesso"] = $@"A Conta Corrente {impressor.NomeAgencia} foi apagada com sucesso!";
            }
            catch
            {
                TempData["erro"] = $@"Erro ao tentar apagar a conta corrente {impressor.NomeAgencia}";
            }
            return RedirectToAction("Index");
        }

    }
}