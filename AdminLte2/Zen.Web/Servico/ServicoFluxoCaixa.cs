using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.Servico
{
    public class ServicoFluxoCaixa
    {
        ServicoBanco servBanco = new ServicoBanco();
        ServicoContaCorrente servCC = new ServicoContaCorrente();
        ServicoFormaPag servFormapag = new ServicoFormaPag();
        ServicoSetor servSetor = new ServicoSetor();
        ServicoSubDespesa servSubDesp = new ServicoSubDespesa();
        ServicoDespesa servDesp = new ServicoDespesa();
        ServicoFornecedor servFornec = new ServicoFornecedor();
        ServicoTipoDoc servTpdoc = new ServicoTipoDoc();
        ServicoContaPagar servCp = new ServicoContaPagar();
        ServicoContaPagarFixa servCpFixa = new ServicoContaPagarFixa();
        ServicoContaReceber ServCr = new ServicoContaReceber();

        public IQueryable<FluxoCaixa> ObterListaObjetos(ZenContext db, DateTime dtMovIni, DateTime dtMovFim, string sentido, string historico, string fornecCli, string formapag)
        {
            var lst = new List<FluxoCaixa>();
            var seq = 0;
            var q = (from cr in db.ContasPagar
                     join bc in db.Bancos on cr.IdBanco.ToString() equals bc.IdBanco
                     into _bc
                     from bc in _bc.DefaultIfEmpty()
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
                     select new { cr, cl, tr, bc, cc, st, fp, td })
                    .Where(c => c.cr.DtVenc >= dtMovIni && c.cr.DtVenc <= dtMovFim);

            foreach (var item in q)
            {
                var hist = "";
                try
                {
                    hist = item.cr.Historico;
                }
                catch
                {
                    hist = "";
                }

                var cliFornec = "";
                try
                {
                    cliFornec = item.cl.Nome;
                }
                catch
                {
                    cliFornec = "";
                }
                

                lst.Add(new FluxoCaixa
                {
                    ContaCorrente = item.cc,
                    Historico = hist,
                    DtVenc = item.cr.DtVenc,
                    Id = seq++,
                    Estado = item.cr.Estado,
                    Sentido = "D",
                    Valor = item.cr.Valor.Value,
                    Formapag = item.fp,
                    TipoDocumento = item.td,
                    FornecCliente = cliFornec,
                    Idtitulo = item.cr.Id
                });
            }

            var qcr = (from cr in db.ContasReceber
                       join bc in db.Bancos on cr.IdBancoCheque.ToString() equals bc.IdBanco
                       into _bc
                       from bc in _bc.DefaultIfEmpty()
                       join cl in db.CLientes on cr.IdCliente equals cl.IdCliente
                       into _cl
                       from cl in _cl.DefaultIfEmpty()
                       join tr in db.TipoReceitas on cr.IdTipoReceita equals tr.Id
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
                       select new { cr, cl, tr, bc, cc, st, fp, td })
                     .Where(c => c.cr.DtVenc >= dtMovIni && c.cr.DtVenc <= dtMovFim);

            foreach (var item in qcr)
            {
                var hist = "";
                try
                {
                    hist = item.cr.Historico;
                }
                catch
                {
                    hist = "";
                }

                var cliFornec = "";
                try
                {
                    cliFornec = item.cl.Nome;
                }
                catch
                {
                    cliFornec = "";
                }

                lst.Add(new FluxoCaixa
                {
                    ContaCorrente = item.cc,

                    Historico = hist,
                    DtVenc = item.cr.DtVenc,
                    Id = seq++,
                    Estado = item.cr.Estado,
                    Sentido = "C",
                    Valor = item.cr.Valor.Value,
                    Formapag = item.fp,
                    TipoDocumento = item.td,
                    FornecCliente = cliFornec,
                    Idtitulo = item.cr.Id
                });
            }

            var lstsentido = new List<FluxoCaixa>();


            if (!string.IsNullOrEmpty(sentido) && (sentido != "T"))
            {
                lstsentido.AddRange(lst.Where(c => c.Sentido == sentido));
            }
            else
            {
                lstsentido.AddRange(lst);
            }

            return lstsentido.OrderByDescending(c => c.DtMov).AsQueryable();
        }
    }
}