using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using X.PagedList;
using Zen.Web.Models;
using Zen.Web.Servico;
using Zen.Web.Utils;
using Zen.Web.ViewModels.ContaPagarViewModel;

namespace Zen.Web.Controllers
{
    public class ContaPagarController : CustomController
    {
        ZenContext db = new ZenContext();
        ServicoBanco servBanco = new ServicoBanco();
        ServicoContaCorrente servCC = new ServicoContaCorrente();
        ServicoFormaPag servFormapag = new ServicoFormaPag();
        ServicoSetor servSetor = new ServicoSetor();
        ServicoSubDespesa servSubDesp = new ServicoSubDespesa();
        ServicoDespesa servDesp = new ServicoDespesa();
        ServicoFornecedor servFornec = new ServicoFornecedor();
        ServicoTipoDoc servTpdoc = new ServicoTipoDoc();

        ServicoContaPagar servico = new ServicoContaPagar();
        ServicoContaCorrente servcCc = new ServicoContaCorrente();

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
                           <a href='/ContaPagar'> Despesas </a>
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
                           <a href='/ContaPagar'> Despesas </a>
                         </li>
                         <li class='active'> Cadastro
                         </li>
                    </ol>";
        }

        // GET: ContaPagar
        public ActionResult Index(int tpfiltro = 1, string filtro = "", int pagina = 1, int tamPag = Constantes.TamanhoPagina, string dtini = "", string dtfim = "", string situacao = "T")
        {
            TempData["breadcrumb"] = CreateBreadCrumbIndex();
            TempData["nometela"] = "Contas a Pagar";

            var lista = PrepararViewModel(tpfiltro, filtro, pagina, tamPag, dtini, dtfim, situacao);

            return View(lista);

        }

        private IPagedList<ContaPagar> PrepararViewModel(int tpfiltro, string filtro, int pagina, int tamPag, string dtini, string dtfim, string situacao)
        {
            //PopularViewBag();
            var lista = new List<ContaPagar>();

            lista = servico.ObterListaObjetos(db, filtro, tpfiltro).ToList();

            ViewBag.Filtro = filtro;
            ViewBag.TamanhoPagina = tamPag;
            ViewBag.TpFiltro = tpfiltro;
            ViewBag.situacao = situacao;
            ViewBag.dtini = dtini;
            ViewBag.dtfim = dtfim;

            var lstCompl = new List<ContaPagar>();

            if (!string.IsNullOrEmpty(dtini) && !string.IsNullOrEmpty(dtfim))
            {

                var _dtini = DateTime.Now;
                var _dtfim = DateTime.Now.AddDays(30);
                try
                {
                    _dtini = Convert.ToDateTime(dtini);
                    _dtfim = Convert.ToDateTime(dtfim);

                    lstCompl.AddRange(lista.Where(c => c.DtVenc >= _dtini && c.DtVenc <= _dtfim));
                }
                catch (Exception ex)
                {
                    _dtini = DateTime.Now;
                    _dtfim = DateTime.Now.AddDays(30);
                    lstCompl.AddRange(lista.Where(c => c.DtVenc >= _dtini && c.DtVenc <= _dtfim));
                }
            }
            else
            {
                lstCompl.AddRange(lista);
            }

            lista.Clear();
            if (!string.IsNullOrEmpty(situacao) && situacao != "T")
            {
                lista.AddRange(lstCompl.Where(c => c.Estado == situacao).OrderByDescending(c => c.DtVenc));
            }
            else
            {
                lista.AddRange(lstCompl.OrderByDescending(c => c.DtVenc));
            }

            return lista.ToPagedList(pagina, tamPag);
        }

        [DireitoAcesso(Constantes.AC_INC_CAD_CP)]
        public ActionResult Create()
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Nova Conta Pagar";
            TempData["lboper"] = "Nova";

            var identity = User.Identity as ClaimsIdentity;
            var valorDaClaim = int.Parse(identity.Claims.FirstOrDefault(c => c.Type == "Id").Value);

            var model = CriarViewModelAddEdit();
            model.Id = -1;
            model.DtVenc = DateTime.Now.AddDays(30);
            model.Valor = 0;
            model.Desconto = 0;
            model.Juros = 0;
            model.Estado = "L";
            model.Total = 0;
            model.FlgConf = "N";
            model.IdUsuario = valorDaClaim;

            return View(model);
        }

        private CreateEditViewModel CriarViewModelAddEdit()
        {
            PopularViewBag();

            var model = new CreateEditViewModel();
            return model;
        }

        private void PopularViewBag()
        {
            ViewBag.SimNao = new SelectList(ListasGenericas.ObterSimNao, "Sigla", "Nome");
            ViewBag.Banco = new SelectList(servBanco.ObterListaObjetos(db, ""), "IdBanco", "Nome");
            ViewBag.SubDespesa = new SelectList(servDesp.ObterListaObjetos(db, ""), "Id", "Nome");
            ViewBag.TipoDoc = new SelectList(servTpdoc.ObterListaObjetos(db, ""), "Id", "Nome");
            ViewBag.FormaPag = new SelectList(servFormapag.ObterListaObjetos(db, ""), "Id", "Nome");
            ViewBag.Setor = new SelectList(servSetor.ObterListaObjetos(db, ""), "Id", "Nome");
            ViewBag.Fornecedor = new SelectList(servFornec.ObterListaObjetos(db, "", 1), "Id", "Nome");
            ViewBag.SituacaoAtivoSusp = new SelectList(ListasGenericas.SituacaoAtivoSusp, "Sigla", "Nome");
            ViewBag.Cc = new SelectList(servcCc.ObterListaObjetos(db, ""), "Id", "NomeAgencia");
        }

        [HttpPost]
        public ActionResult Create(CreateEditViewModel model)
        {

            if (!ModelState.IsValid)
            {
                TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
                TempData["nometela"] = "Nova Conta Pagar";
                TempData["lboper"] = "Nova";

                PopularViewBag();

                return View(model);
            }

            var objeto = new ContaPagar();
            ModelParaObjeto(model, objeto);

            try
            {

                servico.Salvar(db, objeto);
                TempData["sucesso"] = $@"Conta a Pagar salva com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar editar a Conta a Pagar {objeto.Historico}";
            }
            return RedirectToAction("Index");
        }

        private ContaPagar ModelParaObjeto(CreateEditViewModel model, ContaPagar objeto)
        {
            objeto.Id = model.Id;
            objeto.IdBanco = model.IdBanco;
            objeto.IdCc = model.IdCc;
            objeto.Desconto = model.Desconto;
            objeto.DtPag = model.DtPag;
            objeto.DtVenc = model.DtVenc;

            if (((model.Estado != "P") ||(string.IsNullOrEmpty(model.Estado)))&& model.DtPag != null)
            {
                objeto.Estado = "P";
            }
            else
                objeto.Estado = model.Estado;

            if (string.IsNullOrEmpty(objeto.Estado))
            {
                objeto.Estado = "L";
            }

            objeto.FlgConf = model.FlgConf;
            objeto.Historico = model.Historico.ToUpper();
            objeto.IdFornecedor = model.IdFornecedor;
            objeto.IdFormaPag = model.IdFormaPag;
            objeto.IdSetor = model.IdSetor;
            objeto.IdSubDesp = model.IdSubDesp;
            objeto.IdUsuario = model.IdUsuario;
            objeto.Juros = model.Juros;
            objeto.NumChq = model.NumCheque;
            objeto.Obs = model.Obs;
            objeto.DetalheCompra = model.DetCompra.ToUpper();

            return objeto;
        }

        [DireitoAcesso(Constantes.AC_EDIT_CAD_CP)]
        public ActionResult Edit(int id)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Editar Despesa";
            TempData["lboper"] = "Editar";

            var model = CriarViewModelAddEdit();
            var objeto = servico.ObterObjetoPorId(db, id);

            ObjetoParaModel(objeto, model);

            return View("Create", model);
        }

        private void ObjetoParaModel(ContaPagar objeto, CreateEditViewModel model)
        {
            model.Id = objeto.Id;
            try
            {
                model.Desconto = objeto.Desconto.Value;
            }
            catch (Exception ex)
            {
                model.Desconto = 0;
            }
            //     model.DetCompra = objeto.DetalheCompra;
            model.DtPag = objeto.DtPag;
            model.DtVenc = objeto.DtVenc;
            model.Estado = objeto.Estado;
            model.FlgConf = objeto.FlgConf;
            model.Historico = objeto.Historico;
            model.IdBanco = objeto.IdBanco;
            model.IdCc = objeto.IdCc;
            model.IdFornecedor = objeto.IdFornecedor;
            model.IdFormaPag = objeto.IdFormaPag;
            model.IdSetor = objeto.IdFormaPag;
            model.IdTipoDoc = objeto.IdTipoDoc;
            model.IdSubDesp = objeto.IdSubDesp;
            model.IdUsuario = objeto.IdUsuario;

            try
            {
                model.Juros = objeto.Juros.Value;
            }
            catch (Exception ex)
            {
                model.Juros = 0;
            }
            model.NumCheque = objeto.NumChq;
            model.NumDoc = objeto.NumDoc;
            model.Obs = objeto.Obs;
            model.DetCompra = objeto.DetalheCompra;

            try
            {
                model.Valor = objeto.Valor.Value;
            }
            catch (Exception ex)
            {
                model.Valor = 0;
            }
            model.Total = model.Valor + model.Juros - model.Desconto;
        }

        [HttpPost]
        public ActionResult Edit(CreateEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
                TempData["nometela"] = "Editar Despesa";
                TempData["lboper"] = "Editar";

                PopularViewBag();
                return View("Create", model);
            }
            var objeto = servico.ObterObjetoPorId(db, model.Id);

            ModelParaObjeto(model, objeto);

            try
            {
                servico.Salvar(db, objeto);
                TempData["sucesso"] = $@"Conta Pagar salvo com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar editar o Conta Pagar {objeto.Id}";
            }

            return RedirectToAction("Index");
        }

        [DireitoAcesso(Constantes.AC_APG_CAD_CP)]
        public ActionResult Delete(int id)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Apagar Conta a Pagar";

            var objeto = servico.ObterObjetoPorId(db, id);
            DeleteViewModel model = new DeleteViewModel();
            model.Id = objeto.Id;
            model.Nome = objeto.Historico;
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
                TempData["sucesso"] = $@"A conta a pagar {objeto.Historico} foi apagada com sucesso!";
            }
            catch
            {
                TempData["erro"] = $@"Erro ao tentar apagar a conta a pagar {objeto.Historico}";
            }
            return RedirectToAction("Index");
        }
    }
}