using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.Servico
{
    public class ServicoTipoTinta
    {
        public TipoTinta ObterObjetoPorId(ZenContext db, int id)
        {
            return db.TipoTintas.Find(id);
        }

        public IQueryable<TipoTinta> ObterListaObjetos(ZenContext db, string filtroNome)
        {
            if (string.IsNullOrWhiteSpace(filtroNome))
            {
                return db.TipoTintas.OrderBy(u => u.Nome);
            }
            else
            {
                return db.TipoTintas.Where(u => u.Nome.Contains(filtroNome)).OrderBy(u => u.Nome);
            }
        }

        public void Salvar(ZenContext db, TipoTinta objeto)
        {
            if (ObterObjetoPorId(db, objeto.Id) == null)
            {
                objeto.Id = db.TipoTintas.Max(c => c.Id) + 1;
                db.TipoTintas.Add(objeto);
            }

            db.SaveChanges();
        }

        public void Delete(ZenContext db, TipoTinta objeto)
        {
            db.TipoTintas.Remove(objeto);
            db.SaveChanges();
        }

    }
}