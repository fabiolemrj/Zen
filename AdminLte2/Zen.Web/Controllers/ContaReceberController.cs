using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;
using Zen.Web.Models;
using Zen.Web.Servico;

namespace Zen.Web.Controllers
{
    public class ContaReceberController : Controller
    {
        ZenContext db = new ZenContext();
        ServicoContaReceber servico = new ServicoContaReceber();

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
    }
}