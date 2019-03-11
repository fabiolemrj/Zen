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
using Zen.Web.ViewModels.Orcamento;

namespace Zen.Web.Controllers
{
    public class OrcamentoController : CustomController
    {
        ZenContext db = new ZenContext();
        ServicoOrcamento servico = new ServicoOrcamento();
        ServicoCliente servCli = new ServicoCliente();

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
                         <li class='active'>
                           <i class='fa-file-o'> </i>
                           <a href='/Orcamento'> Pedidos </a>
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
                         <li class='active'> Cadastro
                         </li>
                    </ol>";
        }


        // GET: Orcamento
        [DireitoAcesso(Constantes.AC_CONS_CAD_PEDORC)]
        public ActionResult Index(int pagina = 1, int tamPag = Constantes.TamanhoPagina, string dtini = "", string dtfim = "",
                                 string aprovados = "T", string cliente = "", string referencia = "", string orcamento = "", string pendentes = "T")
        {
            TempData["breadcrumb"] = CreateBreadCrumbIndex();
            TempData["nometela"] = "Pedido de Orçamento";

            var lista = PrepararViewModel(pagina, tamPag, dtini, dtfim, aprovados, cliente, referencia, orcamento, pendentes);

            return View(lista);

        }

        
        private IPagedList<Orcamento> PrepararViewModel(int pagina, int tamPag, string dtini, string dtfim,
                                                       string aprovados, string cliente, string referencia, string orcamento,
                                                       string pendentes = "")
        {
            //PopularViewBag();
            var lista = new List<Orcamento>();
            var lstCompl = new List<Orcamento>();

            var _dtini = DateTime.Now;
            var _dtfim = DateTime.Now.AddDays(30);

            try
            {
                if (!string.IsNullOrEmpty(dtini))
                {
                    _dtini = Convert.ToDateTime(dtini);
                }
            }
            catch
            {
                _dtini = DateTime.Now;
            }

            try
            {
                if (!string.IsNullOrEmpty(dtfim))
                {
                    _dtfim = Convert.ToDateTime(dtfim);
                }
            }
            catch
            {
                _dtfim = DateTime.Now.AddDays(30);
            }

            lista = servico.ObterListaObjetosPedidos(db, _dtini, _dtfim).ToList();

            if (!string.IsNullOrEmpty(referencia))
            {
                lstCompl.AddRange(lista.Where(c => c.NmReferencia.Contains(referencia)));
            }
            else
            {
                lstCompl.AddRange(lista);
            }

            lista.Clear();
            if (!string.IsNullOrEmpty(cliente))
            {
                lista.AddRange(lstCompl.Where(c => c.NomeCliente.Contains(cliente)));
            }
            else
            {
                lista.AddRange(lstCompl);
            }

            lstCompl.Clear();
            if (!string.IsNullOrEmpty(orcamento))
            {
                lstCompl.AddRange(lista.Where(c => c.IdOrcamento.ToString() == orcamento));
            }
            else
            {
                lstCompl.AddRange(lista);
            }

            lista.Clear();
            if (aprovados == "A")
            {
                lista.AddRange(lstCompl.Where(c => c.ItensAprov > 0));
            }
            else if (aprovados == "N")
            {
                lista.AddRange(lstCompl.Where(c => c.ItensAprov == 0 || c.ItensAprov == null));
            }
            else
            {
                lista.AddRange(lstCompl);
            }

            lstCompl.Clear();
            if (!string.IsNullOrEmpty(pendentes) && pendentes != "T")
            {

                lstCompl.AddRange(lista.Where(c => c.Pendente == pendentes));
            }
            else
            {
                lstCompl.AddRange(lista);
            }

            ViewBag.referencia = referencia;
            ViewBag.TamanhoPagina = tamPag;
            ViewBag.cliente = cliente;
            ViewBag.aprovados = aprovados;
            ViewBag.dtini = _dtini;
            ViewBag.dtfim = _dtfim;
            ViewBag.orcamento = orcamento;
            ViewBag.pendentes = pendentes;

            return lstCompl.ToPagedList(pagina, tamPag);
        }

        [DireitoAcesso(Constantes.AC_INC_CAD_PEDORC)]
        public ActionResult Create()
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Nova Pedido de Orçamento";
            TempData["lboper"] = "Nova";

            var identity = User.Identity as ClaimsIdentity;
            var valorDaClaim = int.Parse(identity.Claims.FirstOrDefault(c => c.Type == "Id").Value);

            var model = CriarViewModelAddEdit();
            model.IdPedido = -1;
            model.DtPedido = DateTime.Now;
            model.DtAtual = DateTime.Now;
            model.Tel1 = "(21)";
            model.Tel2 = "(21)";
            model.Celular = "(21)";
            model.Fax = "(21)";
            model.IdCliente = 0;
            model.IdReferencia = 0;
            model.IdUsuAtu = valorDaClaim;
            model.ItensEnv = 0;
            model.Comissao = 0;
            model.Incompleto = "N";
            model.NotifCel = "N";
            model.NotifEmail = "N";
            model.NotifFax = "N";
            model.NotiFOutro = "N";
            model.NotifTel = "N";
            model.IdOrcamento = 0;
            model.ItensPend = 0;
            model.Pendente = "S";
            model.ItensEnv = 0;
            model.ItensAprov = 0;
            model.SinalPerc = 0; ;
            model.IdUsuario = valorDaClaim;

            model.Cliente = new Cliente();
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
            ViewBag.SimNao = new SelectList(ListasGenericas.ObterSimNaoCad, "Sigla", "Nome");
            ViewBag.aprovados = new SelectList(ListasGenericas.ObterSitAprov, "Sigla", "Nome");
            ViewBag.ativo = new SelectList(ListasGenericas.ObterAtivo, "Sigla", "Nome");
            ViewBag.cliente = new SelectList(servCli.ObterListaObjetosIniciandoNomePor(db, ""), "IdCliente", "Nome");
        }

        [HttpPost]
        public ActionResult Create(CreateEditViewModel model)
        {

            if (!ModelState.IsValid)
            {
                TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
                TempData["nometela"] = "Novo Pedido de Orçamento";
                TempData["lboper"] = "Novo";

                PopularViewBag();

                return View(model);
            }

            var objeto = new Orcamento();
            ModelParaObjeto(model, objeto);

            try
            {

                servico.Salvar(db, objeto);
                TempData["sucesso"] = $@"Pedido de orçamento salvo com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar editar o pedido de orçamento {objeto.IdPedido}";
            }
            return RedirectToAction("Index");
        }

        private Orcamento ModelParaObjeto(CreateEditViewModel model, Orcamento objeto)
        {
            var cliente = servCli.ObterListaObjetosPorNome(db, model.NomeCliente).FirstOrDefault();
            if(cliente != null)
            {
                objeto.IdCliente = cliente.IdCliente;
            }
            else
            {
                objeto.IdCliente = model.IdCliente;
            }
            objeto.IdOrcamento = model.IdOrcamento;
            objeto.IdPedido = model.IdPedido;
            objeto.IdReferencia = model.IdReferencia;
            objeto.Contato = model.Contato;
            objeto.Comissao = model.Comissao;
            

            if (model.DtAtual == null)
            {
                objeto.DtAtual = model.DtPedido;
            }
            else
            {
                objeto.DtAtual = model.DtAtual;
            }

            objeto.DtPedido = model.DtPedido;

            objeto.IdUsuario = model.IdUsuario;
            objeto.IdUsuAtu = model.IdUsuAtu;
            objeto.Incompleto = model.Incompleto;
            objeto.Indicacao = model.Indicacao;
            objeto.ItensAprov = model.ItensAprov;
            objeto.ItensEnv = model.ItensEnv;
            objeto.ItensPend = model.ItensPend;
            objeto.NmReferencia = model.NmReferencia;
            objeto.NomeCliente = model.NomeCliente;
            objeto.NotifCel = model.NotifCel;
            objeto.NotifEmail = model.NotifEmail;
            objeto.NotifFax = model.NotifFax;
            objeto.NotiFOutro = model.NotiFOutro;
            objeto.NotifTel = model.NotifTel;
            objeto.Obs = model.Obs;
            objeto.Pendente = model.Pendente;
            objeto.Prazo = model.Prazo;
            objeto.Ramal1 = model.Ramal1;
            objeto.Ramal2 = model.Ramal2;
            objeto.RamalF = model.RamalF;
            objeto.SinalPerc = model.SinalPerc;
            objeto.Tel1 = model.Tel1;
            objeto.Tel2 = model.Tel2;
            objeto.Urgente = model.Urgente;
            objeto.Email1 = model.Email1;
            objeto.Email2 = model.Email2;
            objeto.Celular = model.Celular;
            objeto.Pendente = model.Pendente;
            objeto.ItensPend = model.ItensPend;

            return objeto;
        }

        private CreateEditViewModel ObjetoParaModel(CreateEditViewModel model, Orcamento objeto)
        {
            model.Celular = objeto.Celular;
            model.Cliente = objeto.Cliente;
            model.Comissao = objeto.Comissao;
            model.Contato = objeto.Contato;
            model.DtAtual = objeto.DtAtual;
            model.DtPedido = objeto.DtPedido;
            model.Email1 = objeto.Email1;
            model.Email2 = objeto.Email2;
            model.Fax = objeto.Fax;
            model.FormaPag = objeto.FormaPag;
            model.IdCliente = objeto.IdCliente;
            model.IdOrcamento = objeto.IdOrcamento;
            model.IdPedido = objeto.IdPedido;
            model.IdReferencia = objeto.IdReferencia;
            model.IdUsuario = objeto.IdUsuario;
            model.IdUsuAtu = objeto.IdUsuAtu;
            model.Incompleto = objeto.Incompleto;
            model.Indicacao = objeto.Indicacao;
            model.ItensAprov = objeto.ItensAprov;
            model.ItensEnv = objeto.ItensEnv;
            model.ItensPend = objeto.ItensPend;
            model.NmReferencia = objeto.NmReferencia;
            model.NomeCliente = objeto.NomeCliente;
            model.NotifCel = objeto.NotifCel;
            model.NotifEmail = objeto.NotifEmail;
            model.NotifFax = objeto.NotifFax;
            model.NotiFOutro = objeto.NotiFOutro;
            model.NotifTel = objeto.NotifTel;
            model.Obs = objeto.Obs;
            model.Pendente = objeto.Pendente;
            model.Prazo = objeto.Prazo;
            model.Ramal1 = objeto.Ramal1;
            model.Ramal2 = objeto.Ramal2;
            model.RamalF = objeto.RamalF;
            model.SinalPerc = objeto.SinalPerc;
            model.Tel1 = objeto.Tel1;
            model.Tel2 = objeto.Tel2;
            model.Urgente = objeto.Urgente;

            return model;
        }

        [DireitoAcesso(Constantes.AC_EDIT_CAD_PEDORC)]
        public ActionResult Edit(int id)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Editar Pedido de Orçamento";
            TempData["lboper"] = "Editar";

            var model = CriarViewModelAddEdit();
            var objeto = servico.ObterObjetoPorId(db, id);

            ObjetoParaModel(model,objeto );

            return View("Create", model);
        }

        [HttpPost]
        public ActionResult Edit(CreateEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
                TempData["nometela"] = "Editar Pedido de Orçamento";
                TempData["lboper"] = "Editar";

                PopularViewBag();
                return View("Create", model);
            }
            var objeto = servico.ObterObjetoPorId(db, model.IdPedido);

            ModelParaObjeto(model, objeto);

            try
            {
                servico.Salvar(db, objeto);
                TempData["sucesso"] = $@"Pedido de Orçamento salvo com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar editar o pedido de orçamento {objeto.IdPedido}";
            }

            return RedirectToAction("Index");
        }

        [DireitoAcesso(Constantes.AC_APG_CAD_PEDORC)]
        public ActionResult Delete(int id)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Apagar Pedido de Orçamento";

            var objeto = servico.ObterObjetoPorId(db, id);
            DeleteViewModel model = new DeleteViewModel();
            model.Id = objeto.IdPedido;
            model.Nome = objeto.IdPedido.ToString();
            return View(model);
        }

        public JsonResult BuscaCliente(string Prefix)
        {
            var lstcliente = servCli.ObterListaObjetosIniciandoNomePor(db, Prefix);

            var lista = new List<object>();
            foreach (var item in lstcliente)
            {
                lista.Add(new { Id = item.IdCliente, Nome = item.Nome });
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(DeleteViewModel model)
        {
            var objeto = servico.ObterObjetoPorId(db, model.Id);
            try
            {
                if (objeto != null)
                {
                    servico.Delete(db, objeto);

                }
                TempData["sucesso"] = $@"O Pedido de orçamento {objeto.IdPedido} foi apagado com sucesso!";
            }
            catch
            {
                TempData["erro"] = $@"Erro ao tentar apagar o pedido do orçamento {objeto.IdPedido}";
            }
            return RedirectToAction("Index");
        }
    }
}