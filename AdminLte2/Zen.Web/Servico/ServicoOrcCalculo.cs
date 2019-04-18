using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.Servico
{
    public class ServicoOrcCalculo
    {
        public OrcCalculo ObterObjetoPorId(ZenContext db, int idpedido, int item)
        {
           
                return db.OrcCalculos.FirstOrDefault(c => c.IdPedido == idpedido && c.Item == item);
        
        }

        public void Salvar(ZenContext db, OrcCalculo objeto)
        {
            if (ObterObjetoPorId(db, objeto.IdPedido, objeto.Item) == null)
            {
            //    objeto.Item = db.OrcCalculos.Max(c => c.Item) + 1;
                db.OrcCalculos.Add(objeto);
            }

            db.SaveChanges();
        }

        public void Delete(ZenContext db, OrcCalculo objeto)
        {
            db.OrcCalculos.Remove(objeto);
            db.SaveChanges();
        }

        public IQueryable<OrcCalculo> ObterListaObjetos(ZenContext db, Orcamento orcamento)
        {
            return db.OrcCalculos.Where(u => u.IdPedido == orcamento.IdPedido).OrderBy(u => u.Item);
        }


        public IQueryable<OrcCalculo> ObterListaObjetos(ZenContext db, int idpedido)
        {
            return db.OrcCalculos.Where(u => u.IdPedido == idpedido).OrderBy(u => u.Item);
        }
    }
}