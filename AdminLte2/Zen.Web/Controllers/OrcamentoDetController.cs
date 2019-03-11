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
    public class OrcamentoDetController : CustomController
    {

        ZenContext db = new ZenContext();
        ServicoOrcamentoDet servico = new ServicoOrcamentoDet();
        ServicoOrcamento servOrc = new ServicoOrcamento();

        private string CreateBreadCrumbIndex()
        {
            return $@"<ol class='breadcrumb'>
                         <li>                            
                             <a href ='/'> Principal </a>
                         </li>
                         <li>      
                             <i class='fa fa-folder'> </i>
                             <a href ='/'> Orçamento </a>
                         </li>
                         <li>
                           <i class='fa-file-o'> </i>
                           <a href='/Orcamento'> Pedido </a>
                         </li>
                         <li class='active'>
                           <i class='fa-file-o'> </i>
                           <a href='/OrcamentoDet'> Item do Pedido </a>
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
                             <a href ='/'> Orçamento </a>
                         </li>
                         <li>
                           <i class='fa-file-o'> </i>
                           <a href='/Orcamento'> Pedidos </a>
                         </li>
                         <li>
                           <i class='fa-file-o'> </i>
                           <a href='/OrcamentoDet'> Itens do Pedido </a>
                         </li>
                         <li class='active'> Cadastro
                         </li>
                    </ol>";
        }


        // GET: OrcamentoDet
        public ActionResult Index(int pagina = 1, int tamPag = Constantes.TamanhoPagina, int idpedido = -1)
        {
            TempData["breadcrumb"] = CreateBreadCrumbIndex();
            TempData["nometela"] = "Item do Pedido de Orçamento";

            var lista = PrepararViewModel(pagina, tamPag,idpedido);

            return View(lista);
        }


        private IPagedList<OrcamentoDet> PrepararViewModel(int pagina, int tamPag, int idpedido)
        {
            //PopularViewBag();
            var lista = new List<OrcamentoDet>();
            var lstCompl = new List<OrcamentoDet>();

            lista = servico.ObterListaObjetos(db, idpedido).ToList();

            ViewBag.TamanhoPagina = tamPag;
            ViewBag.IdPedido = idpedido;

            return lista.ToPagedList(pagina, tamPag);            
        }

    }
}