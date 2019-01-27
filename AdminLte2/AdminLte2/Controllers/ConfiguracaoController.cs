using AdminLte2.Models;
using AdminLte2.Servico;
using AdminLte2.ViewModels.ConfigEmailViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminLte2.Controllers
{
    public class ConfiguracaoController : CustomController
    {
        ZenContext db = new ZenContext();
        ServicoConfigEmail servico = new ServicoConfigEmail();
        // GET: Configuracao
        [DireitoAcesso(Constantes.AC_EDIT_CONFIG)]
        public ActionResult Editar()
        {
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