using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.Servico
{
    public class ServicoOrcCliches
    {
        public OrcCliches ObterObjetoPorId(ZenContext db, int idpedido, int item, int nrseq)
        {
            return db.OrcCliches.FirstOrDefault(c => c.IdPedido == idpedido && c.Item == item && c.NrSeq == nrseq);
        }


        public void Salvar(ZenContext db, OrcCliches objeto)
        {
            if (ObterObjetoPorId(db, objeto.IdPedido, objeto.Item, objeto.NrSeq) == null)
            {
                objeto.NrSeq = db.OrcAreas.Max(c => c.NrSeq) + 1;
                db.OrcCliches.Add(objeto);
            }
            db.SaveChanges();
        }
        

        public IQueryable<OrcCliches> ObterListaObjetos(ZenContext db, OrcamentoDet orcamentodet)
        {
            return db.OrcCliches.Where(u => u.IdPedido == orcamentodet.IdPedido && u.Item == orcamentodet.Item).OrderBy(u => u.NrSeq);
        }

        public IQueryable<OrcCliches> ObterListaObjetos(ZenContext db, int idpedido, int item)
        {
            return db.OrcCliches.Where(u => u.IdPedido == idpedido && u.Item == item).OrderBy(u => u.NrSeq);
        }

    }
}