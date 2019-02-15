using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.Servico
{
    public class ServicoCor
    {
        public Cor ObterObjetoPorId(ZenContext db, int id)
        {
            return db.Cores.Find(id);
        }

        public IQueryable<Cor> ObterListaObjetos(ZenContext db, string filtroNome)
        {
            if (string.IsNullOrWhiteSpace(filtroNome))
            {
                return db.Cores.OrderBy(u => u.Nome);
            }
            else
            {
                return db.Cores.Where(u => u.Nome.Contains(filtroNome)).OrderBy(u => u.Nome);
            }
        }

        public void Salvar(ZenContext db, Cor cor)
        {
            if (ObterObjetoPorId(db, cor.IdCor) == null)
            {
                cor.IdCor = db.Cores.Max(c => c.IdCor) + 1;
                db.Cores.Add(cor);
            }

            db.SaveChanges();
        }

        public void Delete(ZenContext db, Cor obj)
        {
            var cor = ObterObjetoPorId(db,obj.IdCor); 
            db.Cores.Remove(cor);
            db.SaveChanges();
        }
    }
}