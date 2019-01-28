using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.Servico
{
    public class ServicoProduto
    {
        public Produto ObterObjetoPorId(ZenContext db, int id)
        {
            return db.Produtos.Find(id);
        }

        public IQueryable<Produto> ObterListaObjetos(ZenContext db, string filtroNome)
        {
            if (string.IsNullOrWhiteSpace(filtroNome))
            {
                return db.Produtos.OrderBy(u => u.Nome);
            }
            else
            {
                return db.Produtos.Where(u => u.Nome.Contains(filtroNome)).OrderBy(u => u.Nome);
            }
        }

        public void Salvar(ZenContext db, Produto objeto)
        {
            if (ObterObjetoPorId(db, objeto.Id) == null)
            {
                objeto.Id = db.Produtos.Max(c => c.Id) + 1;
                db.Produtos.Add(objeto);
            }

            db.SaveChanges();
        }

        public void Delete(ZenContext db, Produto objeto)
        {
            db.Produtos.Remove(objeto);
            db.SaveChanges();
        }
    }
}