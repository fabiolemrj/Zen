﻿using System;
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
                           <a href='/Orcamento/Index?idpedido={idpedido}'> Pedido </a>
                         </li>
                        <li>
                         <i class='fa-file-o'> </i>
                        <a href='/OrcamentoDet/Edit?idpedido={idpedido}&item={item}'> Item do Pedido </a>
                        </li>
                         <li class='active'>
                           <i class='fa-file-o'> </i>
                            Item de Variação
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
                           <a href='/OrcamentoDet/Edit?idpedido={idpedido}&item={item}'> Itens do Pedido </a>
                         </li>
                         <li>
                           <i class='fa-file-o'> </i>
                           <a href='/OrcamentoVariacoes/Index?idpedido={idpedido}&item={item}'> Areas de impressão </a>
                         </li>
                         <li class='active'> Cadastro
                         </li>
                    </ol>";
        }



        // GET: OrcamentoVariacoes
        public ActionResult Index(int idpedido, int item)
        {
            TempData["breadcrumb"] = CreateBreadCrumbIndex(idpedido, item);
            TempData["nometela"] = "Variações do item do pedido";

            var viewmodel = PrepararViewModel(idpedido, item);

            return View(viewmodel);
        }


        private IndexViewModel PrepararViewModel(int idpedido, int item)
        {
            //PopularViewBag();
            var model = new IndexViewModel();

            model.LstOrcVariacoes = servico.ObterListaObjetos(db, idpedido, item).ToList();
            model.QuantFrente = model.LstOrcVariacoes.Count(c => c.Local == "F").ToString();
            model.QuantVerso = model.LstOrcVariacoes.Count(c => c.Local == "V").ToString();
            model.Idpedido = idpedido;
            model.Item = item;
            return model;
        }

        public ActionResult Create(int idpedido, int item)
        {
            TempData["nometela"] = "Novo Item de variação do pedido";
            TempData["lboper"] = "Novo";

            var model = CriarViewModelAddEdit(idpedido, item);
            model.Quant = 0;
            model.NrSeq = -1;

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CreateEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["breadcrumb"] = CreateBreadCrumbCreatEdit(model.IdPedido, model.Item);
                TempData["nometela"] = "Novo item de varaição do Pedido de Orçamento";
                TempData["lboper"] = "Novo";

                PopularViewBag(model.IdPedido, model.Item);

                return View(model);
            }

            var objeto = new OrcVariacao();
            ModelParaObjeto(model, objeto);

            try
            {
                servico.Salvar(db, objeto);
                TempData["sucesso"] = $@"Item de variação do Pedido de orçamento salvo com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar editar o item do pedido de orçamento {objeto.Item} ";
            }

            return RedirectToAction("Index", new { idpedido = model.IdPedido, item = model.Item });
        }

        public ActionResult Edit(int idpedido, int item, int nrseq)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit(idpedido, item);
            TempData["nometela"] = "Editar Item  de variação do pedido do orçamento";
            TempData["lboper"] = "Editar";

            //var lstobjetos = servico.ObterListaObjetos(db, idpedido, item);
            var model = CriarViewModelAddEdit(idpedido, item);
            var objeto = servico.ObterObjetoPorId(db, idpedido, item, nrseq);

            if (objeto == null)
            {
                return HttpNotFound();
            }

            ObjetoParaModel(model, objeto);

            return View("Create", model);
        }

        [HttpPost]
        public ActionResult Edit(CreateEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["breadcrumb"] = CreateBreadCrumbCreatEdit(model.IdPedido, model.Item);
                TempData["nometela"] = "Editar Item de variação do pedido";
                TempData["lboper"] = "Editar";
                PopularViewBag(model.IdPedido, model.Item);
                return View(model);
            }

            var objeto = servico.ObterObjetoPorId(db, model.IdPedido, model.Item, model.NrSeq);

            if (objeto == null)
            {
                return HttpNotFound();
            }

            ModelParaObjeto(model, objeto);

            try
            {

                servico.Salvar(db, objeto);
                TempData["sucesso"] = $@"O item de variação do pedido de orçamento salvo com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar editar a variação do item de pedido{objeto.IdPedido}";
            }

            return RedirectToAction("Index", new { idpedido = model.IdPedido, item = model.Item });

        }

        private CreateEditViewModel CriarViewModelAddEdit(int idpedido, int item)
        {
            PopularViewBag(idpedido, item);

            var model = new CreateEditViewModel();
            model.IdPedido = idpedido;
            model.Item = item;
            return model;
        }

        private void PopularViewBag(int idpedido, int item)
        {
            ViewBag.SimNao = new SelectList(ListasGenericas.ObterSimNaoCad, "Sigla", "Nome");
            ViewBag.LocalVariacao = new SelectList(ListasGenericas.LocalVariacao, "Sigla", "Nome");

        }

        private void ObjetoParaModel(CreateEditViewModel model, OrcVariacao objeto)
        {
            model.IdPedido = objeto.IdPedido;
            model.Item = objeto.Item;
            model.NrSeq = objeto.NrSeq;
            model.Quant = objeto.Quant;
            model.Variacao = objeto.Variacao;
            model.Local = objeto.Local;
        }

        private OrcVariacao ModelParaObjeto(CreateEditViewModel model, OrcVariacao objeto)
        {
            objeto.Item = model.Item;
            objeto.IdPedido = model.IdPedido;
            objeto.NrSeq = model.NrSeq;
            objeto.Local = model.Local;
            objeto.Variacao = model.Variacao;

            return objeto;
        }
    }
}