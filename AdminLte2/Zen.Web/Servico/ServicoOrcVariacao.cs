using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.Servico
{
    public class ServicoOrcVariacao
    {
        public OrcVariacao ObterObjetoPorId(ZenContext db, int idpedido, int item, int nrseq)
        {
            return db.OrcVariacao.FirstOrDefault(c => c.IdPedido == idpedido && c.Item == item && c.NrSeq == nrseq);
        }

        public void Salvar(ZenContext db, OrcVariacao objeto)
        {
            if (ObterObjetoPorId(db, objeto.IdPedido, objeto.Item, objeto.NrSeq) == null)
            {
                objeto.NrSeq = db.OrcVariacao.Max(c => c.NrSeq) + 1;
                db.OrcVariacao.Add(objeto);
            }
            db.SaveChanges();
        }

        public void Delete(ZenContext db, OrcVariacao objeto)
        {
            db.OrcVariacao.Remove(objeto);
            db.SaveChanges();
        }

        public IQueryable<OrcVariacao> ObterListaObjetos(ZenContext db, OrcamentoDet orcamentodet)
        {
            return db.OrcVariacao.Where(u => u.IdPedido == orcamentodet.IdPedido && u.Item == orcamentodet.Item).OrderBy(u => u.NrSeq);
        }

        public IQueryable<OrcVariacao> ObterListaObjetos(ZenContext db, int idpedido, int item)
        {
            return db.OrcVariacao.Where(u => u.IdPedido == idpedido && u.Item == item).OrderBy(u => u.NrSeq);
        }
    }
}