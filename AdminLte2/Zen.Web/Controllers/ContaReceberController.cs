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
using Zen.Web.ViewModels.ContaReceberViewModel;

namespace Zen.Web.Controllers
{
    public class ContaReceberController : CustomController
    {
        ZenContext db = new ZenContext();
        ServicoContaReceber servico = new ServicoContaReceber();
        ServicoBanco servBanco = new ServicoBanco();
        ServicoTipoReceita servTpRec = new ServicoTipoReceita();
        ServicoTipoDoc servTpDoc = new ServicoTipoDoc();
        ServicoFormaPag servFormaPag = new ServicoFormaPag();
        ServicoSetor servSetor = new ServicoSetor();

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
                           <a href='/ContaReceber'> Contas Receber </a>
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
                           <a href='/ContaReceber'> Contas Receber </a>
                         </li>
                         <li class='active'> Cadastro
                         </li>
                    </ol>";
        }


        // GET: ContaReceber
        public ActionResult Index(int tpfiltro = 1, string filtro = "", int pagina = 1, int tamPag = Constantes.TamanhoPagina, string dtini="", string dtfim="", string situacao = "T")
        {
            TempData["breadcrumb"] = CreateBreadCrumbIndex();
            TempData["nometela"] = "Contas a Receber";


            var lista = PrepararViewModel(tpfiltro, filtro, pagina, tamPag,dtini,dtfim,situacao);

            return View(lista);

        }

        private IPagedList<ContasReceber> PrepararViewModel(int tpfiltro, string filtro, int pagina, int tamPag, string dtini, string dtfim, string situacao)
        {
            //PopularViewBag();
            var lista = new List<ContasReceber>();

            lista = servico.ObterListaObjetos(db, filtro, tpfiltro).ToList();

            ViewBag.Filtro = filtro;
            ViewBag.TamanhoPagina = tamPag;
            ViewBag.TpFiltro = tpfiltro;
            ViewBag.situacao = situacao;
            ViewBag.dtini = dtini;
            ViewBag.dtfim = dtfim;

            var lstCompl = new List<ContasReceber>();

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
                lista.AddRange(lstCompl.OrderByDescending(c=>c.DtVenc));
            }

            return lista.ToPagedList(pagina, tamPag);
        }

        [DireitoAcesso(Constantes.AC_CONS_CAD_CR)]
        public ActionResult Create()
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Nova Conta Receber";
            TempData["lboper"] = "Nova";

            var identity = User.Identity as ClaimsIdentity;
            var valorDaClaim = int.Parse(identity.Claims.FirstOrDefault(c => c.Type == "Id").Value);

            var model = CriarViewModelAddEdit();
            model.Id = -1;
            model.DtVenc = DateTime.Now;
            model.Valor = 0;
            model.Desconto = 0;
            model.Juros = 0;
            model.Estado = "L";
            model.FlgConf = "N";
            model.IdUsuario = valorDaClaim;

            return View(model);
        }

        private CreateEditViewModel CriarViewModelAddEdit()
        {
            ViewBag.SimNao = new SelectList(ListasGenericas.ObterSimNao, "Sigla", "Nome");
            ViewBag.Banco = new SelectList(servBanco.ObterListaObjetos(db, ""), "IdBanco", "Nome");
            ViewBag.TipoRec = new SelectList(servTpRec.ObterListaObjetos(db,""),"Id","Nome");
            ViewBag.TipoDoc = new SelectList(servTpDoc.ObterListaObjetos(db, ""), "Id", "Nome");
            ViewBag.FormaPag = new SelectList(servFormaPag.ObterListaObjetos(db,""),"Id","Nome");
            ViewBag.Setor = new SelectList(servSetor.ObterListaObjetos(db,""),"Id","Nome");

            var model = new CreateEditViewModel();
            return model;
        }

        [HttpPost]
        public ActionResult Create(CreateEditViewModel model)
        {            

            if (!ModelState.IsValid)
            {
                TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
                TempData["nometela"] = "Nova Conta Receber";
                TempData["lboper"] = "Nova";

                ViewBag.SimNao = new SelectList(ListasGenericas.ObterSimNao, "Sigla", "Nome");
                ViewBag.Banco = new SelectList(servBanco.ObterListaObjetos(db, ""), "IdBanco", "Nome");
                ViewBag.TipoRec = new SelectList(servTpRec.ObterListaObjetos(db, ""), "Id", "Nome");
                ViewBag.TipoDoc = new SelectList(servTpDoc.ObterListaObjetos(db, ""),"Id","Nome");
                ViewBag.FormaPag = new SelectList(servFormaPag.ObterListaObjetos(db, ""), "Id", "Nome");
                ViewBag.Setor = new SelectList(servSetor.ObterListaObjetos(db, ""), "Id", "Nome");

                return View(model);
            }

            var objeto = new ContasReceber();
            ModelParaObjeto(model, objeto);
            
            try
            {
                servico.Salvar(db, objeto);
                TempData["sucesso"] = $@"Conta a Receber salva com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar editar a Conta Receber {objeto.Historico}";
            }
            return RedirectToAction("Index");
        }

        private ContasReceber ModelParaObjeto(CreateEditViewModel model, ContasReceber objeto)
        {
            objeto.Id = model.Id;
            objeto.IdBancoCheque = model.IdBancoCheque;
            objeto.IdCc = model.IdCc;
            objeto.Desconto = model.Desconto;
            objeto.DtPag = model.DtPag;
            objeto.DtPagDesc = model.DtPagDesc;
            objeto.DtVenc = model.DtVenc;
            objeto.Estado = model.Estado;
            objeto.FlgConf = model.FlgConf;           
            objeto.Historico = model.Historico;
            objeto.IdCheque = model.IdCheque;
            objeto.IdCliente = model.IdCliente;
            objeto.IdFormaPag = model.IdFormaPag;
            objeto.IdSetor = model.IdSetor;
            objeto.IdTipoReceita = model.IdTipoReceita;
            objeto.IdUsuario = model.IdUsuario;
            objeto.Juros = model.Juros;
            objeto.NumAgChq = model.NumAgChq;
            objeto.NumChq = model.NumChq;
            objeto.NumOsi = model.NumOsi;
            objeto.NumParc = model.NumParc;
            objeto.Obs = model.Obs;
            objeto.PercTitDesc = model.PercTitDesc;
            
            return objeto;
        }

    }
}