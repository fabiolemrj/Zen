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
using Zen.Web.ViewModels.ContaPagarFixaViewModel;


namespace Zen.Web.Controllers
{
    public class ContaPagarFixaController : CustomController
    {
        ZenContext db = new ZenContext();
        ServicoBanco servBanco = new ServicoBanco();
        ServicoContaCorrente servCC = new ServicoContaCorrente();
        ServicoFormaPag servFormapag = new ServicoFormaPag();
        ServicoSetor servSetor = new ServicoSetor();
        ServicoSubDespesa servSubDesp = new ServicoSubDespesa();
        ServicoDespesa servDesp = new ServicoDespesa();
        ServicoFornecedor servFornec = new ServicoFornecedor();
        ServicoTipoDoc servTpdoc = new ServicoTipoDoc();

        ServicoContaPagarFixa servico = new ServicoContaPagarFixa();

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
                           <a href='/ContaPagarFixa'> Despesas Fixas </a>
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
                           <a href='/ContaPagarFixa'> Despesas Fixas </a>
                         </li>
                         <li class='active'> Cadastro
                         </li>
                    </ol>";
        }


        // GET: ContaPagarFixa
        public ActionResult Index(int tpfiltro = 1, string filtro = "", int pagina = 1, int tamPag = Constantes.TamanhoPagina)
        {
            TempData["breadcrumb"] = CreateBreadCrumbIndex();
            TempData["nometela"] = "Contas a Pagar Fixas";

            var lista = PrepararViewModel(tpfiltro, filtro, pagina, tamPag);

            return View(lista);

        }

        private IPagedList<ContaPagarFixa> PrepararViewModel(int tpfiltro, string filtro, int pagina, int tamPag)
        {
            //PopularViewBag();
            var lista = new List<ContaPagarFixa>();

            lista = servico.ObterListaObjetos(db, filtro).ToList();

            ViewBag.Filtro = filtro;
            ViewBag.TamanhoPagina = tamPag;
            ViewBag.TpFiltro = tpfiltro;
                       

            return lista.ToPagedList(pagina, tamPag);
        }

        [DireitoAcesso(Constantes.AC_INC_CAD_CP)]
        public ActionResult Create()
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Nova Despesa Fixa";
            TempData["lboper"] = "Nova";

            var identity = User.Identity as ClaimsIdentity;
            var valorDaClaim = int.Parse(identity.Claims.FirstOrDefault(c => c.Type == "Id").Value);

            var model = CriarViewModelAddEdit();
            model.IdSubDesp = -1;
            model.IdDesp = -1;
            model.Valor = 0;
            model.Estado = "A";
            model.IdUsuario = valorDaClaim;
            model.Periodicidade = "M";
            
            return View(model);
        }

        private CreateEditViewModel CriarViewModelAddEdit()
        {
            PopularViewBag();

            var model = new CreateEditViewModel();
            return model;
        }

        private void PopularViewBag()
        {
            ViewBag.SimNao = new SelectList(ListasGenericas.ObterSimNao, "Sigla", "Nome");
            ViewBag.Banco = new SelectList(servBanco.ObterListaObjetos(db, ""), "IdBanco", "Nome");
            ViewBag.SubDespesa = new SelectList(servSubDesp.ObterListaObjetos(db, ""), "IdSubDesp", "Nome");
            ViewBag.TipoDoc = new SelectList(servTpdoc.ObterListaObjetos(db, ""), "Id", "Nome");
            ViewBag.FormaPag = new SelectList(servFormapag.ObterListaObjetos(db, ""), "Id", "Nome");
            ViewBag.Setor = new SelectList(servSetor.ObterListaObjetos(db, ""), "Id", "Nome");
            ViewBag.Fornecedor = new SelectList(servFornec.ObterListaObjetos(db, "", 1), "Id", "Nome");
            ViewBag.SituacaoAtivoSusp = new SelectList(ListasGenericas.SituacaoAtivoSusp, "Sigla", "Nome");
            ViewBag.Periodicidade = new SelectList(ListasGenericas.ListaPeriodicidade, "Sigla", "Nome"); 
        }

        [HttpPost]
        public ActionResult Create(CreateEditViewModel model)
        {

            if (!ModelState.IsValid)
            {
                TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
                TempData["nometela"] = "Nova Despesa Fixa";
                TempData["lboper"] = "Nova";

                PopularViewBag();

                return View(model);
            }

            var objeto = new ContaPagarFixa();
            ModelParaObjeto(model, objeto);

            try
            {
                if(objeto.NumParc > 0)
                {
                    objeto.NumParcAtu = objeto.NumParcAtu + 1;
                }
                servico.Salvar(db, objeto);
                TempData["sucesso"] = $@"Conta a Pagar Fixa salva com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar criar a Conta a Pagar Fixa {objeto.Historico}";
            }
            return RedirectToAction("Index");
        }

        private ContaPagarFixa ModelParaObjeto(CreateEditViewModel model, ContaPagarFixa objeto)
        {
            objeto.IdSubDesp = model.IdSubDesp;           
            objeto.IdDesp = model.IdDesp;
            objeto.Dia = model.Dia;
            objeto.DtUltLanc = model.DtUltLanc;
            objeto.NumParc = model.NumParc;
            objeto.Estado = model.Estado;
            objeto.NumParcAtu = model.NuParcAtu;
            objeto.Historico = model.Historico;
            objeto.IdFornecedor = model.IdFornecedor;
            objeto.IdFormaPag = model.IdFormaPag;
            objeto.IdSetor = model.IdSetor;
            objeto.IdSubDesp = model.IdSubDesp;
            objeto.IdUsuario = model.IdUsuario;
            objeto.Periodicidade = model.Periodicidade;
            objeto.Valor = model.Valor;
            objeto.IdCc = model.IdCc;
            objeto.DtPrimLanc = model.DtPrimLanc;            

            return objeto;
        }

        [DireitoAcesso(Constantes.AC_EDIT_CAD_CP)]
        public ActionResult Edit(int iddesp, int idSubdesp)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Editar Despesa Fixa";
            TempData["lboper"] = "Editar";

            var model = CriarViewModelAddEdit();
            var objeto = servico.ObterObjetoPorId(db, iddesp,idSubdesp);

            ObjetoParaModel(objeto, model);

            return View("Create", model);
        }

        [HttpPost]
        public ActionResult Edit(CreateEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
                TempData["nometela"] = "Editar Despesa Fixa";
                TempData["lboper"] = "Editar";

                PopularViewBag();
                return View("Create", model);
            }
            var objeto = servico.ObterObjetoPorId(db, model.IdDesp,model.IdSubDesp);

            ModelParaObjeto(model, objeto);

            try
            {
                servico.Salvar(db, objeto);
                TempData["sucesso"] = $@"Conta Pagar fixa salva com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar editar o Conta Pagar fixa {objeto.Historico}";
            }

            return RedirectToAction("Index");
        }

        private void ObjetoParaModel(ContaPagarFixa objeto, CreateEditViewModel model)
        {
            model.IdSubDesp = objeto.IdSubDesp;
            model.IdDesp = objeto.IdDesp;
            
            model.DtPrimLanc = objeto.DtPrimLanc;
            model.DtUltLanc = objeto.DtUltLanc;
            model.Estado = objeto.Estado;
            model.NumParc = objeto.NumParc;
            model.Historico = objeto.Historico;
            model.NuParcAtu = objeto.NumParcAtu;
            model.IdCc = objeto.IdCc;
            model.IdFornecedor = objeto.IdFornecedor;
            model.IdFormaPag = objeto.IdFormaPag;
            model.IdSetor = objeto.IdFormaPag;
            model.IdTipoDoc = objeto.IdTipoDoc;
            model.IdSubDesp = objeto.IdSubDesp;
            model.IdUsuario = objeto.IdUsuario;
                     
            model.Periodicidade = objeto.Periodicidade;
            model.Dia = objeto.Dia;
            
            try
            {
                model.Valor = objeto.Valor.Value;
            }
            catch (Exception ex)
            {
                model.Valor = 0;
            }            
        }

        public string DescrPeriodicidade(string period)
        {
            var result = "";

            if (period == "M")
                result = "mensal";
            else if (period == "B")
                result = "Bimestral";
            else if (period == "T")
                result = "Trimestral";
            else if (period == "Q")
                result = "Quadrimestral";
            else if (period == "S")
                result = "Semestral";
            else if (period == "A")
                result = "Anual";
            else
                result = "";

            return result;
        }

        [DireitoAcesso(Constantes.AC_APG_CAD_CP)]
        public ActionResult Delete(int iddesp, int idSubdesp)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Apagar Conta a Pagar Fixa";

            var objeto = servico.ObterObjetoPorId(db, iddesp,idSubdesp);
            DeleteViewModel model = new DeleteViewModel();
            model.IdDesp = objeto.IdDesp;
            model.IdSubDesp = objeto.IdSubDesp;
            model.Nome = objeto.Historico;
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(DeleteViewModel model)
        {
            var objeto = servico.ObterObjetoPorId(db, model.IdDesp,model.IdSubDesp);
            try
            {
                if (objeto != null)
                {
                    servico.Delete(db, objeto);
                }
                TempData["sucesso"] = $@"A conta a pagar fixa {objeto.Historico} foi apagada com sucesso!";
            }
            catch
            {
                TempData["erro"] = $@"Erro ao tentar apagar a conta a pagar fixa {objeto.Historico}";
            }
            return RedirectToAction("Index");
        }

    }
}