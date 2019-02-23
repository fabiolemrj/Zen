using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.Servico
{
    public class ServicoOrcamentoDet
    {
        public OrcamentoDet ObterObjetoPorId(ZenContext db, int idpedido, int item)
        {
            return db.OrcamentoDets.FirstOrDefault(c=>c.IdPedido== idpedido && c.Item == item);
        }

        public void Salvar(ZenContext db, OrcamentoDet objeto)
        {
            if (ObterObjetoPorId(db, objeto.IdPedido, objeto.Item) == null)
            {
                objeto.Item = db.OrcamentoDets.Max(c => c.Item) + 1;
                db.OrcamentoDets.Add(objeto);
            }

            db.SaveChanges();
        }

        public void Delete(ZenContext db, OrcamentoDet objeto)
        {
            db.OrcamentoDets.Remove(objeto);
            db.SaveChanges();
        }

        public IQueryable<OrcamentoDet> ObterListaObjetos(ZenContext db, Orcamento orcamento)
        {
            return db.OrcamentoDets.Where(u => u.IdPedido == orcamento.IdPedido).OrderBy(u => u.Item);
        }


        public IQueryable<OrcamentoDet> ObterListaObjetos(ZenContext db, int idpedido)
        {
            return db.OrcamentoDets.Where(u => u.IdPedido == idpedido).OrderBy(u => u.Item);
        }


    }
}