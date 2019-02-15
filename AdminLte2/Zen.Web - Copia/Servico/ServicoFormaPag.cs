using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.Servico
{
    public class ServicoFormaPag
    {
        public FormaPag ObterObjetoPorId(ZenContext db, int id)
        {
            return db.FormasPag.Find(id);
        }

        public IQueryable<FormaPag> ObterListaObjetos(ZenContext db, string filtroNome)
        {
            if (string.IsNullOrWhiteSpace(filtroNome))
            {
                return db.FormasPag.OrderBy(u => u.Nome);
            }
            else
            {
                return db.FormasPag.Where(u => u.Nome.Contains(filtroNome)).OrderBy(u => u.Nome);
            }
        }

        public void Salvar(ZenContext db, FormaPag formapag)
        {
            if (ObterObjetoPorId(db, formapag.Id) == null)
            {
                db.FormasPag.Add(formapag);
            }

            db.SaveChanges();
        }

        public void Delete(ZenContext db, FormaPag formapag)
        {
            db.FormasPag.Remove(formapag);
            db.SaveChanges();
        }

    }
}