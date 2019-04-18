using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using X.PagedList;
using Zen.Web.Models;
using Zen.Web.Servico;
using Zen.Web.Utils;
using Zen.Web.ViewModels.OrcamentoDetViewModel;

namespace Zen.Web.Controllers
{
    public class OrcamentoDetController : CustomController
    {

        ZenContext db = new ZenContext();
        ServicoOrcamentoDet servico = new ServicoOrcamentoDet();
        ServicoOrcamento servOrc = new ServicoOrcamento();
        ServicoMaterial servmat = new ServicoMaterial();
        ServicoProduto servProd = new ServicoProduto();
        ServicoOrcCalculo servicoOrcCalculo = new ServicoOrcCalculo();
        ServicoOrcVariacao servicoOrcVariacao = new ServicoOrcVariacao();

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

            var lista = PrepararViewModel(pagina, tamPag, idpedido);

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

        [DireitoAcesso(Constantes.AC_INC_CAD_PEDORC)]
        public ActionResult Create(int idpedido)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Novo Item do Pedido de Orçamento";
            TempData["lboper"] = "Novo";
            var item = -1;
            var model = CriarViewModelAddEdit(idpedido, item);
            model.IdPedido = idpedido;
            model.Item = item;
            model.AltA = 0;
            model.AltF = 0;
            model.AltHs = 0;
            model.AltRs = 0;
            model.CodFaca = "";
            model.Cola = "N";
            model.CompF = 0;
            model.ContraPlaca = "N";
            model.Cordao = "N";
            model.CorteEsp = "N";
            model.CorteSimples = "N";
            model.CorteVinco = "N";
            model.Dobra = "N";
            model.Elastico = "N";
            model.Encadernacao = "N";
            model.Espiral = "N";
            model.HotStamp = "N";
            model.HsChapa = "N";
            model.IdMaterial1 = 0;
            model.IdMaterial2 = 0;
            model.IdMaterial3 = 0;
            model.IdMaterial4 = 0;
            model.IdProduto = 0;
            model.Ilhos = 0;
            model.LamFoscaF = "N";
            model.LamFoscaV = "N";
            model.LargA = 0;
            model.LargF = 0;
            model.LargHs = 0;
            model.LargRs = 0;
            model.MeioCorte = "N";
            model.Montagem = "N";
            model.ObsAcab1 = "";
            model.ObsImp = "";
            model.OffF = 0;
            model.OffV = 0;
            model.OutrosAcab1 = "N";
            model.OutrosAcab2 = "N";
            model.OutrosImp = "N";
            model.Pintura = "N";
            model.Quant = 0;
            model.RelevoSeco = "N";
            model.RsChapa = "N";
            model.SemAcab = "N";
            model.Vazador = 0;
            model.Vinco = "N";
            model.WireO = "N";
            model.FotoPoli = "N";
            model.FotoPoliFornec = "N";
            model.FotoRet = "N";
            model.FotoTraco = "N";
            model.FotoRetFornec = "N";
            model.FotoTracoFornec = "N";
            model.Faca = "N";
            model.Quant = 0;
            model.Mat1Fornec = false;
            model.Mat2Fornec = false;
            model.Mat3Fornec = false;
            model.Mat4Fornec = false;
            ObjetoParaModel(model, new List<OrcVariacao> { });

            return View(model);
        }

        private void Validar(CreateEditViewModel model)
        {


            if ((model.LargF > model.LargA) || (model.AltF > model.AltA))
            {
                ModelState.AddModelError("1", "O formato fechado não pode ser maior que o formato aberto");
            }

            if (model.HsChapa == "S")
            {
                if (model.LargHs <= 0)
                    ModelState.AddModelError("2", "Informe a dimensão da chapa do hot stamp");

                if (model.AltHs <= 0)
                    ModelState.AddModelError("3", "Informe a dimensão da chapa do hot stamp");
            }

            if (model.OffSet == "S")
            {
                if (model.OffF <= 0)
                    ModelState.AddModelError("4", "Informe o valor  de frente de offset");
                if (model.OffV <= 0)
                    ModelState.AddModelError("5", "Informe o valor  de verso de offset");
            }

            if (model.IdMaterial1 <= 0)
            {
                ModelState.AddModelError("6", "O campo material 1 é obrigatório");
            }
        }

        [HttpPost]
        public ActionResult Create(CreateEditViewModel model)
        {
            Validar(model);
            if (!ModelState.IsValid)
            {
                TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
                TempData["nometela"] = "Novo Pedido de Orçamento";
                TempData["lboper"] = "Novo";

                PopularViewBag(model.IdPedido, model.Item);

                return View(model);
            }

            var objeto = new OrcamentoDet();
            ModelParaObjeto(model, objeto);

            var objetoCalc = new OrcCalculo();
            ModelParaObjeto(model, objetoCalc);

            var objetoVariacao = new OrcVariacao();
           

            try
            {

                servico.Salvar(db, objeto);
                objetoCalc.IdPedido = objeto.IdPedido;
                objetoCalc.Item = objeto.Item;
                servicoOrcCalculo.Salvar(db, objetoCalc);
                TempData["sucesso"] = $@"Item do Pedido de orçamento salvo com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar editar o item do pedido de orçamento {objeto.Item} ";
            }

            return RedirectToAction("Index", new { model.IdPedido });
            //return RedirectToAction("Index");
        }

        private OrcVariacao ModelParaObjeto(CreateEditViewModel model, OrcVariacao objeto)
        {
            objeto.Item = model.Item;
            objeto.IdPedido = model.IdPedido;
            //objeto.Quant = model.VariacaoFrente
            return objeto;
        }

        private OrcCalculo ModelParaObjeto(CreateEditViewModel model, OrcCalculo objeto)
        {
            objeto.TemCart = model.TemCartela;
            objeto.TemVar = model.TemVariacoes;
            objeto.Sangrada = model.Sangrada;
            objeto.Margem = model.Margem;
            objeto.QuantCart = model.QuantCart;
            objeto.Distancia = model.Distancia;
            objeto.FacaExt = model.FacaExt;
            objeto.Item = model.Item;
            objeto.IdPedido = model.IdPedido;
            return objeto;
        }
        private OrcamentoDet ModelParaObjeto(CreateEditViewModel model, OrcamentoDet objeto)
        {
            var mat1 = "";
            if (model.IdMaterial1 > 0)
            {
                objeto.Material1 = servmat.ObterObjetoPorId(db, model.IdMaterial1.Value);
                mat1 = objeto.Material1.Nome;
            }

            var mat2 = "";
            if (model.IdMaterial2 > 0)
            {
                objeto.Material2 = servmat.ObterObjetoPorId(db, model.IdMaterial2.Value);
                mat2 = objeto.Material2.Nome;
            }

            var mat3 = "";
            if (model.IdMaterial3 > 0)
            {
                objeto.Material3 = servmat.ObterObjetoPorId(db, model.IdMaterial3.Value);
                mat3 = objeto.Material3.Nome;
            }

            var mat4 = "";
            if (model.IdMaterial4 > 0)
            {
                objeto.Material4 = servmat.ObterObjetoPorId(db, model.IdMaterial4.Value);
                mat4 = objeto.Material4.Nome;
            }

            var prod = "";
            if (model.IdProduto > 0)
            {
                objeto.Produto = servProd.ObterObjetoPorId(db, model.IdProduto.Value);
                prod = objeto.Produto.Nome;
            }
            objeto.IdMaterial1 = model.IdMaterial1;
            objeto.IdMaterial2 = model.IdMaterial2;
            objeto.IdMaterial3 = model.IdMaterial3;
            objeto.IdMaterial4 = model.IdMaterial4;

            objeto.IdPedido = model.IdPedido;
            objeto.Item = model.Item;

            objeto.AltA = model.AltA;
            objeto.AltF = model.AltF;
            objeto.AltHs = model.AltHs;
            objeto.AltRs = model.AltRs;
            objeto.CodFaca = model.CodFaca;
            objeto.Cola = model.Cola;
            objeto.ContraPlaca = model.ContraPlaca;
            objeto.Cordao = model.Cordao;
            objeto.CorteEsp = model.CorteEsp;
            objeto.CorteSimples = model.CorteSimples;
            objeto.CorteVinco = model.CorteVinco;
            objeto.DescMaterial1 = mat1;
            objeto.DescMaterial2 = mat2;
            objeto.DescMaterial3 = mat3;
            objeto.DescMaterial4 = mat4;
            objeto.DescProduto = prod;
            objeto.IdProduto = model.IdProduto;
            objeto.Ilhos = model.Ilhos;
            objeto.ImpF = model.ImpF;
            objeto.ImpV = model.ImpV;
            objeto.LamFoscaF = model.LamFoscaF;
            objeto.LamFoscaV = model.LamFoscaV;
            objeto.LargA = model.LargA;
            objeto.LargF = model.LargF;
            objeto.CompF = model.CompF;
            objeto.LargHs = model.LargHs;
            objeto.LargRs = model.LargRs;
            objeto.MeioCorte = model.MeioCorte;
            objeto.Montagem = model.Montagem;
            objeto.ObsAcab1 = model.ObsAcab1;
            objeto.ObsImp = model.ObsImp;
            objeto.OffF = model.OffF;
            objeto.OffSet = model.OffSet;
            objeto.OffV = model.OffV;
            objeto.OutrosAcab1 = model.OutrosAcab1;
            objeto.OutrosAcab2 = model.OutrosAcab2;
            objeto.OutrosImp = model.OutrosImp;
            objeto.Pintura = model.Pintura;
            objeto.Quant = model.Quant;
            objeto.RelevoSeco = model.RelevoSeco;
            objeto.RsChapa = model.RsChapa;
            objeto.SemAcab = model.SemAcab;
            objeto.Vazador = model.Vazador;
            objeto.Vinco = model.Vinco;
            objeto.WireO = model.WireO;
            objeto.FotoPoli = model.FotoPoli;
            objeto.FotoPoliFornec = model.FotoPoliFornec;
            objeto.FotoRet = model.FotoRet;
            objeto.FotoRetFornec = model.FotoRetFornec;
            objeto.FotoTraco = model.FotoTracoFornec;
            objeto.Faca = model.Faca;
            objeto.Obs1 = model.Obs1;

            if (model.Mat1Fornec && objeto.IdMaterial1 > 0)
                objeto.Mat1Fornec = "S";
            else
                objeto.Mat1Fornec = "N";

            if (model.Mat2Fornec && objeto.IdMaterial2 > 0)
                objeto.Mat2Fornec = "S";
            else
                objeto.Mat2Fornec = "N";

            if (model.Mat3Fornec && objeto.IdMaterial3 > 0)
                objeto.Mat3Fornec = "S";
            else
                objeto.Mat3Fornec = "N";

            if (model.Mat4Fornec && objeto.IdMaterial4 > 0)
                objeto.Mat4Fornec = "S";
            else
                objeto.Mat4Fornec = "N";

            return objeto;
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
            ViewBag.ativo = new SelectList(ListasGenericas.ObterAtivo, "Sigla", "Nome");
            ViewBag.Produtos = new SelectList(servProd.ObterListaObjetos(db, ""), "ID", "NOME");

            var ltsitem = new List<SelectListItem>();

            ltsitem.Add(new SelectListItem() { Text = "", Value = "-1" });

            var mat = new SelectList(servmat.ObterListaObjetos(db, ""), "ID", "NOME");

            foreach (var it in mat)
            {
                ltsitem.Add(new SelectListItem() { Text = it.Text, Value = it.Value });
            }

            ViewBag.SitFotolito = new SelectList(ListasGenericas.ObterSitFotolito, "Sigla", "Nome");


            ViewBag.Material = ltsitem;

            ViewBag.IdPedido = idpedido;
            ViewBag.Item = item;
        }

        public ActionResult Edit(int idpedido, int item)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Editar Item do pedido do orçamento";
            TempData["lboper"] = "Editar";

            var model = CriarViewModelAddEdit(idpedido, item);

            var objeto = servico.ObterObjetoPorId(db, idpedido, item);
            if (objeto == null)
            {
                return HttpNotFound();
            }

            ObjetoParaModel(model, objeto);

            var objetoOrcCalc = servicoOrcCalculo.ObterObjetoPorId(db, idpedido, item);
            if (objetoOrcCalc == null)
            {
                objetoOrcCalc = new OrcCalculo();
            }
            else
            {
                ObjetoParaModel(model, objetoOrcCalc);
            }

             
            var lstVariacao = servicoOrcVariacao.ObterListaObjetos(db,idpedido,item).ToList();

            ObjetoParaModel(model, lstVariacao);


            return View("Create", model);
        }

        private void ObjetoParaModel(CreateEditViewModel model, List<OrcVariacao> lstobjeto)
        {
            model.VariacaoFrente = 0;
            model.VariacaoVerso = 0;
            foreach (var item in lstobjeto)
            {
                if (item.Item <= 0)
                    model.Item = item.Item;

                if (item.IdPedido <= 0)
                    model.IdPedido = item.IdPedido;

                if (item.Local == "F")
                {
                    model.VariacaoFrente++;
                }
                else if(item.Local == "V")
                {
                    model.VariacaoVerso++;
                }
            }
            
        }

        private void ObjetoParaModel(CreateEditViewModel model, OrcCalculo objeto)
        {
            model.IdPedido = objeto.IdPedido;
            model.Item = objeto.Item;
            model.TemCartela = objeto.TemCart;
            model.TemVariacoes = objeto.TemVar;
            model.Sangrada = objeto.Sangrada;
            model.Margem = objeto.Margem;
            model.QuantCart = objeto.QuantCart;
            model.Distancia = objeto.Distancia;
            model.FacaExt = objeto.FacaExt;

        }
        private void ObjetoParaModel(CreateEditViewModel model, OrcamentoDet objeto)
        {
            model.IdMaterial1 = objeto.IdMaterial1;
            model.IdMaterial2 = objeto.IdMaterial2;
            model.IdMaterial3 = objeto.IdMaterial3;
            model.IdMaterial4 = objeto.IdMaterial4;
            model.IdProduto = objeto.IdProduto;

            model.IdPedido = objeto.IdPedido;
            model.Item = objeto.Item;

            model.AltA = objeto.AltA;
            model.AltF = objeto.AltF;
            model.AltHs = objeto.AltHs;
            model.AltRs = objeto.AltRs;
            model.CodFaca = objeto.CodFaca;
            model.Cola = objeto.Cola;
            model.ContraPlaca = objeto.ContraPlaca;
            model.Cordao = objeto.Cordao;
            model.CorteEsp = objeto.CorteEsp;
            model.CorteSimples = objeto.CorteSimples;
            model.CompF = objeto.CompF;
            model.CorteVinco = objeto.CorteVinco;
            model.IdProduto = objeto.IdProduto;
            model.Ilhos = objeto.Ilhos;
            model.ImpF = objeto.ImpF;
            model.ImpV = objeto.ImpV;
            model.LamFoscaF = objeto.LamFoscaF;
            model.LamFoscaV = objeto.LamFoscaV;
            model.LargA = objeto.LargA;
            model.LargF = objeto.LargF;
            model.LargHs = objeto.LargHs;
            model.LargRs = objeto.LargRs;
            model.MeioCorte = objeto.MeioCorte;
            model.Montagem = objeto.Montagem;
            model.ObsAcab1 = objeto.ObsAcab1;
            model.ObsImp = objeto.ObsImp;
            model.OffF = objeto.OffF;
            model.OffSet = objeto.OffSet;
            model.OffV = objeto.OffV;
            model.OutrosAcab1 = objeto.OutrosAcab1;
            model.OutrosAcab2 = objeto.OutrosAcab2;
            model.OutrosImp = objeto.OutrosImp;
            model.Pintura = objeto.Pintura;
            model.Quant = objeto.Quant;
            model.RelevoSeco = objeto.RelevoSeco;
            model.RsChapa = objeto.RsChapa;
            model.SemAcab = objeto.SemAcab;
            model.Vazador = objeto.Vazador;
            model.Vinco = objeto.Vinco;
            model.WireO = objeto.WireO;
            model.FotoPoli = objeto.FotoPoli;
            model.FotoPoliFornec = objeto.FotoPoliFornec;
            model.FotoRet = objeto.FotoRet;
            model.FotoRetFornec = objeto.FotoRetFornec;
            model.FotoTraco = objeto.FotoTracoFornec;
            model.Faca = objeto.Faca;
            model.Obs1 = objeto.Obs1;

            if (objeto.Mat1Fornec == "S")
                model.Mat1Fornec = true;
            else
                model.Mat1Fornec = false;

            if (objeto.Mat2Fornec == "S")
                model.Mat2Fornec = true;
            else
                model.Mat2Fornec = false;

            if (objeto.Mat3Fornec == "S")
                model.Mat3Fornec = true;
            else
                model.Mat3Fornec = false;

            if (objeto.Mat4Fornec == "S")
                model.Mat4Fornec = true;
            else
                model.Mat4Fornec = false;

        }

        [HttpPost]
        public ActionResult Edit(CreateEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
                TempData["nometela"] = "Editar Item do pedido do orçamento";
                TempData["lboper"] = "Editar";

                PopularViewBag(model.IdPedido, model.Item);

                return View("Create", model);
            }
            var objeto = servico.ObterObjetoPorId(db, model.IdPedido, model.Item);

            var objetoOrcCalc = servicoOrcCalculo.ObterObjetoPorId(db, model.IdPedido, model.Item);

            if (objetoOrcCalc == null)
            {
                objetoOrcCalc = new OrcCalculo();
            }

            ModelParaObjeto(model, objetoOrcCalc);

            if (objeto == null)
            {
                return HttpNotFound();
            }

            ModelParaObjeto(model, objeto);

            try
            {
                servicoOrcCalculo.Salvar(db, objetoOrcCalc);
                servico.Salvar(db, objeto);
                TempData["sucesso"] = $@"Item do pedido de orçamento salvo com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar editar o cliente {objeto.IdPedido}";
            }

            return RedirectToAction("Index", new { model.IdPedido });
            //return RedirectToAction("Index");
        }

    }
}