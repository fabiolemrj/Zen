using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.Servico
{
    public class ServicoTransporte
    {
        public Transporte ObterObjetoPorId(ZenContext db, int id)
        {
            return db.Transportes.Find(id);
        }

        public IQueryable<Transporte> ObterListaObjetos(ZenContext db, string filtroNome)
        {
            if (string.IsNullOrWhiteSpace(filtroNome))
            {
                return db.Transportes.OrderBy(u => u.Nome);
            }
            else
            {
                return db.Transportes.Where(u => u.Nome.Contains(filtroNome)).OrderBy(u => u.Nome);
            }
        }

        public void Salvar(ZenContext db, Transporte objeto)
        {
            if (ObterObjetoPorId(db, objeto.Id) == null)
            {
                db.Transportes.Add(objeto);
            }

            db.SaveChanges();
        }

        public void Delete(ZenContext db, Transporte objeto)
        {
            db.Transportes.Remove(objeto);
            db.SaveChanges();
        }
    }
}