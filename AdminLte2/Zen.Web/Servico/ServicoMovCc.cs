using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.Servico
{
    public class ServicoMovCc
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

        public MovCc ObterObjetoPorId(ZenContext db, int id)
        {
            return db.MovCcs.Find(id);
        }

        public void Salvar(ZenContext db, MovCc objeto)
        {
            if (ObterObjetoPorId(db, objeto.NrSeq) == null)
            {
                //objeto.NrSeq = db.MovCcs.Max(c => c.NrSeq) + 1;
                db.MovCcs.Add(objeto);
            }

            db.SaveChanges();
        }

       
            public void Delete(ZenContext db, MovCc objeto)
        {
            db.MovCcs.Remove(objeto);
            db.SaveChanges();
        }


        public void AtualizarMovCcCr(ZenContext db, ContaPagar cp)
        {

            var movcc = ObterMovCcPorIdTitulo(db, cp.Id, "D");

            var valor = 0.0;
            var juros = 0.0;
            var desc = 0.0;
            var saldo = 0.0;
            var idcc = 0;

            try
            {
                idcc = cp.IdCc.Value;
            }
            catch
            {
                idcc = -1;
            }

            var cc = servCC.ObterObjetoPorId(db, idcc);
            if (cc != null)
            {
                try
                {
                    saldo = cc.SaldoAtual.Value;
                    valor = cp.Valor.Value;
                    juros = cp.Juros.Value;
                    desc = cp.Desconto.Value;
                    valor = valor + juros - desc;
                    cc.SaldoAtual = saldo - valor;
                    cc.DtLanc = cp.DtPag.Value;
                }
                catch
                {

                }
            }

            if (movcc == null)
            {
                movcc = new MovCc
                {
                    IdTitulo = cp.Id,
                    DtMov = cp.DtPag.Value,
                    Sentido = "D",
                    IdCc = cc.Id,
                    Valor = valor,
                    SaldoAnt = saldo,
                    Historico = cp.Historico
                };
            }
            else
            {
                movcc.DtMov = cp.DtPag.Value;
                movcc.Sentido = "D";
                movcc.IdCc = cp.IdCc.Value;
                movcc.Valor = valor;
                movcc.SaldoAnt = saldo;
                movcc.Historico = cp.Historico;
            }

            //  Salvar(db, movcc);
        }


        public void AtualizarMovCcCr(ZenContext db, ContasReceber cr)
        {

            var movcc = ObterMovCcPorIdTitulo(db, cr.Id, "C");

            var valor = 0.0;
            var juros = 0.0;
            var desc = 0.0;
            var saldo = 0.0;
            var idcc = 0;

            try
            {
                idcc = cr.IdCc.Value;
            }
            catch
            {
                idcc = -1;
            }

            var cc = servCC.ObterObjetoPorId(db, idcc);
            if (cc != null)
            {
                try
                {
                    saldo = cc.SaldoAtual.Value;
                    valor = cr.Valor.Value;
                    juros = cr.Juros.Value;
                    desc = cr.Desconto.Value;
                    valor = valor + juros - desc;
                    cc.SaldoAtual = valor + saldo;
                    cc.DtLanc = cr.DtPag.Value;
                }
                catch
                {

                }
            }

            if (movcc == null)
            {
                movcc = new MovCc
                {
                    IdTitulo = cr.Id,
                    DtMov = cr.DtPag.Value,
                    Sentido = "C",
                    IdCc = cc.Id,
                    Valor = valor,
                    SaldoAnt = saldo,
                    Historico = cr.Historico
                };
            }
            else
            {
                movcc.DtMov = cr.DtPag.Value;
                movcc.Sentido = "C";
                movcc.IdCc = cr.IdCc.Value;
                movcc.Valor = valor;
                movcc.SaldoAnt = saldo;
                movcc.Historico = cr.Historico;
            }

          //  Salvar(db, movcc);
        }

        public MovCc ObterMovCcPorIdTitulo(ZenContext db, int idtitulo, string sentido)
        {
            return db.MovCcs.FirstOrDefault(c => c.IdTitulo == idtitulo && c.Sentido == sentido);
        }

        public IQueryable<MovCc> ObterListaObjetos(ZenContext db, DateTime dtMovIni, DateTime dtMovFim, string sentido)
        {
            var lst = new List<MovCc>();
            var saldoAtual = 0.0;
            var movcp = (from movcc in db.MovCcs
                         join cp in db.ContasPagar on movcc.IdTitulo equals cp.Id
                         into _cp
                         from cp in _cp.DefaultIfEmpty()
                         where (movcc.Sentido == "D")
                         join forn in db.Fornecedores on cp.IdFornecedor equals forn.Id
                         into _forn
                         from forn in _forn.DefaultIfEmpty()
                         join tr in db.SubDespesas on cp.IdSubDesp equals tr.Id
                         into _tr
                         from tr in _tr.DefaultIfEmpty()
                         join cc in db.ContasCorrentes on cp.IdCc equals cc.Id
                         into _cc
                         from cc in _cc.DefaultIfEmpty()
                         join st in db.Setores on cp.IdSetor equals st.Id
                         into _st
                         from st in _st.DefaultIfEmpty()
                         join fp in db.FormasPag on cp.IdFormaPag equals fp.Id
                         into _fp
                         from fp in _fp.DefaultIfEmpty()
                         join td in db.TiposDoc on cp.IdTipoDoc equals td.Id
                         into _td
                         from td in _td.DefaultIfEmpty()
                         select new { movcc, cp, forn, tr, cc, st, fp, td })
                    .Where(c => c.movcc.DtMov >= dtMovIni && c.movcc.DtMov <= dtMovFim);

            foreach (var item in movcp)
            {
                var cc = item.cc;
                var td = item.td;
                var fp = item.fp;
                var cp = item.cp;
                var nome = "";
                if (item.forn != null)
                    nome = item.forn.Nome;

                saldoAtual = item.movcc.SaldoAnt - item.movcc.Valor.Value;

                lst.Add(new MovCc
                {
                    ContaCorrente = cc,
                    ContaPagar = cp,
                    DtMov = item.movcc.DtMov,
                    Historico = item.movcc.Historico,
                    IdCc = item.movcc.IdCc,
                    IdLink = item.movcc.IdLink,
                    IdTitulo = item.movcc.IdTitulo,
                    NrSeq = item.movcc.NrSeq,
                    NumChq = item.movcc.NumChq,
                    SaldoAnt = item.movcc.SaldoAnt,
                    Sentido = item.movcc.Sentido,
                    TipoOper = item.movcc.TipoOper,
                    Valor = item.movcc.Valor,
                    Formapag = fp,
                    TipoDocumento = td,
                    FornecCliente = nome,
                    SaldoAtual = saldoAtual

                });
            }

            var movcr = (from movcc in db.MovCcs
                         join cp in db.ContasReceber on movcc.IdTitulo equals cp.Id
                         into _cp
                         from cp in _cp.DefaultIfEmpty()
                         where (movcc.Sentido == "C")
                         join forn in db.CLientes on cp.IdCliente equals forn.IdCliente
                         into _forn
                         from forn in _forn.DefaultIfEmpty()
                         join tr in db.TipoReceitas on cp.IdTipoReceita equals tr.Id
                         into _tr
                         from tr in _tr.DefaultIfEmpty()
                         join cc in db.ContasCorrentes on cp.IdCc equals cc.Id
                         into _cc
                         from cc in _cc.DefaultIfEmpty()
                         join st in db.Setores on cp.IdSetor equals st.Id
                         into _st
                         from st in _st.DefaultIfEmpty()
                         join fp in db.FormasPag on cp.IdFormaPag equals fp.Id
                         into _fp
                         from fp in _fp.DefaultIfEmpty()
                         join td in db.TiposDoc on cp.IdTipoDoc equals td.Id
                         into _td
                         from td in _td.DefaultIfEmpty()
                         select new { movcc, cp, forn, tr, cc, st, fp, td })
                         .Where(c => c.movcc.DtMov >= dtMovIni && c.movcc.DtMov <= dtMovFim);

            foreach (var item in movcr)
            {
                var cc = item.cc;
                var td = item.td;
                var fp = item.fp;
                var cp = item.cp;
                var nome = "";
                if (item.forn != null)
                    nome = item.forn.Nome;

                saldoAtual = item.movcc.SaldoAnt + item.movcc.Valor.Value;

                lst.Add(new MovCc
                {
                    ContaCorrente = cc,
                    ContaReceber = cp,
                    DtMov = item.movcc.DtMov,
                    Historico = item.movcc.Historico,
                    IdCc = item.movcc.IdCc,
                    IdLink = item.movcc.IdLink,
                    IdTitulo = item.movcc.IdTitulo,
                    NrSeq = item.movcc.NrSeq,
                    NumChq = item.movcc.NumChq,
                    SaldoAnt = item.movcc.SaldoAnt,
                    Sentido = item.movcc.Sentido,
                    TipoOper = item.movcc.TipoOper,
                    Valor = item.movcc.Valor,
                    Formapag = fp,
                    TipoDocumento = td,
                    FornecCliente = nome,
                    SaldoAtual = saldoAtual

                });
            }

            var lstsentido = new List<MovCc>();


            if (!string.IsNullOrEmpty(sentido) && (sentido != "T"))
            {
                lstsentido.AddRange(lst.Where(c => c.Sentido == sentido));
            }
            else
            {
                lstsentido.AddRange(lst);
            }

            return lstsentido.OrderBy(c => c.DtMov).AsQueryable();
        }
    }
}