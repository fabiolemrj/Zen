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
        public ActionResult Index(int tpfiltro = 1, string filtro = "", int pagina = 1, int tamPag = Constantes.TamanhoPagina, string dtini="", string dtfim="")
        {
            TempData["breadcrumb"] = CreateBreadCrumbIndex();
            TempData["nometela"] = "Contas a Receber";


            var lista = PrepararViewModel(tpfiltro, filtro, pagina, tamPag);

            return View(lista);

        }

        private IPagedList<ContasReceber> PrepararViewModel(int tpfiltro, string filtro, int pagina, int tamPag)
        {
            //PopularViewBag();
            var lista = new List<ContasReceber>();

            lista = servico.ObterListaObjetos(db, filtro, tpfiltro).ToList();

            ViewBag.Filtro = filtro;
            ViewBag.TamanhoPagina = tamPag;
            ViewBag.TpFiltro = tpfiltro;
            return lista.ToPagedList(pagina, tamPag);
        }
    }
}