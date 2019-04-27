using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zen.Web.Models;
using Zen.Web.Servico;
using Zen.Web.Utils;
using Zen.Web.ViewModels.OrcamentoFotolitoViewModel;

namespace Zen.Web.Controllers
{
    public class OrcamentoFotolitoController : CustomController
    {

        ZenContext db = new ZenContext();
        ServicoOrcFotolitos servico = new ServicoOrcFotolitos();

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
                           <a href=#> Fotolito</a>
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
                           <a href='/OrcamentoFotolito/Index?idpedido={idpedido}&item={item}'> Fotolito </a>
                         </li>
                         <li class='active'> Cadastro
                         </li>
                    </ol>";
        }

        // GET: OrcamentoFotolito
        public ActionResult Index(int idpedido, int item)
        {
            TempData["breadcrumb"] = CreateBreadCrumbIndex(idpedido, item);
            TempData["nometela"] = "Fotolito";
           
            var viewmodel = PrepararViewModel(idpedido, item);


            return View(viewmodel);
        }

        public ActionResult Edit(int idpedido, int item, int nrseq)
        {
            TempData["nometela"] = "Editar Fotolito";
            TempData["lboper"] = "Editar";
            var _valFotoPadrao = new ValFotoPadrao();
            var model = CriarViewModelAddEdit(idpedido, item, _valFotoPadrao);

            var objeto = servico.ObterObjetoPorId(db, idpedido, item, nrseq);

            if (objeto == null)
            {
                return HttpNotFound();
            }
            ObjetoParaModel(model, objeto);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CreateEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                PopularViewBag(model.IdPedido, model.Item);
                TempData["nometela"] = "Editar Fotolito";
                TempData["lboper"] = "Editar";
                var _valFotoPadrao = new ValFotoPadrao();
                
                return View(model);
            }

            var objeto = servico.ObterObjetoPorId(db, model.IdPedido, model.Item, model.NrSeq);

            try
            {
                servico.Salvar(db, objeto);
                TempData["sucesso"] = $@"O item do fotolito salvo com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar editar o item da area de impressão do fotolito";
            }

            return RedirectToAction("Index", new { idpedido = model.IdPedido, item = model.Item });
        }

        private OrcFotolitos ModelParaObjeto(CreateEditViewModel model, OrcFotolitos objeto)
        {
            objeto.Item = model.Item;
            objeto.IdPedido = model.IdPedido;
            objeto.NrSeq = model.NrSeq;
            objeto.Quant = model.Quant;
            objeto.Alt = model.Altura;
            objeto.Larg = model.Largura;
            objeto.Valor = model.Valor;
            objeto.Fornec = model.Fornecedor;
            objeto.Fixo = model.Fixo;
            return objeto;
        }

        private void ObjetoParaModel(CreateEditViewModel model, OrcFotolitos objeto)
        {
            model.IdPedido = objeto.IdPedido;
            model.Item = objeto.Item;
            model.NrSeq = objeto.NrSeq;
            model.Quant = objeto.Quant;
            model.Altura = objeto.Alt;
            model.Largura = objeto.Larg;
            model.Valor = objeto.Valor;
            model.Total = model.Valor.Value * model.Quant.Value;
        }

        private IndexViewModel PrepararViewModel(int idpedido, int item)
        {
            //PopularViewBag();
            var model = new IndexViewModel();
           
            model.LstOrcFotolito = servico.ObterListaObjetos(db, idpedido, item).ToList();
            model.Idpedido = idpedido;
            model.Item = item;
            return model;
        }

        public ActionResult Create(int idpedido, int item)
        {
            TempData["nometela"] = "Novo Item de Fotolito";
            TempData["lboper"] = "Novo";
            var _valFotoPadrao = new ValFotoPadrao();
            var model = CriarViewModelAddEdit(idpedido,item, _valFotoPadrao);
            model.Quant = 0;
            model.Largura = 0;
            model.Altura = 0;
            model.NrSeq = -1;
          
            PreencherValoresPadroes(idpedido, item, _valFotoPadrao, model);
            model.Fornecedor = "N";
            model.Fixo = "S";
            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult Create(CreateEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                PopularViewBag(model.IdPedido, model.Item);
                TempData["nometela"] = "Novo Fotolito";
                TempData["lboper"] = "Novo";
                var _valFotoPadrao = new ValFotoPadrao();

                return View("Edit", model);
            }

            var objeto = new OrcFotolitos();

            ModelParaObjeto(model, objeto);

            try
            {
                servico.Salvar(db, objeto);
                TempData["sucesso"] = $@"Item de Fotolito salvo com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar editar o item de Fotolito ";
            }

            return RedirectToAction("Index", new { idpedido = model.IdPedido, item = model.Item });
        }


        private void PreencherValoresPadroes(int idpedido, int item, ValFotoPadrao _valFotoPadrao, CreateEditViewModel objeto)
        {
            var servOrcCalc = new ServicoOrcCalculo();
            var servOrcDet = new ServicoOrcamentoDet();

            var orccalc = servOrcCalc.ObterObjetoPorId(db, idpedido, item);
            var orcdet = servOrcDet.ObterObjetoPorId(db, idpedido, item);
                                
            if (orccalc.TemCart == "N" && orccalc.TemVar == "S")
            {
                _valFotoPadrao.NumCart = 0;
                _valFotoPadrao.Marg = 1;
                _valFotoPadrao.Dist = 0;
            }
            else if (orccalc.TemCart == "S" && orccalc.TemVar == "N")
            {
                _valFotoPadrao.NumCart = orccalc.QuantCart.Value;
                _valFotoPadrao.Marg = 1;
                _valFotoPadrao.Dist = orccalc.Distancia.Value;
            }
            else if (orccalc.TemCart == "S" && orccalc.TemVar == "S")
            {
                _valFotoPadrao.NumCart = orccalc.QuantCart.Value;
                _valFotoPadrao.Marg = 1;
                _valFotoPadrao.Dist = orccalc.Distancia.Value;
            }
            else
            {
                _valFotoPadrao.NumCart = 0;
                _valFotoPadrao.Marg = 0;
                _valFotoPadrao.Dist = 0;
            }

            _valFotoPadrao.NumCores = orcdet.ImpF.Value + orcdet.ImpV.Value;
            
            objeto.IdPedido = idpedido;
            objeto.Item = item;
            objeto.Fornecedor = "N";
            objeto.Fixo = "S";
            var l1 = _valFotoPadrao.Larg;
            var a1 = _valFotoPadrao.Alt;
            if (a1 < l1)
            {
                objeto.Largura = a1;
                objeto.Altura = l1;
            }
            else
            {
                objeto.Largura = l1;
                objeto.Altura = a1;
            }
            objeto.Valor = CustoFotolito(objeto.Largura.Value - 2.0, objeto.Altura.Value - 2.0);

        }

        private CreateEditViewModel CriarViewModelAddEdit(int idpedido, int item, ValFotoPadrao _valFotoPadrao)
        {
            PopularViewBag(idpedido, item);             
            
            var model = new CreateEditViewModel();
            model.IdPedido = idpedido;
            model.Item = item;

            //model.LstFotolito = servico.ObterListaObjetos(db, idpedido, item).ToList();
        
            return model;
        }

        private void PopularViewBag(int idpedido, int item)
        {
            ViewBag.SimNao = new SelectList(ListasGenericas.ObterSimNaoCad, "Sigla", "Nome");
        }

        private double CustoFotolito(double larg, double alt)
        {
            var result = 0.0;

            var servConfig = new ServicoConfig();

            var config = servConfig.ObterObjetoPorId(db, 1);

            var menor = 0.0;
            var maior = 0.0;

            if(larg < alt)
            {
                menor = larg;
                maior = alt;
            }
            else
            {
                menor = alt;
                maior = larg;
            }

            if(menor <= 15.0 && maior <= 21.0)
            {
                result = config.VrFoto1.Value;
            }
            else if (menor <= 21.0 && maior <= 29.7)
            {
                result = config.VrFoto2.Value;
            }
            else if (menor <= 29.7 && maior <= 42)
            {
                result = config.VrFoto3.Value;
            }
            else
            {
                result = 0;
            }

            if(result != 0 && result < config.VrMinFoto)
            {
                result = config.VrMinFoto.Value;
            }

            return result;
        }

        private int FormatoFoto(OrcFotolitos objeto)
        {
            var maior = 0.0;
            var menor = 0.0;
            
            if(objeto.Larg < objeto.Alt)
            {
                menor = objeto.Larg.Value;
                maior = objeto.Alt.Value;
            }
            else
            {
                menor = objeto.Alt.Value;
                maior = objeto.Larg.Value;
            }

            var result = 0;
            if (menor <= 15 + 2 && maior <= 21 + 2)
            {
                result = 1;
            }
            else if (menor <= 21 + 2 && maior <= 29.7 + 2)
            {
                result = 2;
            }
            else if (menor <= 29.7 + 2 && maior <= 42 + 2)
            {
                result = 3;
            }
            else
            {
                result = 4;
            }

                return result;
        }
    }
}