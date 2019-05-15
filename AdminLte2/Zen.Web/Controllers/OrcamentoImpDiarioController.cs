using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zen.Web.Models;
using Zen.Web.Servico;
using Zen.Web.Utils;
using Zen.Web.ViewModels.OrcamentoImpDiarioViewModel;

namespace Zen.Web.Controllers
{
    public class OrcamentoImpDiarioController : CustomController
    {
        ZenContext db = new ZenContext();
        ServicoOrcImpDiaria servico = new ServicoOrcImpDiaria();
        ServicoConfig servConfig = new ServicoConfig();

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
                           <a href=#> Impressão diária</a>
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
                           <a href='/OrcamentoCentMaq/Index?idpedido={idpedido}&item={item}'> Impressão diária </a>
                         </li>
                         <li class='active'> Cadastro
                         </li>
                    </ol>";
        }

        // GET: OrcamentoImpDiario
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

            model.LstOrcImpDiaria = servico.ObterListaObjetos(db, idpedido, item).ToList();
            model.Idpedido = idpedido;
            model.Item = item;
            return model;
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
        }

        public ActionResult Create(int idpedido, int item)
        {
            TempData["nometela"] = "Nova Impressão Diária";
            TempData["lboper"] = "Nova";
            var _valFotoPadrao = new ValPadraoImpDiaria();
            var model = CriarViewModelAddEdit(idpedido, item);
            PreencherValoresPadroes(idpedido, item, _valFotoPadrao);
            model.Quant = _valFotoPadrao.NumImp;

            model.NrSeq = -1;
            model.QuantImpDia = 1000;

            model.TpImpDia = 1;
            model.Fixo = "S";
            model.ValorDia = CalcValorDia(idpedido, item);
            model.Valor = CalcValor(model.Quant.Value, model.QuantImpDia.Value, model.ValorDia.Value);
            model.Total = model.Valor.Value * model.Quant.Value;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CreateEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                PopularViewBag(model.IdPedido, model.Item);
                TempData["nometela"] = "Nova Impressão Diária";
                TempData["lboper"] = "Nova";
                var _valFotoPadrao = new ValPadraoImpDiaria();

                return View(model);
            }

            var objeto = new OrcImpDiaria();

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

        private OrcImpDiaria ModelParaObjeto(CreateEditViewModel model, OrcImpDiaria objeto)
        {
            objeto.Item = model.Item;
            objeto.IdPedido = model.IdPedido;
            objeto.NrSeq = model.NrSeq;
            objeto.Quant = model.Quant;
            objeto.QuantImpDia = model.QuantImpDia;
            objeto.Valor = model.Valor;
            objeto.TpImpDia = model.TpImpDia;
            objeto.QuantImpDia = model.QuantImpDia;
            objeto.Fixo = model.Fixo;
            objeto.ValorDia = model.ValorDia;
            return objeto;
        }

        private void PreencherValoresPadroes(int idpedido, int item, ValPadraoImpDiaria _valFotoPadrao)
        {
            var servOrcDet = new ServicoOrcamentoDet();
            var servOrcCalc = new ServicoOrcCalculo();
            var servVar = new ServicoOrcVariacao();
            var servCart = new ServicoOrcCartela();

            var orcDet = servOrcDet.ObterObjetoPorId(db, idpedido, item);
            var orccalc = servOrcCalc.ObterObjetoPorId(db, idpedido, item);
            var lstorcVar = servVar.ObterListaObjetos(db, idpedido, item);
            var lstorcCart = servCart.ObterListaObjetos(db, idpedido, item);

            _valFotoPadrao.Larg = orcDet.LargA.Value;
            _valFotoPadrao.Alt = orcDet.AltA.Value;

            var quant = orcDet.Quant.Value;
            var impF = orcDet.ImpF.Value;
            var impV = orcDet.ImpV.Value;

            _valFotoPadrao.Tipo = 1;
            var tpimp = 0;
            var frente = false;
            var verso = false;

            if (orccalc.TemCart != "S")
            {
                if (orccalc.TemVar != "S")
                {
                    _valFotoPadrao.NumImp = quant * (impF + impV);
                }
                else
                {
                    foreach (var itmvar in lstorcVar)
                    {
                        if (itmvar.Local == "F")
                        {
                            _valFotoPadrao.NumImp += itmvar.Quant.Value * impF;
                            tpimp += 100;
                            frente = true;
                        }
                        else
                        {
                            _valFotoPadrao.NumImp += itmvar.Quant.Value * impV;
                            tpimp += 1;
                            verso = true;
                        }
                    }

                    if (frente)
                    {
                        _valFotoPadrao.NumImp += quant * impF;
                    }

                    if (verso)
                    {
                        _valFotoPadrao.NumImp = quant * impV;
                    }
                }
            }
            else
            {
                if (orccalc.TemVar != "S")
                {
                    _valFotoPadrao.NumCart = orccalc.QuantCart.Value;
                    if (_valFotoPadrao.NumCart == 0)
                    {
                        _valFotoPadrao.NumCart = 1;
                    }

                }
                else
                {
                    _valFotoPadrao.NumCart = orccalc.QuantCart.Value;
                    if (_valFotoPadrao.NumCart == 0)
                    {
                        _valFotoPadrao.NumCart = 1;
                    }
                    foreach (var cart in lstorcCart)
                    {
                        if (cart.Local == "F")
                        {
                            _valFotoPadrao.NumImp += cart.QuantImp.Value * impF;
                            tpimp += 100;
                            frente = true;
                        }
                        else
                        {
                            _valFotoPadrao.NumCart += cart.QuantImp.Value * impV;
                            tpimp += 1;
                            verso = true;
                        }
                    }

                    if (frente)
                    {
                        _valFotoPadrao.NumImp += ((quant - _valFotoPadrao.NumCart - 1) / _valFotoPadrao.NumCart) * impF;
                    }

                    if (verso)
                    {
                        _valFotoPadrao.NumImp += ((quant - _valFotoPadrao.NumCart - 1) / _valFotoPadrao.NumCart) * impV;
                    }
                }
            }
        }

        private double CalcValorDia(int idpedido, int item)
        {

            var config = servConfig.ObterObjetoPorId(db, 1);
            var result = 0.0;
            var nrseq = servico.ObterListaObjetos(db, idpedido, item).Max(c => c.NrSeq) + 1;
            switch (nrseq)
            {
                case 1:
                    result = config.VrImpDia1.Value;
                    break;
                case 2:
                    result = config.VrImpDia2.Value;
                    break;
                case 3:
                    result = config.VrImpDia3.Value;
                    break;
            }

            return result;
        }

        private double CalcValor(int quant, int quantDia, double valordia)
        {
            var result = 0.0;
            result = (valordia / quantDia) * quant;

            var config = servConfig.ObterObjetoPorId(db, 1);

            var vrmin = config.VrImpDiaMin;
            if (result < vrmin && quant > 0 && quantDia > 0 && valordia > 0)
            {
                result = vrmin.Value;
            }

            return result;
        }
    }
}