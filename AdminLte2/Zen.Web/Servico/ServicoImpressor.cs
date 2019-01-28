using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.Servico
{
    public class ServicoImpressor
    {
        public Impressor ObterObjetoPorId(ZenContext db, int id)
        {
            return db.Impressores.Find(id);
        }

        public IQueryable<Impressor> ObterListaObjetos(ZenContext db, string filtroNome, string Ativo = "" )
        {
            IQueryable<Impressor> lst;
            if (string.IsNullOrWhiteSpace(filtroNome))
            {                
                lst = db.Impressores.OrderBy(u => u.Nome);
            }
            else
            {
                lst = db.Impressores.Where(u => u.Nome.Contains(filtroNome)).OrderBy(u => u.Nome);
            }

            if (!string.IsNullOrEmpty(Ativo))
            {
                lst = lst.Where(u => u.Ativo == Ativo);
            }

            return lst;
        }
        
        public void Salvar(ZenContext db, Impressor impressor)
        {
            if (ObterObjetoPorId(db, impressor.Id) == null)
            {
                impressor.Id = db.Impressores.Max(c => c.Id) + 1;
                db.Impressores.Add(impressor);
            }

            db.SaveChanges();
        }

        public void Delete(ZenContext db, Impressor impressor)
        {
            db.Impressores.Remove(impressor);
            db.SaveChanges();
        }

    }
}