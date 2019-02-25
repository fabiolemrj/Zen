using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.Servico
{
    public class ServicoOrcFotolitos
    {
        public OrcFotolitos ObterObjetoPorId(ZenContext db, int idpedido, int item, int nrseq)
        {
            return db.OrcFotolitos.FirstOrDefault(c => c.IdPedido == idpedido && c.Item == item && c.NrSeq == nrseq);
        }

        public void Salvar(ZenContext db, OrcFotolitos objeto)
        {
            if (ObterObjetoPorId(db, objeto.IdPedido, objeto.Item, objeto.NrSeq) == null)
            {
                objeto.NrSeq = db.OrcAreas.Max(c => c.NrSeq) + 1;
                db.OrcFotolitos.Add(objeto);
            }
            db.SaveChanges();
        }

        public IQueryable<OrcFotolitos> ObterListaObjetos(ZenContext db, OrcamentoDet orcamentodet)
        {
            return db.OrcFotolitos.Where(u => u.IdPedido == orcamentodet.IdPedido && u.Item == orcamentodet.Item).OrderBy(u => u.NrSeq);
        }

        public IQueryable<OrcFotolitos> ObterListaObjetos(ZenContext db, int idpedido, int item)
        {
            return db.OrcFotolitos.Where(u => u.IdPedido == idpedido && u.Item == item).OrderBy(u => u.NrSeq);
        }
    }
}