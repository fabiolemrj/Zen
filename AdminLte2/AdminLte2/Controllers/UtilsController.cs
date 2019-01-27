using AdminLte2.Models;
using AdminLte2.Servico;
using AdminLte2.ViewModels.UtilsViewModal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace AdminLte2.Controllers
{
    public class UtilsController : CustomController
    {
        // GET: Utils
        string controlador = string.Empty;
        ZenContext db = new ZenContext();
        ServicoUsuarios servico = new ServicoUsuarios();

        public ActionResult Index(string nmRegistro, string nmControlador)
        {
            var model = new IndexViewModel();
            model.NmRegistro = nmRegistro;
            controlador = nmControlador;
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(IndexViewModel model)
        {
            
            return RedirectToAction("Delete",controlador,new { yesno = model.YesNo });
        }

 


    }
}