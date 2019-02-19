using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zen.Web.Models;
using Zen.Web.Servico;
using Zen.Web.ViewModels.ClienteViewModel;
using X.PagedList;
using Zen.Web.Utils;

namespace Zen.Web.Controllers
{
    public class ClienteController : CustomController
    {
        ZenContext db = new ZenContext();
        ServicoCliente servico = new ServicoCliente();

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
                           <i class='fa fa-male'> </i>
                           <a href='/cliente'> Clientes </a>
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
                           <i class='fa fa-male'> </i>
                           <a href='/Cliente'> Clientes </a>
                         </li>
                         <li class='active'> Cadastro
                         </li>
                    </ol>";
        }
        // GET: Cliente

        [DireitoAcesso(Constantes.AC_CONS_CAD_CLI)]
        public ActionResult Index(int tpfiltro = 1, string filtro = "", string ehDesigner = "N", int pagina = 1, int tamPag = Constantes.TamanhoPagina)
        {
            TempData["breadcrumb"] = CreateBreadCrumbIndex();
            TempData["nometela"] = "Clientes";
            var model = new IndexViewModel();
            model.EhDesigner = false;

            var lista = PrepararViewModel(tpfiltro, filtro, ehDesigner == "on", pagina, tamPag);

            return View(lista);

        }

        private IPagedList<Cliente> PrepararViewModel(int tpfiltro, string filtro, bool ehDesigner, int pagina, int tamPag)
        {
            //PopularViewBag();
            var lstCli = new List<Cliente>();

            lstCli = servico.ObterListaObjetos(db, filtro, ehDesigner, tpfiltro).ToList();

            ViewBag.Filtro = filtro;
            ViewBag.TamanhoPagina = tamPag;
            ViewBag.TpFiltro = tpfiltro;
            ViewBag.EhDesigner = ehDesigner;
            return lstCli.ToPagedList(pagina, tamPag);
        }

        [DireitoAcesso(Constantes.AC_INC_CAD_CLI)]
        public ActionResult Create()
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Novo Cliente";
            TempData["lboper"] = "Novo";

            var model = CriarViewModelAddEdit();
            model.IdCliente = -1;
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
            model.EhAtivo = true;
            model.EhCliente = "S";
            model.EhDesign = "N";
            model.DadosOk = "S";
            model.DtAtu = DateTime.Now;
            model.DtIns = DateTime.Now;
            model.Site = "www.";

            return View(model);
        }

        private CreateEditViewModel CriarViewModelAddEdit()
        {
            

            ViewBag.Estados = new SelectList(ListasGenericas.ObterEstados, "Sigla", "Nome");
            ViewBag.SimNao = new SelectList(ListasGenericas.ObterSimNao, "Sigla", "Nome");

            var model = new CreateEditViewModel();
            return model;
        }
                

        [HttpPost]
        public ActionResult Create(CreateEditViewModel model)
        {
            if (model.IdCliente > 0)
            {
                Edit(model);
            }

            if (!ModelState.IsValid)
            {
                TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
                TempData["nometela"] = "Novo Cliente";
                TempData["lboper"] = "Novo";

                ViewBag.Estados = new SelectList(ListasGenericas.ObterEstados, "Sigla", "Nome");
                ViewBag.SimNao = new SelectList(ListasGenericas.ObterSimNao, "Sigla", "Nome");
                return View(model);
            }
            

            var cliente = new Cliente();
            ModelParaObjeto(model, cliente);
            if (model.EhAtivo)
            {
                cliente.EhAtivo = "S";
            }
            else
            {
                cliente.EhAtivo = "N";
            }

            try
            {
                servico.Salvar(db, cliente);
                TempData["sucesso"] = $@"Cliente salvo com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar editar o cliente {cliente.Nome}";
            }
            return RedirectToAction("Index");
        }

        [DireitoAcesso(Constantes.AC_EDIT_CAD_CLI)]
        public ActionResult Edit(int id)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Editar Cliente";
            TempData["lboper"] = "Editar";

            var model = CriarViewModelAddEdit();
            var cliente = servico.ObterObjetoPorId(db, id);
            if(cliente == null)
            {
                return HttpNotFound();
            }
            ObjetoParaModel(cliente,model);

            return View("Create",model);
        }

        [HttpPost]
        public ActionResult Edit(CreateEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
                TempData["nometela"] = "Editar Cliente";
                TempData["lboper"] = "Editar";

                ViewBag.Estados = new SelectList(ListasGenericas.ObterEstados, "Sigla", "Nome");
                ViewBag.SimNao = new SelectList(ListasGenericas.ObterSimNao, "Sigla", "Nome");
                return View("Create",model);
            }
            var cliente = servico.ObterObjetoPorId(db, model.IdCliente);
            if(cliente == null)
            {
                return HttpNotFound();
            }
     
            ModelParaObjeto(model,cliente);

            if (model.EhAtivo)
            {
                cliente.EhAtivo = "S";
            }
            else
            {
                cliente.EhAtivo = "N";
            }

            try
            {
                servico.Salvar(db, cliente);
                TempData["sucesso"] = $@"Cliente salvo com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar editar o cliente {cliente.Nome}";
            }

            
            return RedirectToAction("Index");
        }

        [DireitoAcesso(Constantes.AC_APG_CAD_CLI)]
        public ActionResult Delete(int id)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Apagar Cliente";

            var cliente = servico.ObterObjetoPorId(db, id);
            DeleteViewModel model = new DeleteViewModel();
            model.Id = cliente.IdCliente;
            model.Nome = cliente.Nome;
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(DeleteViewModel model)
        {
            var cliente = servico.ObterObjetoPorId(db, model.Id);
            try
            {
                if (cliente != null)
                {
                    servico.Delete(db, cliente);

                }
                TempData["sucesso"] = $@"O cliente {cliente.Nome} foi apagado com sucesso!";
            }
            catch
            {
                TempData["erro"] = $@"Erro ao tentar apagar o cliente {cliente.Nome}";
            }
            return RedirectToAction("Index");
        }

        private void ObjetoParaModel(Cliente cliente, CreateEditViewModel model)
        {
            model.IdCliente = cliente.IdCliente;
            model.Apelido = cliente.Apelido;
            model.Bairro = cliente.Bairro;
            model.BairroRs = cliente.BairroRs;
            model.cargo = cliente.cargo;
            model.Celular1 = cliente.Celular1;
            model.Celular2 = cliente.Celular2;
            model.Cep = cliente.Cep;
            model.CepRs = cliente.CepRs;
            model.Cidade = cliente.Cidade;
            model.CidadeRs = cliente.CidadeRs;
            model.Cnpj = cliente.Cnpj;
            model.Comissao = cliente.Comissao;
            model.Contato = cliente.Contato;
            model.Cpf = cliente.Cpf;
            model.DadosOk = cliente.DadosOk;
            model.DtAtu = cliente.DtAtu;
            model.DtIns = cliente.DtIns;
            model.EhCliente = cliente.EhCliente;
            model.EhDesign = cliente.EhDesign;
            model.Email = cliente.Email;
            model.Endereco = cliente.Endereco;
            model.EndRs = cliente.EndRs;
            model.Fax1 = cliente.Fax1;
            model.Fax2 = cliente.Fax2;
            model.InscEstadual = cliente.InscEstadual;
            model.InscMunicipal = cliente.InscMunicipal;
            model.Nome = cliente.Nome;
            model.Obs = cliente.Obs;
            model.Ramal1 = cliente.Ramal1;
            model.Ramal2 = cliente.Ramal2;
            
            model.RazaoSocial = cliente.RazaoSocial;
            model.Site = cliente.Site;
            model.Tel1 = cliente.Tel1;
            model.Tel2 = cliente.Tel2;
            model.Tel0800 = cliente.Tel0800;
            model.Tel3 = cliente.Tel3;
            model.Uf = cliente.Uf;
            model.UfRs = cliente.UfRs;
            model.EhAtivo = cliente.EhAtivo == "S";
        

        }

        private Cliente ModelParaObjeto(CreateEditViewModel model, Cliente cliente)
        {
            cliente.IdCliente = model.IdCliente;
            cliente.Apelido = model.Apelido.ToUpper();
            cliente.Bairro = model.Bairro.ToUpper();
            cliente.BairroRs = model.BairroRs.ToUpper();
            cliente.cargo = model.cargo.ToUpper();
            cliente.Celular1 = model.Celular1.ToUpper();
            cliente.Celular2 = model.Celular2.ToUpper();

            if (!string.IsNullOrEmpty(cliente.Cep))
              cliente.Cep = model.Cep.Replace(".","").Replace("-","");

            if (!string.IsNullOrEmpty(cliente.CepRs))
                cliente.CepRs = model.CepRs.Replace(".", "").Replace("-", ""); ;

            cliente.Cidade = model.Cidade.ToUpper();
            cliente.CidadeRs = model.CidadeRs.ToUpper();
            cliente.Cnpj = model.Cnpj;
            cliente.Comissao = model.Comissao;
            cliente.Contato = model.Contato.ToUpper();
            cliente.Cpf = model.Cpf;
            cliente.DadosOk = model.DadosOk;
            cliente.DtAtu = model.DtAtu;
            cliente.DtIns = model.DtIns;
            cliente.EhCliente = model.EhCliente;
            cliente.EhDesign = model.EhDesign;
            cliente.Email = model.Email.ToUpper();
            cliente.Endereco = model.Endereco.ToUpper();
            cliente.EndRs = model.EndRs.ToUpper();
            cliente.Fax1 = model.Fax1.ToUpper();
            cliente.Fax2 = model.Fax2.ToUpper();
            cliente.InscEstadual = model.InscEstadual;
            cliente.InscMunicipal = model.InscMunicipal;
            cliente.Nome = model.Nome.ToUpper();
            cliente.Obs = model.Obs.ToUpper();
            cliente.Ramal1 = model.Ramal1.ToUpper();
            cliente.Ramal2 = model.Ramal2.ToUpper();
            cliente.RamalF1 = model.Ramal1.ToUpper();
            cliente.RamalF2 = model.Ramal2.ToUpper();
            cliente.RazaoSocial = model.RazaoSocial.ToUpper();
            cliente.Site = model.Site.ToUpper();
            cliente.Tel1 = model.Tel1.ToUpper();
            cliente.Tel2 = model.Tel2.ToUpper();
            cliente.Tel0800 = model.Tel0800.ToUpper();
            cliente.Tel3 = model.Tel3.ToUpper();
            cliente.Uf = model.Uf.ToUpper();
            cliente.UfRs = model.UfRs.ToUpper();

            return cliente;
        }

       

    }
}