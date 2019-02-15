using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.Servico
{
    public class ServicoContaPagarFixa
    {
        ServicoBanco servBanco = new ServicoBanco();
        ServicoContaCorrente servCC = new ServicoContaCorrente();
        ServicoFormaPag servFormapag = new ServicoFormaPag();
        ServicoSetor servSetor = new ServicoSetor();
        ServicoSubDespesa servSubDesp = new ServicoSubDespesa();
        ServicoDespesa servDesp = new ServicoDespesa();
        ServicoFornecedor servFornec = new ServicoFornecedor();
        ServicoTipoDoc servTpdoc = new ServicoTipoDoc();

        public ContaPagarFixa ObterObjetoPorId(ZenContext db, int id)
        {
            return db.ContasPagarFixas.Find(id);
        }

        public void Salvar(ZenContext db, ContaPagarFixa objeto)
        {
            if (ObterObjetoPorId(db, objeto.Id) == null)
            {
                objeto.Id = db.ContasPagarFixas.Max(c => c.Id) + 1;
                db.ContasPagarFixas.Add(objeto);
            }

            db.SaveChanges();
        }

        public void Delete(ZenContext db, ContaPagarFixa objeto)
        {
            db.ContasPagarFixas.Remove(objeto);
            db.SaveChanges();
        }

        public IQueryable<ContaPagarFixa> ObterListaObjetos(ZenContext db, string filtroNome, int tpfiltro)
        {
            var lst = new List<ContaPagarFixa>();

            if (string.IsNullOrEmpty(filtroNome))
            {
                var q = (from cr in db.ContasPagarFixas                        
                         join cl in db.Fornecedores on cr.IdFornecedor equals cl.Id
                         into _cl
                         from cl in _cl.DefaultIfEmpty()
                         join tr in db.SubDespesas on cr.IdSubDesp equals tr.Id
                         into _tr
                         from tr in _tr.DefaultIfEmpty()
                         join cc in db.ContasCorrentes on cr.IdCc equals cc.Id
                         into _cc
                         from cc in _cc.DefaultIfEmpty()
                         join st in db.Setores on cr.IdSetor equals st.Id
                         into _st
                         from st in _st.DefaultIfEmpty()
                         join fp in db.FormasPag on cr.IdFormaPag equals fp.Id
                         into _fp
                         from fp in _fp.DefaultIfEmpty()
                         join td in db.TiposDoc on cr.IdTipoDoc equals td.Id
                         into _td
                         from td in _td.DefaultIfEmpty()
                         select new { cr, cl, tr,  cc, st, fp, td });

                foreach (var item in q)
                {
                    lst.Add(new ContaPagarFixa
                    {
                        Id = item.cr.Id,
                        Dia = item.cr.Dia ,
                        NumParc = item.cr.NumParc,
                        NumParcAtu = item.cr.NumParcAtu,
                        Estado = item.cr.Estado,
                        Historico = item.cr.Historico,
                        IdCc = item.cr.IdCc,
                        IdFornecedor = item.cr.IdFornecedor,
                        IdFormaPag = item.cr.IdFormaPag,
                        IdSetor = item.cr.IdSetor,
                        IdTipoDoc = item.cr.IdTipoDoc,
                        IdSubDesp = item.cr.IdSubDesp,
                        IdUsuario = item.cr.IdUsuario,
                        IdDesp = item.cr.IdDesp,
                        Valor = item.cr.Valor,
                        Fornecedor = item.cl,
                        SubDespesa = item.tr,
                        Setor = item.st,
                        ContaCorrente = item.cc,
                        FormaPag = item.fp,
                        TipoDoc = item.td
                    });
                }
            }
           
            return lst.AsQueryable();
        }

    }
}