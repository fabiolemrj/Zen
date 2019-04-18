using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;
using Zen.Web.Models;
using Zen.Web.Servico;
using Zen.Web.Utils;
using Zen.Web.ViewModels.OrcamentoVariacoesViewModel;

namespace Zen.Web.Controllers
{
    public class OrcamentoVariacoesController : CustomController
    {
        ZenContext db = new ZenContext();
        ServicoOrcVariacao servico = new ServicoOrcVariacao();

        private string CreateBreadCrumbIndex(int idpedido, int item)
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
                           <i class='fa-file-o'> </i>
                           <a href='/OrcamentoDetEdit?idpedido={idpedido}&item={item}> Itens do Pedido</a>
                         </li>
                            <li class='active'>
                           <i class='fa-file-o'> </i>
                           <a href='/OrcamentoVariacoes'> Variações do Pedido </a>
                         </li>
                    </ol>";
        }

        private string CreateBreadCrumbCreatEdit(int idpedido, int item)
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
                           <a href='/Orcamento'> Pedidos </a>
                         </li>
                         <li>
                           <a href='/OrcamentoDet/Edit?idpedido={idpedido}&item={item}> Itens do Pedido' </a>
                         </li>
                         <li class='active'> Cadastro
                         </li>
                    </ol>";
        }

        // GET: OrcamentoVariacoes
        public ActionResult Index(int idpedido, int item)
        {
            TempData["breadcrumb"] = CreateBreadCrumbIndex(idpedido,item);
            TempData["nometela"] = "Item do Pedido de Orçamento";

            var viewmodel = PrepararViewModel(idpedido,item);

            return View(viewmodel);
        }


        private IndexViewModel PrepararViewModel(int idpedido, int item)
        {
            //PopularViewBag();
            var model = new IndexViewModel();
            
            model.LstOrcVariacoes = servico.ObterListaObjetos(db, idpedido , item).ToList();
            model.QuantFrente = model.LstOrcVariacoes.Count(c=>c.Local=="F").ToString();
            model.QuantVerso = model.LstOrcVariacoes.Count(c => c.Local == "V").ToString();
            model.Idpedido = idpedido;
            model.Item = item;
            return model;
        }

        public ActionResult Edit(int idpedido, int item)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit(idpedido,item);
            TempData["nometela"] = "Editar Item do pedido do orçamento";
            TempData["lboper"] = "Editar";

            var lstobjetos = servico.ObterListaObjetos(db, idpedido, item);
            return View();
        }

        private CreateEditViewModel CriarViewModelAddEdit(int idpedido, int item)
        {
            PopularViewBag(idpedido, item);

            var model = new CreateEditViewModel();
            return model;
        }

        private void PopularViewBag(int idpedido, int item)
        {
            ViewBag.SimNao = new SelectList(ListasGenericas.ObterSimNaoCad, "Sigla", "Nome");
    

        }
    }
}