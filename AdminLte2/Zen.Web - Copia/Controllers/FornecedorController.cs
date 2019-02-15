using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;
using Zen.Web.Models;
using Zen.Web.Servico;
using Zen.Web.Utils;
using Zen.Web.ViewModels.FornecedorViewModel;

namespace Zen.Web.Controllers
{
    public class FornecedorController : CustomController
    {

        ZenContext db = new ZenContext();
        ServicoFornecedor servico = new ServicoFornecedor();

        private string CreateBreadCrumbIndex()
        {
            return $@"<ol class='breadcrumb'>
                         <li>                            
                             <a href ='/'> Principal </a>
                         </li>
                         <li>      
                             <i class='fa fa-folder'> </i>
                             <a href ='/'> Arquivo </a>
                         </li>
                         <li class='active'>
                           <i class='fa fa-industry'> </i>
                           <a href='/Fornecedor'> Fornecedor </a>
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
                             <a href ='/'> Arquivo </a>
                         </li>
                         <li>
                           <i class='fa fa-industry'> </i>
                           <a href='/Fornecedor'> Fornecedor </a>
                         </li>
                         <li class='active'> Cadastro
                         </li>
                    </ol>";
        }

        // GET: Fornecedor
        [DireitoAcesso(Constantes.AC_CONS_CAD_FORNEC)]
        public ActionResult Index(int tpfiltro = 1, string filtro = "", int pagina = 1, int tamPag = Constantes.TamanhoPagina)
        {
            TempData["breadcrumb"] = CreateBreadCrumbIndex();
            TempData["nometela"] = "Fornecedor";
           

            var lista = PrepararViewModel(tpfiltro, filtro, pagina, tamPag);

            return View(lista);

        }

        private IPagedList<Fornecedor> PrepararViewModel(int tpfiltro, string filtro, int pagina, int tamPag)
        {
            //PopularViewBag();
            var lstCli = new List<Fornecedor>();

            lstCli = servico.ObterListaObjetos(db, filtro,  tpfiltro).ToList();

            ViewBag.Filtro = filtro;
            ViewBag.TamanhoPagina = tamPag;
            ViewBag.TpFiltro = tpfiltro;
            return lstCli.ToPagedList(pagina, tamPag);
        }

        [DireitoAcesso(Constantes.AC_INC_CAD_FORNEC)]
        public ActionResult Create()
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Novo Fornecedor";
            TempData["lboper"] = "Novo";

            var model = CriarViewModelAddEdit();
            model.Id = -1;
            model.Uf = "RJ";
            model.Cidade = "Rio de Janeiro";
            model.Tel1 = "(21)";
            model.Tel0800 = "(0800)";
            model.Fax1 = "(21)";
            model.Celular1 = "(21)";
            model.UfRs = "RJ";
            model.CidadeRs = "Rio de Janeiro";
            model.Tel2 = "(21)";
            model.Fax2 = "(21)";
            model.Celular1 = "(21)";
            model.EhAtivo = "S";
            model.DadosOk = "S";
            model.DtAtu = DateTime.Now;
            model.DtIns = DateTime.Now;
            model.Site = "www.";

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CreateEditViewModel model)
        {
           
            if (!ModelState.IsValid)
            {
                TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
                TempData["nometela"] = "Novo Fornecedor";
                TempData["lboper"] = "Novo";

                ViewBag.Estados = new SelectList(ListasGenericas.ObterEstados, "Sigla", "Nome");
                ViewBag.SimNao = new SelectList(ListasGenericas.ObterSimNao, "Sigla", "Nome");
                return View(model);
            }


            var objeto = new Fornecedor();
            ModelParaObjeto(model, objeto);
          
            try
            {
                servico.Salvar(db, objeto);
                TempData["sucesso"] = $@"Fornecedor salvo com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar salvar o fornecedor {objeto.Nome}";
            }
            return RedirectToAction("Index");
        }

        private CreateEditViewModel CriarViewModelAddEdit()
        {


            ViewBag.Estados = new SelectList(ListasGenericas.ObterEstados, "Sigla", "Nome");
            ViewBag.SimNao = new SelectList(ListasGenericas.ObterSimNao, "Sigla", "Nome");

            var model = new CreateEditViewModel();
            return model;
        }

        private Fornecedor ModelParaObjeto(CreateEditViewModel model, Fornecedor objeto)
        {
            objeto.Id = model.Id;
            objeto.Bairro = model.Bairro;
            objeto.BairroRs = model.BairroRs;
            objeto.Cargo = model.Cargo;
            objeto.Celular1 = model.Celular1;
            objeto.Celular2 = model.Celular2;

            if (!string.IsNullOrEmpty(objeto.Cep))
                objeto.Cep = model.Cep.Replace(".", "").Replace("-", "");

            if (!string.IsNullOrEmpty(objeto.CepRs))
                objeto.CepRs = model.CepRs.Replace(".", "").Replace("-", ""); ;

            objeto.Cidade = model.Cidade;
            objeto.CidadeRs = model.CidadeRs;
            objeto.CNPJ = model.Cnpj;
            objeto.Contato = model.Contato;
            objeto.CPF = model.Cpf;
            objeto.DadosOk = model.DadosOk;
            objeto.DtAtu = model.DtAtu;
            objeto.DtIns = model.DtIns;
            objeto.Email = model.Email;
            objeto.Endereco = model.End;
            objeto.EnderecoRs = model.EndRs;
            objeto.Fax1 = model.Fax1;
            objeto.Fax2 = model.Fax2;
            objeto.InscEstadual = model.InscEstadual;
            objeto.InscMunicipal = model.InscMunicipal;
            objeto.Nome = model.Nome;
            objeto.Obs = model.Obs;
            objeto.RazaoSocial = model.RazaoSocial;
            objeto.Site = model.Site;
            objeto.Tel1 = model.Tel1;
            objeto.Tel2 = model.Tel2;
            objeto.Tel0800 = model.Tel0800;            
            objeto.Uf = model.Uf;
            objeto.UfRs = model.UfRs;

            return objeto;
        }

        [DireitoAcesso(Constantes.AC_EDIT_CAD_FORNEC)]
        public ActionResult Edit(int id)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Editar Fornecedor";
            TempData["lboper"] = "Editar";

            var model = CriarViewModelAddEdit();
            var objeto = servico.ObterObjetoPorId(db, id);
            if (objeto == null)
            {
                return HttpNotFound();
            }
            ObjetoParaModel(objeto, model);

            return View("Create", model);
        }

        [HttpPost]
        public ActionResult Edit(CreateEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
                TempData["nometela"] = "Editar Fornecedor";
                TempData["lboper"] = "Editar";

                ViewBag.Estados = new SelectList(ListasGenericas.ObterEstados, "Sigla", "Nome");
                ViewBag.SimNao = new SelectList(ListasGenericas.ObterSimNao, "Sigla", "Nome");
                return View("Create", model);
            }
            var cliente = servico.ObterObjetoPorId(db, model.Id);
            if (cliente == null)
            {
                return HttpNotFound();
            }

            ModelParaObjeto(model, cliente);

            try
            {
                servico.Salvar(db, cliente);
                TempData["sucesso"] = $@"Fornecedor salvo com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar editar o Fornecedor {cliente.Nome}";
            }


            return RedirectToAction("Index");
        }

        private void ObjetoParaModel(Fornecedor objeto, CreateEditViewModel model)
        {
            model.Id = objeto.Id;
            model.Bairro = objeto.Bairro;
            model.BairroRs = objeto.BairroRs;
            model.Cargo = objeto.Cargo;
            model.Celular1 = objeto.Celular1;
            model.Celular2 = objeto.Celular2;
            model.Cep = objeto.Cep;
            model.CepRs = objeto.CepRs;
            model.Cidade = objeto.Cidade;
            model.CidadeRs = objeto.CidadeRs;
            model.Cnpj = objeto.CNPJ;
            model.Contato = objeto.Contato;
            model.Cpf = objeto.CPF;
            model.DadosOk = objeto.DadosOk;
            model.DtAtu = objeto.DtAtu;
            model.DtIns = objeto.DtIns;
            model.Email = objeto.Email;
            model.End = objeto.Endereco;
            model.EndRs = objeto.EnderecoRs;
            model.Fax1 = objeto.Fax1;
            model.Fax2 = objeto.Fax2;
            model.InscEstadual = objeto.InscEstadual;
            model.InscMunicipal = objeto.InscMunicipal;
            model.Nome = objeto.Nome;
            model.Obs = objeto.Obs;
            
            model.RazaoSocial = objeto.RazaoSocial;
            model.Site = objeto.Site;
            model.Tel1 = objeto.Tel1;
            model.Tel2 = objeto.Tel2;
            model.Tel0800 = objeto.Tel0800;
            model.Uf = objeto.Uf;
            model.UfRs = objeto.UfRs;
            model.EhAtivo = objeto.EhAtivo; 
        }


        [DireitoAcesso(Constantes.AC_APG_CAD_FORNEC)]
        public ActionResult Delete(int id)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Apagar Fornecedor";

            var objeto = servico.ObterObjetoPorId(db, id);
            DeleteViewModel model = new DeleteViewModel();
            model.Id = objeto.Id;
            model.Nome = objeto.Nome;
            return View(model);
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
                TempData["sucesso"] = $@"O Fornecedor {objeto.Nome} foi apagado com sucesso!";
            }
            catch
            {
                TempData["erro"] = $@"Erro ao tentar apagar o Fornecedor {objeto.Nome}";
            }
            return RedirectToAction("Index");
        }


    }
}