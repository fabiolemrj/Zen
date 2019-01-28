using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zen.Web.Models;
using Zen.Web.Servico;
using Zen.Web.ViewModels.ConfigEmailViewModel;

namespace Zen.Web.Controllers
{
    public class ConfiguracaoController : CustomController
    {
        // GET: Configuracao
        ZenContext db = new ZenContext();
        ServicoConfigEmail servico = new ServicoConfigEmail();
        private string CreateBreadCrumbIndex()
        {
            return $@"<ol class='breadcrumb'>
                         <li>                            
                             <a href ='/'> Principal </a>
                         </li>
                         <li class='active'>
                           <i class='fa fa-wrench'> </i> Configuração 
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
                           <i class='fa fa-wrench'> </i>
                           <a href='/Configuracoes'> Configuração </a>
                         </li>
                         <li class='active'> Cadastro
                         </li>
                    </ol>";
        }
        // GET: Configuracao
        [DireitoAcesso(Constantes.AC_EDIT_CONFIG)]
        public ActionResult Editar()
        {
            TempData["breadcrumb"] = CreateBreadCrumbIndex();
            TempData["nometela"] = "Configuração";

            var configEmail = servico.ObterObjeto(db);
            var model = new EditarViewModel();
            model.Email = configEmail.Email;
            model.PortaSmtp = configEmail.PortaSmtp;
            model.Remetente = configEmail.Remetente;
            model.Senha = configEmail.Senha;
            model.EndSmtp = configEmail.EndSmtp;
            model.Usuario = configEmail.Usuario;
            model.Autentica = configEmail.Autentica == "S";
            model.Id = configEmail.Id;

            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(EditarViewModel model)
        {
            TempData["breadcrumb"] = CreateBreadCrumbIndex();
            TempData["nometela"] = "Configuração";
            var configEmail = servico.ObterObjeto(db);
            if (model.Autentica)
                configEmail.Autentica = "S";
            else
                configEmail.Autentica = "N";

            configEmail.Email = model.Email;
            configEmail.EndSmtp = model.EndSmtp;
            configEmail.PortaSmtp = model.PortaSmtp;
            configEmail.Remetente = model.Remetente;
            configEmail.Senha = model.Senha;
            configEmail.Usuario = model.Usuario;

            servico.Salvar(db, configEmail);
            return View(model);
        }
    }
}