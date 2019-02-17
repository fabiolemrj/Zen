﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;
using Zen.Web.Models;
using Zen.Web.Servico;

namespace Zen.Web.Controllers
{
    public class FluxoCaixaController : CustomController
    {
        ZenContext db = new ZenContext();
        ServicoFluxoCaixa servico = new ServicoFluxoCaixa();

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
                           <a href='/FluxoCaixa'> Fluxo de Caixa </a>
                         </li>
                         
                    </ol>";

        }
        // GET: FluxoCaixa
        public ActionResult Index(int pagina = 1, int tamPag = Constantes.TamanhoPagina100, string dtini = "", string dtfim = "", string sentido = "T", 
            string hist = "", string forneccli = "", string formapag = "", string estado = "T")
        {
            TempData["breadcrumb"] = CreateBreadCrumbIndex();
            TempData["nometela"] = "Fluxo de Caixa";

            var lista = PrepararViewModel(pagina, tamPag, dtini, dtfim, sentido,hist,forneccli,formapag, estado);

            return View(lista);

        }

        private IPagedList<FluxoCaixa> PrepararViewModel(int pagina, int tamPag, string dtini, string dtfim,
            string sentido, string historico,string fornecCli, string formapag, string estado)
        {
            //PopularViewBag();
            var lst = new List<FluxoCaixa>();
            var lstAux = new List<FluxoCaixa>();

            ViewBag.Sentido = sentido;

            ViewBag.TamanhoPagina = tamPag;
            ViewBag.hist = historico;
            ViewBag.forneccli = fornecCli;
            ViewBag.formapag = formapag;
            ViewBag.estado = estado;
       

            var _dtini = DateTime.Now;
            var _dtfim = DateTime.Now.AddDays(30);

            if (!string.IsNullOrEmpty(dtini) && !string.IsNullOrEmpty(dtfim))
            {
                try
                {
                    _dtini = Convert.ToDateTime(dtini);
                    _dtfim = Convert.ToDateTime(dtfim);
                }
                catch (Exception ex)
                {
                    _dtini = DateTime.Now;
                    _dtfim = DateTime.Now.AddDays(30);
                }
            }
            ViewBag.dtini = dtini;
            ViewBag.dtfim = dtfim;

            lstAux = servico.ObterListaObjetos(db, _dtini, _dtfim, sentido,historico,fornecCli,formapag).OrderByDescending(c=>c.DtVenc).ToList();

            if (!string.IsNullOrEmpty(historico))
            {
                lst.AddRange(lstAux.Where(c=> c.Historico != null && c.Historico.Contains(historico)));
                lstAux.Clear();
            }
            else
            {
                lst.AddRange(lstAux);
                lstAux.Clear();
            }

            if (!string.IsNullOrEmpty(fornecCli))
            {
                lstAux.AddRange(lst.Where(c => c.FornecCliente != null && c.FornecCliente.Contains(fornecCli)));
                lst.Clear();
            }
            else
            {
                lstAux.AddRange(lst);
                lst.Clear();
            }

            if (!string.IsNullOrEmpty(formapag))
            {
                
                lst.AddRange(lstAux.Where(c => c.Formapag != null && c.Formapag.Nome.Contains(formapag)));
                lstAux.Clear();
            }
            else
            {
                lst.AddRange(lstAux);
                lstAux.Clear();
            }

            if (estado != "T")
            {
                lstAux.AddRange(lst.Where(c => c.Estado.Contains(estado)));
                lst.Clear();
            }
            else
            {
                lstAux.AddRange(lst);
                lst.Clear();
            }

            var desp = 0.0;
            var cred = 0.0;
            foreach(var item in lstAux)
            {
                if (item.Sentido == "D")
                    desp += item.Valor;
                else if (item.Sentido == "C")
                    cred += item.Valor;
            }

            var lstgrupo = new List<FormaPagGrupo>();
            var grpfpNull = new FormaPagGrupo();
            grpfpNull.Id = -1;
            grpfpNull.Nome = "Nulo";

            foreach(var item in lstAux)
            {
                if (item.Formapag != null)
                {
                    var fp = item.Formapag;
                    if (lstgrupo.FirstOrDefault(c => c.Id == fp.Id) == null)
                    {
                        lstgrupo.Add(new FormaPagGrupo {
                            Id = fp.Id,
                            Nome = fp.Nome,
                            Valor = item.Valor
                        });
                    }
                    else
                    {
                        var grupo = lstgrupo.FirstOrDefault(c => c.Id == fp.Id);
                        lstgrupo.Remove(grupo);

                        grupo.Valor += item.Valor;
                        lstgrupo.Add(grupo);
                    }
                }
                else
                {
                    grpfpNull.Valor += item.Valor;
                }
            }
            lstgrupo.Add(grpfpNull);
            //lstgrupo.AddRange(.ToList());

            ViewBag.saldocr = string.Format("{0:c}", cred);
            ViewBag.saldocp = string.Format("{0:c}", desp); 
            ViewBag.saldotot = string.Format("{0:c}", (cred - desp));
            ViewBag.lstFormapag = lstgrupo;
            return lstAux.ToPagedList(pagina, tamPag);
        }
    }
}