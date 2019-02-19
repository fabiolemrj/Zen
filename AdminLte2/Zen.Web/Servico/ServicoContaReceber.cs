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

            if (string.IsNullOrEmpty(filtroNome))
            {
                var q = (from cr in db.ContasReceber
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
                         select new { cr, cl, tr, bc, cc, st, fp, td });

                foreach (var item in q)
                {
                    lst.Add(new ContasReceber
                    {
                        Id = item.cr.Id,
                        Desconto = item.cr.Desconto,
                        DtPag = item.cr.DtPag,
                        DtPagDesc = item.cr.DtPagDesc,
                        DtVenc = item.cr.DtVenc,
                        Estado = item.cr.Estado,
                        FlgConf = item.cr.FlgConf,
                        FlgTitDesc = item.cr.FlgTitDesc,
                        Historico = item.cr.Historico,
                        IdBancoCheque = item.cr.IdBancoCheque,
                        IdCc = item.cr.IdCc,
                        IdCheque = item.cr.IdCheque,
                        IdCliente = item.cr.IdCliente,
                        IdDoNoChq = item.cr.IdDoNoChq,
                        IdFormaPag = item.cr.IdFormaPag,
                        IdLink = item.cr.IdLink,
                        IdSetor = item.cr.IdSetor,
                        IdTipoDoc = item.cr.IdTipoDoc,
                        IdTipoReceita = item.cr.IdTipoReceita,
                        IdUsuario = item.cr.IdUsuario,
                        Juros = item.cr.Juros,
                        NumAgChq = item.cr.NumAgChq,
                        NumChq = item.cr.NumChq,
                        NumOsi = item.cr.NumOsi,
                        NumParc = item.cr.NumParc,
                        Obs = item.cr.Obs,
                        Valor = item.cr.Valor,
                        Cliente = item.cl,
                        TipoReceita = item.tr,
                        BancoCheque = item.bc,
                        Setor = item.st,
                        ContaCorrente = item.cc,
                        FormaPag = item.fp,
                        TipoDoc = item.td
                    });
                }
            }
            else
            {
                switch (tpfiltro)
                {
                    //historico
                    case 1:
                        {

                            var q = (from cr in db.ContasReceber
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
                                     select new { cr, cl, tr, bc, cc, st, fp, td }).Where(b => b.cr.Historico.Contains(filtroNome)).OrderByDescending(b => b.cr.DtVenc);

                            foreach (var item in q)
                            {
                                lst.Add(new ContasReceber
                                {
                                    Id = item.cr.Id,
                                    Desconto = item.cr.Desconto,
                                    DtPag = item.cr.DtPag,
                                    DtPagDesc = item.cr.DtPagDesc,
                                    DtVenc = item.cr.DtVenc,
                                    Estado = item.cr.Estado,
                                    FlgConf = item.cr.FlgConf,
                                    FlgTitDesc = item.cr.FlgTitDesc,
                                    Historico = item.cr.Historico,
                                    IdBancoCheque = item.cr.IdBancoCheque,
                                    IdCc = item.cr.IdCc,
                                    IdCheque = item.cr.IdCheque,
                                    IdCliente = item.cr.IdCliente,
                                    IdDoNoChq = item.cr.IdDoNoChq,
                                    IdFormaPag = item.cr.IdFormaPag,
                                    IdLink = item.cr.IdLink,
                                    IdSetor = item.cr.IdSetor,
                                    IdTipoDoc = item.cr.IdTipoDoc,
                                    IdTipoReceita = item.cr.IdTipoReceita,
                                    IdUsuario = item.cr.IdUsuario,
                                    Juros = item.cr.Juros,
                                    NumAgChq = item.cr.NumAgChq,
                                    NumChq = item.cr.NumChq,
                                    NumOsi = item.cr.NumOsi,
                                    NumParc = item.cr.NumParc,
                                    Obs = item.cr.Obs,
                                    Valor = item.cr.Valor,
                                    Cliente = item.cl,
                                    TipoReceita = item.tr,
                                    BancoCheque = item.bc,
                                    Setor = item.st,
                                    ContaCorrente = item.cc,
                                    FormaPag = item.fp,
                                    TipoDoc = item.td
                                });
                            }

                            break;
                        }
                    case 2:
                        {
                            var q = (from cr in db.ContasReceber
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
                                     select new { cr, cl, tr, bc, cc, st, fp, td }).Where(c => c.bc.Nome.Contains(filtroNome)).OrderByDescending(b => b.cr.DtVenc);

                            foreach (var item in q)
                            {
                                lst.Add(new ContasReceber
                                {
                                    Id = item.cr.Id,
                                    Desconto = item.cr.Desconto,
                                    DtPag = item.cr.DtPag,
                                    DtPagDesc = item.cr.DtPagDesc,
                                    DtVenc = item.cr.DtVenc,
                                    Estado = item.cr.Estado,
                                    FlgConf = item.cr.FlgConf,
                                    FlgTitDesc = item.cr.FlgTitDesc,
                                    Historico = item.cr.Historico,
                                    IdBancoCheque = item.cr.IdBancoCheque,
                                    IdCc = item.cr.IdCc,
                                    IdCheque = item.cr.IdCheque,
                                    IdCliente = item.cr.IdCliente,
                                    IdDoNoChq = item.cr.IdDoNoChq,
                                    IdFormaPag = item.cr.IdFormaPag,
                                    IdLink = item.cr.IdLink,
                                    IdSetor = item.cr.IdSetor,
                                    IdTipoDoc = item.cr.IdTipoDoc,
                                    IdTipoReceita = item.cr.IdTipoReceita,
                                    IdUsuario = item.cr.IdUsuario,
                                    Juros = item.cr.Juros,
                                    NumAgChq = item.cr.NumAgChq,
                                    NumChq = item.cr.NumChq,
                                    NumOsi = item.cr.NumOsi,
                                    NumParc = item.cr.NumParc,
                                    Obs = item.cr.Obs,
                                    Valor = item.cr.Valor,
                                    Cliente = item.cl,
                                    TipoReceita = item.tr,
                                    BancoCheque = item.bc,
                                    Setor = item.st,
                                    ContaCorrente = item.cc,
                                    FormaPag = item.fp,
                                    TipoDoc = item.td
                                });
                            }

                        }

                        break;

                    case 3:
                        {
                            var q = (from cr in db.ContasReceber
                                     join bc in db.Bancos on cr.IdBancoCheque.ToString() equals bc.IdBanco
                                     into _bc
                                     from bc in _bc.DefaultIfEmpty()
                                     join cl in db.CLientes on cr.IdCc equals cl.IdCliente
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
                                     select new { cr, cl, tr, bc, cc, st, fp, td }).OrderByDescending(b => b.cr.DtVenc);
                            foreach (var item in q)
                            {
                                lst.Add(new ContasReceber
                                {
                                    Id = item.cr.Id,
                                    Desconto = item.cr.Desconto,
                                    DtPag = item.cr.DtPag,
                                    DtPagDesc = item.cr.DtPagDesc,
                                    DtVenc = item.cr.DtVenc,
                                    Estado = item.cr.Estado,
                                    FlgConf = item.cr.FlgConf,
                                    FlgTitDesc = item.cr.FlgTitDesc,
                                    Historico = item.cr.Historico,
                                    IdBancoCheque = item.cr.IdBancoCheque,
                                    IdCc = item.cr.IdCc,
                                    IdCheque = item.cr.IdCheque,
                                    IdCliente = item.cr.IdCliente,
                                    IdDoNoChq = item.cr.IdDoNoChq,
                                    IdFormaPag = item.cr.IdFormaPag,
                                    IdLink = item.cr.IdLink,
                                    IdSetor = item.cr.IdSetor,
                                    IdTipoDoc = item.cr.IdTipoDoc,
                                    IdTipoReceita = item.cr.IdTipoReceita,
                                    IdUsuario = item.cr.IdUsuario,
                                    Juros = item.cr.Juros,
                                    NumAgChq = item.cr.NumAgChq,
                                    NumChq = item.cr.NumChq,
                                    NumOsi = item.cr.NumOsi,
                                    NumParc = item.cr.NumParc,
                                    Obs = item.cr.Obs,
                                    Valor = item.cr.Valor,
                                    Cliente = item.cl,
                                    TipoReceita = item.tr,
                                    BancoCheque = item.bc,
                                    Setor = item.st,
                                    ContaCorrente = item.cc,
                                    FormaPag = item.fp,
                                    TipoDoc = item.td
                                });
                            }
                            break;
                        }
                }
            }


            return lst.AsQueryable();
        }

        public void Salvar(ZenContext db, ContasReceber objeto)
        {
            if (ObterObjetoPorId(db, objeto.Id) == null)
            {
                ServicoCntr_CprCr serv_cntrCpCr = new ServicoCntr_CprCr();
                var cntr_cpcr = serv_cntrCpCr.ObterObjetoPorId(db, 0);
                cntr_cpcr.IdCr++;
                objeto.Id = cntr_cpcr.IdCr;
                //objeto.Id = db.ContasReceber.Max(c => c.Id) + 1;
                db.ContasReceber.Add(objeto);
            }

            if (objeto.Estado == "P")
            {
                ServicoMovCc servmovcc = new ServicoMovCc();
                servmovcc.AtualizarMovCcCr(db, objeto);
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