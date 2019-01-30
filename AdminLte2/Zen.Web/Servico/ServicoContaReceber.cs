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
                    var lsttpreceita = servTpRec.ObterListaObjetos(db, filtroNome).ToList();
                    var ids = "";
                    foreach(var item in lsttpreceita)
                    {
                        if (ids == "")
                            ids = item.Id.ToString();
                        else
                            ids += ","+ item.Id.ToString();
                    }
                    lst = db.ContasReceber.Where(u=>u.IdTipoReceita.ToString().Contains(ids)).ToList()
                    break;
                case 3:
                    {
                        break;
                    }
            }

            var lstCompl = new List<ContaCorrente>();
            foreach (var item in lst)
            {
                var id = "";
                if (!string.IsNullOrEmpty(item.IdBanco))
                    id = item.IdBanco;

                item.Banco = servBanco.ObterObjetoPorId(db, id);
                lstCompl.Add(item);
            }

            return lstCompl.AsQueryable();
        }

    }
}