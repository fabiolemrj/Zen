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
    public class MovCcController : CustomController
    {
        ZenContext db = new ZenContext();
        ServicoMovCc servico = new ServicoMovCc();

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
                           <a href='/MovCc'> Fluxo de Caixa </a>
                         </li>
                         
                    </ol>";
        
        }

        // GET: MovCc
        public ActionResult Index(int pagina = 1, int tamPag = Constantes.TamanhoPagina100, string dtMovIni = "", string dtMovFim = "", string sentido = "")
        {
            TempData["breadcrumb"] = CreateBreadCrumbIndex();
            TempData["nometela"] = "Contas a Pagar";

            var lista = PrepararViewModel( pagina, tamPag, dtMovIni, dtMovFim, sentido);

            return View(lista);

        }

        private IPagedList<MovCc> PrepararViewModel(int pagina, int tamPag, string dtMovIni, string dtMovFim,string sentido)
        {
            //PopularViewBag();
            var lst = new List<MovCc>();

            ViewBag.Sentido = sentido;
           
            ViewBag.TamanhoPagina = tamPag;
            ViewBag.dtMovIni = dtMovIni;
            ViewBag.dtMovFim = dtMovFim;

            var _dtini = DateTime.Now;
            var _dtfim = DateTime.Now.AddDays(30);

            if (!string.IsNullOrEmpty(dtMovIni) && !string.IsNullOrEmpty(dtMovFim))
            {               
                try
                {
                    _dtini = Convert.ToDateTime(dtMovIni);
                    _dtfim = Convert.ToDateTime(dtMovFim);                                      
                }
                catch (Exception ex)
                {
                    _dtini = DateTime.Now;
                    _dtfim = DateTime.Now.AddDays(30);
                }
            }
           

            lst = servico.ObterListaObjetos(db, _dtini,_dtfim,sentido).ToList();

            return lst.ToPagedList(pagina, tamPag);
        }
    }
}