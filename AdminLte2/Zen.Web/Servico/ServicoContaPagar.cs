using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.Servico
{
    public class ServicoContaPagar
    {
        ServicoBanco servBanco = new ServicoBanco();
        ServicoContaCorrente servCC = new ServicoContaCorrente();
        ServicoFormaPag servFormapag = new ServicoFormaPag();
        ServicoSetor servSetor = new ServicoSetor();
        ServicoSubDespesa servSubDesp = new ServicoSubDespesa();
        ServicoDespesa servDesp = new ServicoDespesa();
        ServicoFornecedor servFornec = new ServicoFornecedor();
        ServicoTipoDoc servTpdoc = new ServicoTipoDoc();

        public ContaPagar ObterObjetoPorId(ZenContext db, int id)
        {
            return db.ContasPagar.Find(id);
        }

        public void Salvar(ZenContext db, ContaPagar objeto)
        {
            if (ObterObjetoPorId(db, objeto.Id) == null)
            {
                ServicoCntr_CprCr serv_cntrCpCr = new ServicoCntr_CprCr();
                var cntr_cpcr = serv_cntrCpCr.ObterObjetoPorId(db,0);
                cntr_cpcr.IdCp++; 
                objeto.Id = cntr_cpcr.IdCp;
                db.ContasPagar.Add(objeto);
            }

            if (objeto.Estado == "P")
            {
                ServicoMovCc servmovcc = new ServicoMovCc();
                servmovcc.AtualizarMovCcCr(db, objeto);
            }

            db.SaveChanges();
        }

        public void Delete(ZenContext db, ContaPagar objeto)
        {
            db.ContasPagar.Remove(objeto);
            db.SaveChanges();
        }

        public IQueryable<ContaPagar> ObterListaObjetos(ZenContext db, string filtroNome, int tpfiltro)
        {
            var lst = new List<ContaPagar>();

            if (string.IsNullOrEmpty(filtroNome))
            {
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
                         select new { cr, cl, tr, bc, cc, st, fp, td });

                foreach (var item in q)
                {
                    lst.Add(new ContaPagar
                    {
                        Id = item.cr.Id,
                        Desconto = item.cr.Desconto,
                        DtPag = item.cr.DtPag,                        
                        DtVenc = item.cr.DtVenc,
                        Estado = item.cr.Estado,
                        FlgConf = item.cr.FlgConf,
                        Historico = item.cr.Historico,
                        IdBanco = item.cr.IdBanco,
                        IdCc = item.cr.IdCc,                        
                        IdFornecedor = item.cr.IdFornecedor,
                        IdFormaPag = item.cr.IdFormaPag,
                        IdLink = item.cr.IdLink,
                        IdSetor = item.cr.IdSetor,
                        IdTipoDoc = item.cr.IdTipoDoc,
                        IdSubDesp = item.cr.IdSubDesp,
                        IdUsuario = item.cr.IdUsuario,
                        Juros = item.cr.Juros,
                        IdDesp = item.cr.IdDesp,
                        NumChq = item.cr.NumChq,
                        Obs = item.cr.Obs,
                        Valor = item.cr.Valor,
                        Fornecedor = item.cl,
                        SubDespesa = item.tr,
                        Banco = item.bc,
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
                                     select new { cr, cl, tr, bc, cc, st, fp, td }).Where(b => b.cr.Historico.Contains(filtroNome)).OrderByDescending(b => b.cr.DtVenc);

                            foreach (var item in q)
                            {
                                lst.Add(new ContaPagar
                                {
                                    Id = item.cr.Id,
                                    Desconto = item.cr.Desconto,
                                    DtPag = item.cr.DtPag,
                                    DtVenc = item.cr.DtVenc,
                                    Estado = item.cr.Estado,
                                    FlgConf = item.cr.FlgConf,
                                    Historico = item.cr.Historico,
                                    IdBanco = item.cr.IdBanco,
                                    IdCc = item.cr.IdCc,
                                    IdFornecedor = item.cr.IdFornecedor,
                                    IdFormaPag = item.cr.IdFormaPag,
                                    IdLink = item.cr.IdLink,
                                    IdSetor = item.cr.IdSetor,
                                    IdTipoDoc = item.cr.IdTipoDoc,
                                    IdSubDesp = item.cr.IdSubDesp,
                                    IdUsuario = item.cr.IdUsuario,
                                    Juros = item.cr.Juros,
                                    IdDesp = item.cr.IdDesp,
                                    NumChq = item.cr.NumChq,
                                    Obs = item.cr.Obs,
                                    Valor = item.cr.Valor,
                                    Fornecedor = item.cl,
                                    SubDespesa = item.tr,
                                    Banco = item.bc,
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
                                     select new { cr, cl, tr, bc, cc, st, fp, td }).Where(c => c.bc.Nome.Contains(filtroNome)).OrderByDescending(b => b.cr.DtVenc);

                            foreach (var item in q)
                            {
                                lst.Add(new ContaPagar
                                {
                                    Id = item.cr.Id,
                                    Desconto = item.cr.Desconto,
                                    DtPag = item.cr.DtPag,
                                    DtVenc = item.cr.DtVenc,
                                    Estado = item.cr.Estado,
                                    FlgConf = item.cr.FlgConf,
                                    Historico = item.cr.Historico,
                                    IdBanco = item.cr.IdBanco,
                                    IdCc = item.cr.IdCc,
                                    IdFornecedor = item.cr.IdFornecedor,
                                    IdFormaPag = item.cr.IdFormaPag,
                                    IdLink = item.cr.IdLink,
                                    IdSetor = item.cr.IdSetor,
                                    IdTipoDoc = item.cr.IdTipoDoc,
                                    IdSubDesp = item.cr.IdSubDesp,
                                    IdUsuario = item.cr.IdUsuario,
                                    Juros = item.cr.Juros,
                                    IdDesp = item.cr.IdDesp,
                                    NumChq = item.cr.NumChq,
                                    Obs = item.cr.Obs,
                                    Valor = item.cr.Valor,
                                    Fornecedor = item.cl,
                                    SubDespesa = item.tr,
                                    Banco = item.bc,
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
                                     select new { cr, cl, tr, bc, cc, st, fp, td }).OrderByDescending(b => b.cr.DtVenc);
                            foreach (var item in q)
                            {
                                lst.Add(new ContaPagar
                                {
                                    Id = item.cr.Id,
                                    Desconto = item.cr.Desconto,
                                    DtPag = item.cr.DtPag,
                                    DtVenc = item.cr.DtVenc,
                                    Estado = item.cr.Estado,
                                    FlgConf = item.cr.FlgConf,
                                    Historico = item.cr.Historico,
                                    IdBanco = item.cr.IdBanco,
                                    IdCc = item.cr.IdCc,
                                    IdFornecedor = item.cr.IdFornecedor,
                                    IdFormaPag = item.cr.IdFormaPag,
                                    IdLink = item.cr.IdLink,
                                    IdSetor = item.cr.IdSetor,
                                    IdTipoDoc = item.cr.IdTipoDoc,
                                    IdSubDesp = item.cr.IdSubDesp,
                                    IdUsuario = item.cr.IdUsuario,
                                    Juros = item.cr.Juros,
                                    IdDesp = item.cr.IdDesp,
                                    NumChq = item.cr.NumChq,
                                    Obs = item.cr.Obs,
                                    Valor = item.cr.Valor,
                                    Fornecedor = item.cl,
                                    SubDespesa = item.tr,
                                    Banco = item.bc,
                                    Setor = item.st,
                                    ContaCorrente = item.cc,
                                    FormaPag = item.fp,
                                    TipoDoc = item.td
                                });
                            }
                            break;
                        }
                    case 4:
                        {
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
                                     select new { cr, cl, tr, bc, cc, st, fp, td }).Where(c => c.cl.Nome.Contains(filtroNome)).OrderByDescending(b => b.cr.DtVenc); ;
                            foreach (var item in q)
                            {
                                lst.Add(new ContaPagar
                                {
                                    Id = item.cr.Id,
                                    Desconto = item.cr.Desconto,
                                    DtPag = item.cr.DtPag,
                                    DtVenc = item.cr.DtVenc,
                                    Estado = item.cr.Estado,
                                    FlgConf = item.cr.FlgConf,
                                    Historico = item.cr.Historico,
                                    IdBanco = item.cr.IdBanco,
                                    IdCc = item.cr.IdCc,
                                    IdFornecedor = item.cr.IdFornecedor,
                                    IdFormaPag = item.cr.IdFormaPag,
                                    IdLink = item.cr.IdLink,
                                    IdSetor = item.cr.IdSetor,
                                    IdTipoDoc = item.cr.IdTipoDoc,
                                    IdSubDesp = item.cr.IdSubDesp,
                                    IdUsuario = item.cr.IdUsuario,
                                    Juros = item.cr.Juros,
                                    IdDesp = item.cr.IdDesp,
                                    NumChq = item.cr.NumChq,
                                    Obs = item.cr.Obs,
                                    Valor = item.cr.Valor,
                                    Fornecedor = item.cl,
                                    SubDespesa = item.tr,
                                    Banco = item.bc,
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

    }
}