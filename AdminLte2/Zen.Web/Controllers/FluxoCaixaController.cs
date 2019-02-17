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
    public class FluxoCaixaController : CustomController
    {
        ZenContext db = new ZenContext();
        ServicoFluxoCaixa servico = new ServicoFluxoCaixa();

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
                           <a href='/FluxoCaixa'> Fluxo de Caixa </a>
                         </li>
                         
                    </ol>";

        }
        // GET: FluxoCaixa
        public ActionResult Index(int pagina = 1, int tamPag = Constantes.TamanhoPagina100, string dtini = "", string dtfim = "", string sentido = "T", 
            string historico = "", string fornecCli = "", string formapag = "")
        {
            TempData["breadcrumb"] = CreateBreadCrumbIndex();
            TempData["nometela"] = "Fluxo de Caixa";

            var lista = PrepararViewModel(pagina, tamPag, dtini, dtfim, sentido,historico,fornecCli,formapag);

            return View(lista);

        }

        private IPagedList<FluxoCaixa> PrepararViewModel(int pagina, int tamPag, string dtini, string dtfim,
            string sentido, string historico,string fornecCli, string formapag)
        {
            //PopularViewBag();
            var lst = new List<FluxoCaixa>();

            ViewBag.Sentido = sentido;

            ViewBag.TamanhoPagina = tamPag;
       

            var _dtini = DateTime.Now;
            var _dtfim = DateTime.Now.AddDays(30);

            if (!string.IsNullOrEmpty(dtini) && !string.IsNullOrEmpty(dtfim))
            {
                try
                {
                    _dtini = Convert.ToDateTime(dtini);
                    _dtfim = Convert.ToDateTime(dtfim);
                }
                catch (Exception ex)
                {
                    _dtini = DateTime.Now;
                    _dtfim = DateTime.Now.AddDays(30);
                }
            }
            ViewBag.dtini = dtini;
            ViewBag.dtfim = dtfim;

            lst = servico.ObterListaObjetos(db, _dtini, _dtfim, sentido,historico,fornecCli,formapag).OrderByDescending(c=>c.DtVenc).ToList();

            var desp = 0.0;
            var cred = 0.0;
            foreach(var item in lst)
            {
                if (item.Sentido == "D")
                    desp += item.Valor;
                else if (item.Sentido == "C")
                    cred += item.Valor;
            }

            ViewBag.saldocr = string.Format("{0:c}", cred);
            ViewBag.saldocp = string.Format("{0:c}", desp); 
            ViewBag.saldotot = string.Format("{0:c}", (cred - desp));
            return lst.ToPagedList(pagina, tamPag);
        }
    }
}