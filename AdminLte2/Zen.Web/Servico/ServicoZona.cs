using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.Servico
{
    public class ServicoZona
    {
        public Zona ObterObjetoPorId(ZenContext db, int id)
        {
            return db.Zonas.Find(id);
        }
               
        public IQueryable<Zona> ObterListaObjetos(ZenContext db, string filtroNome)
        {
            if (string.IsNullOrWhiteSpace(filtroNome))
            {
                return db.Zonas.OrderBy(u => u.Nome);
            }
            else
            {
                return db.Zonas.Where(u => u.Nome.Contains(filtroNome)).OrderBy(u => u.Nome);
            }
        }

        public void Salvar(ZenContext db, Zona objeto)
        {
            if (ObterObjetoPorId(db, objeto.Id) == null)
            {
                objeto.Prioridade = db.Zonas.Max(c => c.Prioridade) + 1;
                db.Zonas.Add(objeto);
            }

            db.SaveChanges();
        }

        public void Delete(ZenContext db, Zona objeto)
        {
            db.Zonas.Remove(objeto);
            db.SaveChanges();
        }
    }
}