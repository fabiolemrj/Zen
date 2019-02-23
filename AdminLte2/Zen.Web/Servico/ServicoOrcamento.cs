using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.Servico
{
    public class ServicoOrcamento
    {
        public Orcamento ObterObjetoPorId(ZenContext db, int id)
        {
            return db.Orcamentos.Find(id);
        }

        public void Salvar(ZenContext db, Orcamento objeto)
        {
            if (ObterObjetoPorId(db, objeto.IdPedido) == null)
            {

                objeto.IdPedido = db.Orcamentos.Max(c => c.IdPedido) + 1;
                db.Orcamentos.Add(objeto);
            }

            db.SaveChanges();
        }

        public void Delete(ZenContext db, Orcamento objeto)
        {
            db.Orcamentos.Remove(objeto);
            db.SaveChanges();
        }

        public IQueryable<Orcamento> ObterListaObjetosPedidos(ZenContext db, DateTime dtini, DateTime dtfim)
        {
            var lst = new List<Orcamento>();
            var dataini = DateTime.Now;
            var datafim = DateTime.Now;

            if (dtini != null)
            {
                dataini = dtini;
            }

            if (dtfim != null)
            {
                datafim = dtfim;
            }

            var q = (from orc in db.Orcamentos
                     join cl in db.CLientes on orc.IdCliente equals cl.IdCliente
                     into _cl
                     from cl in _cl.DefaultIfEmpty()
                     join rf in db.CLientes on orc.IdCliente equals rf.IdCliente
                     into _rf 
                     from rf in _rf.DefaultIfEmpty()
                     select new { orc, cl, rf }).Where(c => c.orc.DtPedido >= dataini && c.orc.DtPedido <= datafim);

            foreach (var item in q)
            {
                lst.Add(new Orcamento()
                {
                    IdPedido = item.orc.IdPedido,
                    Celular = item.orc.Celular,
                    Comissao = item.orc.Comissao,
                    Contato = item.orc.Contato,
                    DtAtual = item.orc.DtAtual,
                    DtPedido = item.orc.DtPedido,
                    Email1 = item.orc.Email1,
                    Email2 = item.orc.Email2,
                    Fax = item.orc.Fax,
                    FormaPag = item.orc.FormaPag,
                    IdCliente = item.orc.IdCliente,
                    IdOrcamento = item.orc.IdOrcamento,
                    IdReferencia = item.orc.IdReferencia,
                    IdUsuario = item.orc.IdUsuario,
                    IdUsuAtu = item.orc.IdUsuAtu,
                    Incompleto = item.orc.Incompleto,
                    Indicacao = item.orc.Indicacao,
                    ItensAprov = item.orc.ItensAprov,
                    ItensEnv = item.orc.ItensEnv,
                    NmReferencia = item.orc.NmReferencia,
                    NomeCliente = item.orc.NomeCliente,
                    NotifCel = item.orc.NotifCel,
                    NotifEmail = item.orc.NotifEmail,
                    NotifFax = item.orc.NotifFax,
                    NotiFOutro = item.orc.NotiFOutro,
                    NotifTel = item.orc.NotifTel,
                    Obs = item.orc.Obs,
                    Prazo = item.orc.Prazo,
                    Ramal1 = item.orc.Ramal1,
                    Ramal2 = item.orc.Ramal2,
                    RamalF = item.orc.RamalF,
                    SinalPerc = item.orc.SinalPerc,
                    Tel1 = item.orc.Tel1,
                    Tel2 = item.orc.Tel2,
                    Urgente = item.orc.Urgente,
                    Pendente = item.orc.Pendente,
                    Editando = item.orc.Editando,
                    ItensPend = item.orc.ItensPend,

                    Referencia = item.rf,
                    Cliente = item.cl
                });
            }

            return lst.AsQueryable();
        }
    }
}