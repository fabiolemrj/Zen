using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.Servico
{
    public class ServicoSetor
    {
        public Setor ObterObjetoPorId(ZenContext db, int id)
        {
            return db.Setores.Find(id);
        }

        public IQueryable<Setor> ObterListaObjetos(ZenContext db, string filtroNome)
        {
            if (string.IsNullOrWhiteSpace(filtroNome))
            {
                return db.Setores.OrderBy(u => u.Nome);
            }
            else
            {
                return db.Setores.Where(u => u.Nome.Contains(filtroNome)).OrderBy(u => u.Nome);
            }
        }

        public void Salvar(ZenContext db, Setor setor)
        {
            if (ObterObjetoPorId(db, setor.Id) == null)
            {
                //banco.IdBanco = db.Bancos.Max(c => c.IdBanco) + 1;
                db.Setores.Add(setor);
            }

            db.SaveChanges();
        }

        public void Delete(ZenContext db, Setor setor)
        {
            db.Setores.Remove(setor);
            db.SaveChanges();
        }
    }
}