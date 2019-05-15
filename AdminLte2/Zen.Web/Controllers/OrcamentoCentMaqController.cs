using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zen.Web.Models;
using Zen.Web.Servico;
using Zen.Web.Utils;
using Zen.Web.ViewModels.OrcamentoCentMaqViewModel;

namespace Zen.Web.Controllers
{
    public class OrcamentoCentMaqController : CustomController
    {
        ZenContext db = new ZenContext();
        ServicoOrcCentMaq servico = new ServicoOrcCentMaq();

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
                           <a href=#> Entrada Maquina</a>
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
                           <a href='/OrcamentoCentMaq/Index?idpedido={idpedido}&item={item}'> Entrada de maquina </a>
                         </li>
                         <li class='active'> Cadastro
                         </li>
                    </ol>";
        }

        // GET: OrcamentoCentMaq
        public ActionResult Index(int idpedido, int item)
        {
            TempData["breadcrumb"] = CreateBreadCrumbIndex(idpedido, item);
            TempData["nometela"] = "Entrada de maquina";

            var viewmodel = PrepararViewModel(idpedido, item);

            return View(viewmodel);
        }

        private IndexViewModel PrepararViewModel(int idpedido, int item)
        {
            //PopularViewBag();
            var model = new IndexViewModel();

            model.LstOrcCentMaq = servico.ObterListaObjetos(db, idpedido, item).ToList();
            model.Idpedido = idpedido;
            model.Item = item;
            return model;
        }

        public ActionResult Create(int idpedido, int item)
        {
            TempData["nometela"] = "Nova Entrada de Maquinas";
            TempData["lboper"] = "Nova";
            var _valFotoPadrao = new ValPadraoEntMaq();
            var model = CriarViewModelAddEdit(idpedido, item);
            PreencherValoresPadroes(idpedido, item, _valFotoPadrao);
            model.Quant = _valFotoPadrao.NumEnt;

            model.NrSeq = -1;
            model.QuantImpDia = 1;
            
            model.FmtImp = 1;
            model.Fixo = "S";
            model.Valor = CalcValorItem(idpedido,item);
            model.Total = model.Valor.Value * model.Quant.Value;
            return View(model);
        }

        private double CalcValorItem(int idpedido,int item)
        {
            var servConfig = new ServicoConfig();
            var config = servConfig.ObterObjetoPorId(db, 1);
            var result = 0.0;
            var nrseq = servico.ObterListaObjetos(db, idpedido, item).Max(c => c.NrSeq)+1;
            switch (nrseq)
            {
                case 1: result = config.VrEntrMaqP.Value;
                    break;
                case 2: result = config.VrEntrMaqM.Value;
                    break;
                case 3: result = config.VrEntrMaqG.Value;
                    break;                
            }

            return result;
        }

        [HttpPost]
        public ActionResult Create(CreateEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                PopularViewBag(model.IdPedido, model.Item);
                TempData["nometela"] = "Nova Entrada de maquinas";
                TempData["lboper"] = "Nova";
                var _valFotoPadrao = new ValFotoPadrao();

                return View(model);
            }

            var objeto = new OrcCentMaq();

            ModelParaObjeto(model, objeto);

            try
            {
                servico.Salvar(db, objeto);
                TempData["sucesso"] = $@"Item de entrada de maquinas salvo com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar editar o item de entrada de maquinas ";
            }

            return RedirectToAction("Index", new { idpedido = model.IdPedido, item = model.Item });
        }


        private void PopularViewBag(int idpedido, int item)
        {
            ViewBag.SimNao = new SelectList(ListasGenericas.ObterSimNaoCad, "Sigla", "Nome");
        }

        private CreateEditViewModel CriarViewModelAddEdit(int idpedido, int item)
        {
            PopularViewBag(idpedido, item);

            var model = new CreateEditViewModel();
            model.IdPedido = idpedido;
            model.Item = item;

            return model;
        }

        private OrcCentMaq ModelParaObjeto(CreateEditViewModel model, OrcCentMaq objeto)
        {
            objeto.Item = model.Item;
            objeto.IdPedido = model.IdPedido;
            objeto.NrSeq = model.NrSeq;
            objeto.Quant = model.Quant;
            objeto.FmtImp = model.FmtImp;
            objeto.Valor = model.Valor;
            objeto.QuantImpDia = model.QuantImpDia;
            objeto.Fixo = model.Fixo;
            return objeto;
        }

        private void ObjetoParaModel(CreateEditViewModel model, OrcCentMaq objeto)
        {
            model.IdPedido = objeto.IdPedido;
            model.Item = objeto.Item;
            model.NrSeq = objeto.NrSeq;
            model.Quant = objeto.Quant;
            model.FmtImp = objeto.FmtImp;
            model.QuantImpDia = objeto.QuantImpDia;
            model.Valor = objeto.Valor;
            model.Total = model.Valor.Value * model.Quant.Value;
        }

        private void PreencherValoresPadroes(int idpedido, int item, ValPadraoEntMaq _valFotoPadrao)
        {
            var servOrcDet = new ServicoOrcamentoDet();
            var servOrcCalc = new ServicoOrcCalculo();
            var servVar = new ServicoOrcVariacao();

            var orcDet = servOrcDet.ObterObjetoPorId(db, idpedido, item);
            var orccalc = servOrcCalc.ObterObjetoPorId(db, idpedido, item);
            var lstorcVar = servVar.ObterListaObjetos(db, idpedido, item);

            _valFotoPadrao.Larg = orcDet.LargA.Value;
            _valFotoPadrao.Alt = orcDet.AltA.Value;
            var cores = 0;

            try
            {
                cores = orcDet.ImpF.Value + orcDet.ImpV.Value;
            }
            catch
            {
                cores = 0;
            }

            _valFotoPadrao.NumEnt = cores;
            _valFotoPadrao.Tipo = 1;
            if (orccalc.TemCart != "S" && orccalc.TemVar != "S")
            {
                _valFotoPadrao.NumCart = cores;
            }
            else if (orccalc.TemCart != "S" && orccalc.TemVar == "S")
            {
                if (lstorcVar.Count() == 0)
                {

                }
                _valFotoPadrao.NumCart = lstorcVar.Count() + cores - 1;
            }
            else
            {
                if (orccalc.TemVar != "S")
                    _valFotoPadrao.NumEnt = 1;
                else
                    _valFotoPadrao.NumEnt = lstorcVar.Count();

                if (_valFotoPadrao.NumEnt == 0)
                {

                }

                _valFotoPadrao.NumEnt += cores - 1;
            }
        }

        private CreateEditViewModel CriarViewModelAddEdit(int idpedido, int item, ValPadraoEntMaq _valFotoPadrao)
        {
            PopularViewBag(idpedido, item);

            var model = new CreateEditViewModel();
            model.IdPedido = idpedido;
            model.Item = item;
            
            return model;
        }

        public ActionResult Edit(int idpedido, int item, int nrseq)
        {
            TempData["nometela"] = "Editar Entrada de Maquinas";
            TempData["lboper"] = "Editar";
            var _valFotoPadrao = new ValPadraoEntMaq();
            var model = CriarViewModelAddEdit(idpedido, item, _valFotoPadrao);

            var objeto = servico.ObterObjetoPorId(db, idpedido, item, nrseq);

            if (objeto == null)
            {
                return HttpNotFound();
            }
            ObjetoParaModel(model, objeto);

            return View("Create",model);
        }

        [HttpPost]
        public ActionResult Edit(CreateEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                PopularViewBag(model.IdPedido, model.Item);
                TempData["nometela"] = "Editar Entrada de Maquinas";
                TempData["lboper"] = "Editar";
                var _valFotoPadrao = new ValPadraoEntMaq();

                return View("Create",model);
            }

            var objeto = servico.ObterObjetoPorId(db, model.IdPedido, model.Item, model.NrSeq);

            try
            {
                servico.Salvar(db, objeto);
                TempData["sucesso"] = $@"O item de entrada de maquina salvo com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar editar o item de entrada de maquina";
            }

            return RedirectToAction("Index", new { idpedido = model.IdPedido, item = model.Item });
        }

        public ActionResult Delete(int idpedido, int item, int nrseq)
        {
            TempData["erro"] = $@"Não é possível apagar item de maquina!";
            var objeto = servico.ObterObjetoPorId(db, idpedido, item, nrseq);
            return RedirectToAction("Index", new { idpedido = idpedido, item = item });            
        }

        

    }
}