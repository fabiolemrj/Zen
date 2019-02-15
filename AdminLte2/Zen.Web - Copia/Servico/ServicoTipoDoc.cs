using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.Servico
{
    public class ServicoTipoDoc
    {
        
        public TipoDoc ObterObjetoPorId(ZenContext db, int id)
        {
            return db.TiposDoc.Find(id);
        }

        public IQueryable<TipoDoc> ObterListaObjetos(ZenContext db, string filtroNome)
        {
            if (string.IsNullOrWhiteSpace(filtroNome))
            {
                return db.TiposDoc.OrderBy(u => u.Nome);
            }
            else
            {
                return db.TiposDoc.Where(u => u.Nome.Contains(filtroNome)).OrderBy(u => u.Nome);
            }
        }

        public void Salvar(ZenContext db, TipoDoc objeto)
        {
            if (ObterObjetoPorId(db, objeto.Id) == null)
            {
                db.TiposDoc.Add(objeto);
            }

            db.SaveChanges();
        }

        public void Delete(ZenContext db, TipoDoc objeto)
        {
            db.TiposDoc.Remove(objeto);
            db.SaveChanges();
        }
    }
}