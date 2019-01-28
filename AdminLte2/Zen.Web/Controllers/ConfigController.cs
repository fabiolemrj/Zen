using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zen.Web.Models;
using Zen.Web.Servico;
using Zen.Web.ViewModels.ConfigViewModel;

namespace Zen.Web.Controllers
{
    public class ConfigController : Controller
    {
        ZenContext db = new ZenContext();
        ServicoConfig servico = new ServicoConfig();

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
                             <i class='fa fa-folder-o'> </i>
                             <a href ='/'> Tabela de Custos </a>
                         </li>
                         <li class='active'>
                           <i class='fa fa-cog'> </i>
                           <a href='/Config'> Configurações </a>
                         </li>
                         
                    </ol>";
        }

        [DireitoAcesso(Constantes.AC_EDIT_CONFIG)]
        public ActionResult Edit(int id = 1)
        {
            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Editar Tabela de Custos";
            TempData["lboper"] = "Editar";

            var model = CriarViewModelAddEdit();
            var objeto = servico.ObterObjetoPorId(db, id);
            if (objeto == null)
            {
                return HttpNotFound();
            }
            ObjetoParaModel(objeto, model);

            return View("Edit", model);
        }

        private CreateEditViewModel CriarViewModelAddEdit()
        {
            var model = new CreateEditViewModel();
            return model;
        }

        private void ObjetoParaModel(Config objeto, CreateEditViewModel model)
        {
            model.Id = objeto.Id;
            model.CaminhoDoc = objeto.CaminhoDoc;
            model.CaminhoLayoutOsi = objeto.CaminhoLayoutOsi;
            model.PathBackup = objeto.PathBackup;

            model.ImpIss = objeto.ImpIss;
            model.ImpSimples = objeto.ImpSimples;
            model.RiscoManuseio = objeto.RiscoManuseio;
            model.RiscoS3 = objeto.RiscoS3;
            model.Vr100Hs = objeto.Vr100Hs;
            model.Vr100Rs = objeto.Vr100Rs;
            model.VrAdd100Hs = objeto.VrAdd100Hs;
            model.VrAdd100Rs = objeto.VrAdd100Rs;
            model.VrArte1 = objeto.VrArte1;
            model.VrArte2 = objeto.VrArte2;
            model.VrArte3 = objeto.VrArte3;
            model.VrCliche1Hs = objeto.VrCliche1Hs;
            model.VrCliche1Rs = objeto.VrCliche1Rs;
            model.VrCliche2Hs = objeto.VrCliche2Hs;
            model.VrCliche2Rs = objeto.VrCliche2Rs;
            model.VrCliche3Hs = objeto.VrCliche3Hs;
            model.VrCliche3Rs = objeto.VrCliche3Rs;
            model.VrCola1 = objeto.VrCola1;
            model.VrCola2 = objeto.VrCola2;
            model.VrCola3 = objeto.VrCola3;
            model.VrContra1 = objeto.VrContra1;
            model.VrContra2 = objeto.VrContra2;
            model.VrContra3 = objeto.VrContra3;
            model.VrDobra1 = objeto.VrDobra1;
            model.VrDobra2 = objeto.VrDobra2;
            model.VrDobra3 = objeto.VrDobra3;
            model.VrEntrMaqG = objeto.VrEntrMaqG;
            model.VrEntrMaqM = objeto.VrEntrMaqM;
            model.VrEntrMaqP = objeto.VrEntrMaqP;
            model.VrFoto1 = objeto.VrFoto1;
            model.VrFoto2 = objeto.VrFoto2;
            model.VrFoto3 = objeto.VrFoto3;
            model.VrImpDia1 = objeto.VrImpDia1;
            model.VrImpDia2 = objeto.VrImpDia2;
            model.VrImpDia3 = objeto.VrImpDia3;
            model.VrImpDiaMin = objeto.VrImpDiaMin;
            model.VrImpMg = objeto.VrImpMg;
            model.VrImpMm = objeto.VrImpMm;
            model.VrImpMp = objeto.VrImpMp;
            model.VrImpSag = objeto.VrImpSag;
            model.VrImpSam = objeto.VrImpSam;
            model.VrImpSap = objeto.VrImpSap;
            model.VrImpSm = objeto.VrImpSm;
            model.VrLamComum = objeto.VrLamComum;
            model.VrLamEspecial = objeto.VrLamEspecial;
            model.VrLaminacao1 = objeto.VrLaminacao1;
            model.VrLaminacao2 = objeto.VrLaminacao2;
            model.VrLaminacao3 = objeto.VrLaminacao3;
            model.VrMinFaca = objeto.VrMinFaca;
            model.VrMinFoto = objeto.VrMinFoto;
            model.VrUsoFaca = objeto.VrUsoFaca;
            model.VrUsoFaca4060 = objeto.VrUsoFaca4060;
            model.VrUsoFaca4060Meio = objeto.VrUsoFaca4060Meio;
            model.VrUsoFacaEspec = objeto.VrUsoFacaEspec;
            model.VrUsoFacaMeio = objeto.VrUsoFacaMeio;
            model.VrVazador = objeto.VrVazador;
        }


        [HttpPost]
        public ActionResult Edit(CreateEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
                TempData["nometela"] = "Editar Tabela de Custos";

                return View("Edit", model);
            }

            TempData["breadcrumb"] = CreateBreadCrumbCreatEdit();
            TempData["nometela"] = "Editar Tabela de Custos";
            TempData["lboper"] = "Editar";

            var objeto = servico.ObterObjetoPorId(db, model.Id);
            if (objeto == null)
            {
                return HttpNotFound();
            }

            ModelParaObjeto(model, objeto);

            try
            {
                servico.Salvar(db, objeto);
                TempData["sucesso"] = $@"Tabela de custos salva com sucesso!";
            }
            catch (Exception e)
            {
                TempData["erro"] = $@"Erro ao tentar editar tabela de custos";
            }
            return View("Edit", model);
        }

        private Config ModelParaObjeto(CreateEditViewModel objeto, Config model)
        {
            objeto.Id = model.Id;
            objeto.CaminhoDoc = model.CaminhoDoc;
            objeto.CaminhoLayoutOsi = model.CaminhoLayoutOsi;
            objeto.PathBackup = model.PathBackup;

            objeto.ImpIss = model.ImpIss;
            objeto.ImpSimples = model.ImpSimples;
            objeto.RiscoManuseio = model.RiscoManuseio;
            objeto.RiscoS3 = model.RiscoS3;
            objeto.Vr100Hs = model.Vr100Hs;
            objeto.Vr100Rs = model.Vr100Rs;
            objeto.VrAdd100Hs = model.VrAdd100Hs;
            objeto.VrAdd100Rs = model.VrAdd100Rs;
            objeto.VrArte1 = model.VrArte1;
            objeto.VrArte2 = model.VrArte2;
            objeto.VrArte3 = model.VrArte3;
            objeto.VrCliche1Hs = model.VrCliche1Hs;
            objeto.VrCliche1Rs = model.VrCliche1Rs;
            objeto.VrCliche2Hs = model.VrCliche2Hs;
            objeto.VrCliche2Rs = model.VrCliche2Rs;
            objeto.VrCliche3Hs = model.VrCliche3Hs;
            objeto.VrCliche3Rs = model.VrCliche3Rs;
            objeto.VrCola1 = model.VrCola1;
            objeto.VrCola2 = model.VrCola2;
            objeto.VrCola3 = model.VrCola3;
            objeto.VrContra1 = model.VrContra1;
            objeto.VrContra2 = model.VrContra2;
            objeto.VrContra3 = model.VrContra3;
            objeto.VrDobra1 = model.VrDobra1;
            objeto.VrDobra2 = model.VrDobra2;
            objeto.VrDobra3 = model.VrDobra3;
            objeto.VrEntrMaqG = model.VrEntrMaqG;
            objeto.VrEntrMaqM = model.VrEntrMaqM;
            objeto.VrEntrMaqP = model.VrEntrMaqP;
            objeto.VrFoto1 = model.VrFoto1;
            objeto.VrFoto2 = model.VrFoto2;
            objeto.VrFoto3 = model.VrFoto3;
            objeto.VrImpDia1 = model.VrImpDia1;
            objeto.VrImpDia2 = model.VrImpDia2;
            objeto.VrImpDia3 = model.VrImpDia3;
            objeto.VrImpDiaMin = model.VrImpDiaMin;
            objeto.VrImpMg = model.VrImpMg;
            objeto.VrImpMm = model.VrImpMm;
            objeto.VrImpMp = model.VrImpMp;
            objeto.VrImpSag = model.VrImpSag;
            objeto.VrImpSam = model.VrImpSam;
            objeto.VrImpSap = model.VrImpSap;
            objeto.VrImpSm = model.VrImpSm;
            objeto.VrLamComum = model.VrLamComum;
            objeto.VrLamEspecial = model.VrLamEspecial;
            objeto.VrLaminacao1 = model.VrLaminacao1;
            objeto.VrLaminacao2 = model.VrLaminacao2;
            objeto.VrLaminacao3 = model.VrLaminacao3;
            objeto.VrMinFaca = model.VrMinFaca;
            objeto.VrMinFoto = model.VrMinFoto;
            objeto.VrUsoFaca = model.VrUsoFaca;
            objeto.VrUsoFaca4060 = model.VrUsoFaca4060;
            objeto.VrUsoFaca4060Meio = model.VrUsoFaca4060Meio;
            objeto.VrUsoFacaEspec = model.VrUsoFacaEspec;
            objeto.VrUsoFacaMeio = model.VrUsoFacaMeio;
            objeto.VrVazador = model.VrVazador;

            return model;
        }
    }
}