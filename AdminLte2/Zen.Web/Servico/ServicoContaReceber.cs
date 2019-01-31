using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.Servico
{
    public class ServicoContaReceber
    {
        ServicoBanco servBanco = new ServicoBanco();
        ServicoContaCorrente servCC = new ServicoContaCorrente();
        ServicoFormaPag servFormapag = new ServicoFormaPag();
        ServicoSetor servSetor = new ServicoSetor();
        ServicoTipoReceita servTpRec = new ServicoTipoReceita();
        ServicoCliente servCli = new ServicoCliente();
        ServicoTipoDoc servTpdoc = new ServicoTipoDoc();

        public ContasReceber ObterObjetoPorId(ZenContext db, int id)
        {
            return db.ContasReceber.Find(id);
        }

        public IQueryable<ContasReceber> ObterListaObjetos(ZenContext db, string filtroNome, int tpfiltro)
        {
            var lst = new List<ContasReceber>();
            switch (tpfiltro)
            {
                //historico
                case 1:
                    if (string.IsNullOrEmpty(filtroNome))
                        lst = db.ContasReceber.OrderByDescending(u => u.DtVenc).ToList();
                    else
                        lst = db.ContasReceber.Where(u => u.Historico.Contains(filtroNome)).OrderByDescending(u => u.DtVenc).ToList();

                    break;

                case 2:
                    {
                        var lsttpreceita = servTpRec.ObterListaObjetos(db, filtroNome).ToList();
                        var ids = "";
                        foreach (var item in lsttpreceita)
                        {
                            if (ids == "")
                                ids = item.Id.ToString();
                            else
                                ids += "," + item.Id.ToString();
                        }
                        lst = db.ContasReceber.Where(u => u.IdTipoReceita.ToString().Contains(ids)).ToList();
                    break;
                    }
                
            }

            var i=0;
            var lstCompl = new List<ContasReceber>();
            foreach (var item in lst)
            {
                i++;
                var id = 0;
               
                if (item.IdBancoCheque != null)
                {
                    id = item.IdBancoCheque.Value;
                    item.BancoCheque = servBanco.ObterObjetoPorId(db, id.ToString());
                }
                /*
               if (item.IdCc != null)
                   item.ContaCorrente = servCC.ObterObjetoPorId(db, item.IdCc.Value);

               if (item.IdCliente != null)
                   item.Cliente = servCli.ObterObjetoPorId(db, item.IdCliente.Value);

               if (item.IdFormaPag != null)
                   item.FormaPag = servFormapag.ObterObjetoPorId(db, item.IdFormaPag.Value);

               if (item.IdSetor != null)
                   item.Setor = servSetor.ObterObjetoPorId(db, item.IdSetor.Value);

               if (item.IdTipoDoc != null)
                   item.TipoDoc = servTpdoc.ObterObjetoPorId(db, item.IdTipoDoc.Value);


               if (item.IdTipoReceita != null)
                   item.TipoReceita = servTpRec.ObterObjetoPorId(db, item.IdTipoReceita.Value);
                      */
                lstCompl.Add(item);
            }

            return lstCompl.AsQueryable();
        }

        public void Salvar(ZenContext db, ContasReceber objeto)
        {
            if (ObterObjetoPorId(db, objeto.Id) == null)
            {
                objeto.Id = db.ContasReceber.Max(c => c.Id) + 1;
                db.ContasReceber.Add(objeto);
            }

            db.SaveChanges();
        }

        public void Delete(ZenContext db, ContasReceber objeto)
        {
            db.ContasReceber.Remove(objeto);
            db.SaveChanges();
        }

    }
}