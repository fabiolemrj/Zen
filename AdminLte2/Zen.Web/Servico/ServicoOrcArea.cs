using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.Servico
{
    public class ServicoOrcArea
    {
        public OrcAreas ObterObjetoPorId(ZenContext db, int idpedido, int item, int nrseq)
        {
            return db.OrcAreas.FirstOrDefault(c => c.IdPedido == idpedido && c.Item == item && c.NrSeq == nrseq);
        }

        public void Salvar(ZenContext db, OrcAreas objeto)
        {
            if (ObterObjetoPorId(db, objeto.IdPedido, objeto.Item, objeto.NrSeq) == null)
            {
                objeto.NrSeq = db.OrcAreas.Where(c=>c.IdPedido == objeto.IdPedido && c.Item == objeto.Item).Max(c => c.NrSeq) + 1;
                db.OrcAreas.Add(objeto);
            }
            db.SaveChanges();
        }

        public IQueryable<OrcAreas> ObterListaObjetos(ZenContext db, OrcamentoDet orcamentodet)
        {
            return db.OrcAreas.Where(u => u.IdPedido == orcamentodet.IdPedido && u.Item == orcamentodet.Item).OrderBy(u => u.NrSeq);
        }

        public IQueryable<OrcAreas> ObterListaObjetos(ZenContext db, int idpedido, int item)
        {
            return db.OrcAreas.Where(u => u.IdPedido == idpedido && u.Item == item).OrderBy(u => u.NrSeq);
        }
    }
}