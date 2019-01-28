using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;
using Zen.Web.Models;
using Zen.Web.Servico;
using Zen.Web.Utils;
using Zen.Web.ViewModels.MaterialViewModel;

namespace Zen.Web.Controllers
{
    public class MaterialController : Controller
    {
        ZenContext db = new ZenContext();
        ServicoMaterial servico = new ServicoMaterial();
        ServicoFornecedor servFornecedor = new ServicoFornecedor();


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
                           <i class='fa fa-file-image-o'> </i>
                           <a href='/Material'> Insumos </a>
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
                           <i class='fa fa-file-image-o'> </i>
                           <a href='/Material'> Insumos </a>
                         </li>
                         <li class='active'> Cadastro
                         </li>
                    </ol>";
        }

        // GET: Material
        [DireitoAcesso(Constantes.AC_CONS_CAD_MAT)]
        public ActionResult Index(string filtro = "", int pagina = 1, int tamPag = Constantes.TamanhoPagina)
        {
            TempData["breadcrumb"] = CreateBreadCrumbIndex();
            TempData["nometela"] = "Insumo";


            var lista = PrepararViewModel(filtro, pagina, tamPag);

            return View(lista);

        }

        private IPagedList<Material> PrepararViewModel(string filtro, int pagina, int tamPag)
        {
            //PopularViewBag();
            var lstCli = new List<Material>();

            lstCli = servico.ObterListaObjetos(db, filtro).ToList();

            ViewBag.Filtro = filtro;
            ViewBag.TamanhoPagina = tamPag;
            return lstCli.ToPagedList(pagina, tamPag);
        }

        [DireitoAcesso(Constantes.AC_INC_CAD_MAT)]
        public ActionResult Create()
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Novo Insumo";
            TempData["lboper"] = "Novo";

            var model = CriarViewModelAddEdit();
            model.DtAtu = DateTime.Now;

            return View(model);
        }

        private void PreencherCombos()
        {
            ViewBag.SimNao = new SelectList(ListasGenericas.ObterSimNao, "Sigla", "Nome");
            ViewBag.TipoLam = new SelectList(ListasGenericas.TipoLam, "Sigla", "Nome");
            ViewBag.Fornecedor = new SelectList(servFornecedor.ObterListaObjetos(db, "", 1),"Id","Nome");
        }

        private CreateEditViewModel CriarViewModelAddEdit()
        {
            var model = new CreateEditViewModel();
            PreencherCombos();

            return model;
        }

        [HttpPost]
        public ActionResult Create(CreateEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
                TempData["nometela"] = "Novo Insumo";
                TempData["lboper"] = "Novo";
                PreencherCombos();
                return View(model);
            }

            var objeto = new Material();

            ModelParaObjeto(model, objeto);
            try
            {
                servico.Salvar(db, objeto);
                TempData["sucesso"] = $@"Insumo salvo com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar editar o insumo {objeto.Nome}";
            }
            return RedirectToAction("Index");
        }

        private Material ModelParaObjeto(CreateEditViewModel model, Material objeto)
        {
            objeto.Id = model.Id;
            objeto.Nome = model.Nome;
            objeto.Alt = model.Alt;
            objeto.Fabricante = model.Fabricante;
            objeto.Fora = model.Fora;
            objeto.IdFornecedor = model.IdFornecedor;
            objeto.Larg = model.Larg;
            objeto.Nome = model.Nome;
            objeto.NumFolha = model.NumFolha;
            objeto.TipoLam = model.TipoLam;
            objeto.ValorTotal = model.ValorTotal;
            objeto.ValorUnit = model.ValorUnit;
            objeto.DtAtu = model.DtAtu;
            
            return objeto;
        }

        [DireitoAcesso(Constantes.AC_EDIT_CAD_MAT)]
        public ActionResult Edit(int id)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Editar Insumo";
            TempData["lboper"] = "Editar";

            var model = CriarViewModelAddEdit();
            var objeto = servico.ObterObjetoPorId(db, id);
            if (objeto == null)
            {
                PreencherCombos();
                TempData["erro"] = $@"Erro ao tentar editar o insumo {objeto.Id}";
                return RedirectToAction("Index");
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
                TempData["nometela"] = "Editar Insumo";
                TempData["lboper"] = "Editar";
                PreencherCombos();
                return View("Create", model);
            }

            var objeto = servico.ObterObjetoPorId(db, model.Id);
            if (objeto == null)
            {
                TempData["erro"] = $@"Erro ao tentar editar o insumo {objeto.Nome}";
                return RedirectToAction("Index");
            }

            ModelParaObjeto(model, objeto);

            try
            {
                servico.Salvar(db, objeto);
                TempData["sucesso"] = $@"Insumo salvo com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar editar o insumo {objeto.Nome}";
            }

            return RedirectToAction("Index");
        }

        private void ObjetoParaModel(Material objeto, CreateEditViewModel model)
        {
            model.Id = objeto.Id;
            model.Nome = objeto.Nome;
            model.Alt = objeto.Alt.Value;
            model.Fabricante = objeto.Fabricante;
            model.Fora = objeto.Fora;
            model.IdFornecedor = objeto.IdFornecedor.Value;
            model.Larg = objeto.Larg.Value;
            model.Nome = objeto.Nome;
            model.NumFolha = objeto.NumFolha.Value;
            model.TipoLam = objeto.TipoLam;
            model.ValorTotal = objeto.ValorTotal.Value;
            model.ValorUnit = objeto.ValorUnit.Value;
            model.DtAtu = objeto.DtAtu.Value;
        }

        [DireitoAcesso(Constantes.AC_APG_CAD_MAT)]
        public ActionResult Delete(int id)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Apagar Insumo";

            var setor = servico.ObterObjetoPorId(db, id);
            DeleteViewModel model = new DeleteViewModel();
            model.Id = setor.Id;
            model.Nome = setor.Nome;
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
                TempData["sucesso"] = $@"O insumo {objeto.Nome} foi apagado com sucesso!";
            }
            catch
            {
                TempData["erro"] = $@"Erro ao tentar apagar o insumo {objeto.Nome}";
            }
            return RedirectToAction("Index");
        }
    }
}