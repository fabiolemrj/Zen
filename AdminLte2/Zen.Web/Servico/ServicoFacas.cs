using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zen.Web.Models;

namespace Zen.Web.Servico
{
    public class ServicoFacas
    {
        public Facas ObterObjetoPorId(ZenContext db, int id)
        {
            return db.Facas.Find(id);
        }

        public IQueryable<Facas> ObterListaObjetos(ZenContext db, string filtroNome)
        {
            if (string.IsNullOrWhiteSpace(filtroNome))
            {
                return db.Facas.OrderBy(u => u.Cod_Faca);
            }
            else
            {
                return db.Facas.Where(u => u.Cod_Faca.Contains(filtroNome)).OrderBy(u => u.Cod_Faca);
            }
        }

        public void Salvar(ZenContext db, Facas faca)
        {
            if (ObterObjetoPorId(db, faca.IdFaca) == null)
            {
                //banco.IdBanco = db.Bancos.Max(c => c.IdBanco) + 1;
                db.Facas.Add(faca);
            }

            db.SaveChanges();
        }

        public void Delete(ZenContext db, Facas faca)
        {
            db.Facas.Remove(faca);
            db.SaveChanges();
        }
    }
}