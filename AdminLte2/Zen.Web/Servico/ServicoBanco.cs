using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.Servico
{
    public class ServicoBanco
    {
        public Banco ObterObjetoPorId(ZenContext db, string id)
        {
            return db.Bancos.Find(id);
        }

        public IQueryable<Banco> ObterListaObjetos(ZenContext db, string filtroNome)
        {
            if (string.IsNullOrWhiteSpace(filtroNome))
            {
                return db.Bancos.OrderBy(u => u.Nome);
            }
            else
            {
                return db.Bancos.Where(u => u.Nome.Contains(filtroNome)).OrderBy(u => u.Nome);
            }
        }

        public void Salvar(ZenContext db, Banco banco)
        {
            if (ObterObjetoPorId(db, banco.IdBanco) == null)
            {
                //banco.IdBanco = db.Bancos.Max(c => c.IdBanco) + 1;
                db.Bancos.Add(banco);
            }

            db.SaveChanges();
        }

        public void Delete(ZenContext db, Banco banco)
        {
            db.Bancos.Remove(banco);
            db.SaveChanges();
        }
    }
}